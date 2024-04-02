using UnityEngine;

namespace Tank
{
    /// <summary>タイトル</summary>
    public class TitleScript : ImageFaderScript
    {
        /// <summary>ステージマネージャー</summary>
        [SerializeField, LightColor] private StageManagerScript StageManager;

        /// <summary>タイトルオブジェクト</summary>
        [SerializeField, LightColor] private GameObject TitleObj;

        private void Start()
        {
            ReStart();                         //リスタート

            StageManager.OnGameClear = ReStart;//ゲームクリア時実行する関数を代入
        }

        /// <summary>リスタート時実行</summary>
        private void ReStart()
        {
            Run(() => TitleObj.SetActive(true), () => AudioScript.I.StageAudio.Play(StageClip.BGM));//フェードを開始 フェードイン時ステージをアクティブ フェードアウト時BGMを再生
        }

        /// <summary>フェードイン時実行する関数</summary>
        private void OnFadeIn()
        {
            TitleObj.SetActive(false);                                   //タイトルをアクティブ

            AudioScript.I.StageAudio.Audios[StageClip.BGM].Source.Stop();//BGMを停止

            StageManager.OnReStartFadeIn();                              //リスタートのフェードアウト時実行する関数を実行
        }

        /// <summary>ゲームを開始</summary>
        private void GameStart()
        {
            if (IsRun) return;                           //フェード中なら/終了
    
            Run(OnFadeIn, StageManager.OnReStartFadeOut);//フェード開始 フェードイン時OnFadeIn関数を実行 フェードアウト時OnReStartFadeIn関数を実行
        }

        protected override void Update()
        {
            base.Update();                                           //基底クラスのUpdateをよびだす

            if (Input.GetMouseButton(default) && TitleObj.activeSelf)//右クリック かつ タイトルオブジェクトがアクティブなら
            {
                GameStart();                                         //右クリックが押されたら/ゲームを開始
            }
        }
    }
}