using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FaderScript : MonoBehaviour
{
    /// <summary>待機インターバル</summary>
    [SerializeField] protected Interval wait;

    /// <summary>フェード速度</summary>
    [SerializeField] protected float speed;

    /// <summary>フェードイン終了時実行するコールバック</summary>
    protected UnityAction onFadeInEnd;

    /// <summary>フェードアウト終了時実行するコールバック</summary>
    protected UnityAction onFadeOutEnd;

    /// <summary>待機インターバル</summary>
    public IInterval Wait => wait;

    /// <summary>フェードイン終了時の待機時間</summary>
    public float WaitTime { get => wait.Time; set => wait.Time = value; }

    /// <summary>フェード速度</summary>
    public float Speed { get => speed; set => speed = value; }

    /// <summary>フェードイン時のアルファ値</summary>
    protected virtual float InValue => MathEx.OneF;//MathEx.OneF = 1f;
    /// <summary>フェードアウト時のアルファ値</summary>
    protected virtual float OutValue => MathEx.ZeroF;//MathEx.ZeroF = 0f;

    /// <summary>フェードさせる値</summary>
    public virtual float FadeValue { get; set; }

    /// <summary>目標の値</summary>
    public float Target { get; protected set; }

    /// <summary>フェードアウト中か</summary>
    public bool IsFadeOut { get; protected set; }

    /// <summary>フェード中か</summary>
    public bool IsRun { get; protected set; }

    /// <summary>フェードを開始</summary>
    /// <param name="onFadeInEnd">フェードイン終了時実行するコールバック</param>
    /// <param name="onFadeOutEnd">フェードアウト終了時実行するコールバック</param>
    public virtual void Run(UnityAction onFadeInEnd, UnityAction onFadeOutEnd)
    {
        if (IsRun) return;               //フェード中なら/終了

        this.onFadeInEnd = onFadeInEnd;  //フェードイン終了時実行するコールバックを代入
        this.onFadeOutEnd = onFadeOutEnd;//フェードアウト終了時実行するコールバックを代入

        Target = InValue;                //目標の値を代入

        IsRun = true;                    //フェード中かをtrue
    }

    /// <summary>フェードを開始</summary>
    /// <param name="onFadeInEnd">フェードイン終了時実行するコールバック</param>
    public virtual void Run(UnityAction onFadeInEnd) => Run(onFadeInEnd, null);

    /// <summary>フェードを開始</summary>
    public virtual void Run() => Run(null, null);

    /// <summary>フェードイン終了時実行</summary>
    protected virtual void OnFadeInEnd()
    {
        onFadeInEnd?.Invoke();//フェードイン終了時のコールバックを呼び出す

        wait.ReSet();         //インターバルをリセット
    }

    /// <summary>フェードアウト終了時実行</summary>
    protected virtual void OnFadeOutEnd()
    {
        onFadeOutEnd?.Invoke();//コールバックを呼び出す

        IsRun = false;         //フェードを終了
    }

    /// <summary>フェード</summary>
    protected virtual void Fade()
    {
        var value = FadeValue;                      //フェードさせる値を代入

        if (Mathf.Approximately(value, Target))     //目標の値とほぼ等しかったら
        {
            FadeValue = Target;                     //目標の値を代入

            Target = IsFadeOut ? InValue : OutValue;//次の目標の値を代入

            if (IsFadeOut) OnFadeOutEnd();          //フェードアウト中なら フェードアウト終了時実行する関数を呼び出す
            else OnFadeInEnd();                     //それ以外なら         フェードイン  終了時実行する関数を呼び出す

            IsFadeOut = !IsFadeOut;                 //反転

            return;                                 //終了
        }

        if (wait.IsOver)                                                         //待機中でなければ
        {
            FadeValue = Mathf.MoveTowards(value, Target, speed * Time.deltaTime);//フェードさせた値を代入
        }
    }

    protected virtual void Update()
    {
        if (IsRun) Fade();//フェード中なら/フェード
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(FaderScript), true)]
    public class FaderEditor : Editor
    {
        protected FaderScript script;

        protected virtual void OnEnable() => script = (FaderScript)target;

        protected virtual void Field()
        {
            script.wait.Time = GUIL.Field(nameof(WaitTime), script.wait.Time);

            script.speed = GUIL.Field(nameof(Speed), script.speed);

            GUIL.Space();

            try
            {
                var value = script.FadeValue;

                script.FadeValue = GUIL.Field(nameof(script.FadeValue), script.FadeValue, script.OutValue, script.InValue);

                if (value != script.FadeValue)
                {
                    EditorApplication.QueuePlayerLoopUpdate();
                }
            }
            catch (System.Exception e)
            {
                EditorGUILayout.HelpBox(e.Message, MessageType.Error);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Field();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            GUIL.FlexibleLabel("------------------------------base------------------------------");
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            base.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}

//フェードの定義

//0 => 1 : フェードイン
//1 => 0 : フェードアウト