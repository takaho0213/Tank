using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class IntervalScript : MonoBehaviour, IInterval
{
    /// <summary>時間</summary>
    [SerializeField] private float time;

    /// <summary>自動でリセットするか</summary>
    [SerializeField] private bool isAutoReSet;

    /// <summary>ストップ中か</summary>
    [SerializeField] private bool isStop;

    /// <summary>時間</summary>
    public float Time { get => time; set => time = value; }

    /// <summary>自動でリセットするか</summary>
    public bool IsAutoReSet { get => isAutoReSet; set => isAutoReSet = value; }

    /// <summary>ストップ中か</summary>
    public bool IsStop { get => isStop; set => isStop = value; }

    /// <summary>リセット時の時間</summary>
    public float LastTime { get; private set; }

    /// <summary>経過時間</summary>
    public float ElapsedTime => UpdateTime - LastTime;

    /// <summary>残り時間</summary>
    public float TimeLimit => time - ElapsedTime;

    /// <summary>更新時間</summary>
    public float UpdateTime {  get; private set; }

    /// <summary>インターバルを越えたか</summary>
    public bool IsOver
    {
        get
        {
            bool isOver = ElapsedTime >= time;     //インターバルを越えたか

            if (isOver && isAutoReSet) ReSetTime();//インターバルを越えた かつ オートリセットなら/リセット

            return isOver;                         //インターバルを越えたかを返す
        }
    }

    /// <summary>リセット</summary>
    public void ReSetTime() => LastTime = UpdateTime;

    private void Update()
    {
        if (!isStop)                                 //停止中なければ
        {
            UpdateTime += UnityEngine.Time.deltaTime;//デルタタイムを加算代入
        }
    }

    public static implicit operator bool(IntervalScript i) => i.IsOver;

#if UNITY_EDITOR
    [CustomEditor(typeof(IntervalScript))]
    public class IntervalScriptEditor : Editor
    {
        private IntervalScript instance;

        private void OnEnable() => instance = (IntervalScript)target;

        public override void OnInspectorGUI()
        {
            instance.time = EditorGUILayout.DelayedFloatField(nameof(Time), instance.time);
            instance.isAutoReSet = EditorGUILayout.Toggle(nameof(IsAutoReSet), instance.isAutoReSet);
            instance.isStop = EditorGUILayout.Toggle(nameof(IsStop), instance.isStop);

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            using (new EditorGUI.DisabledGroupScope(true))
            {
                EditorGUILayout.FloatField(nameof(LastTime), instance.LastTime);
                EditorGUILayout.FloatField(nameof(ElapsedTime), instance.ElapsedTime);
                EditorGUILayout.FloatField(nameof(TimeLimit), instance.TimeLimit);
                EditorGUILayout.FloatField(nameof(UpdateTime), instance.UpdateTime);
            }
        }
    }
#endif
}