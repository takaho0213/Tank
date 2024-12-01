﻿using UnityEngine;
using UnityEngine.Events;

public class TankLifeScript : MonoBehaviour
{
    /// <summary>残りライフ</summary>
    public int LifeCount { get; private set; }

    /// <summary>何ステージクリアしたら残機を得るか</summary>
    [SerializeField] private int addLifeCount;

    /// <summary>残機を増やすか判定する</summary>
    /// <param name="stageCount">現在ステージ数</param>
    /// <returns>残機を増やすか</returns>
    public bool IsAddLife(int stageCount) => stageCount % addLifeCount == default;

    private UnityAction<StageClip> audioPlayer;

    private void Start()
    {
        audioPlayer = AudioScript.I.StageAudio.Play;
    }

    /// <summary>残機を1増やす</summary>
    public void AddLife()
    {
        LifeCount++;                                          //ライフを + 1

        audioPlayer?.Invoke(StageClip.LifeAdd);//ライフ追加SEを再生
    }

    /// <summary>残機を1減らす</summary>
    public void ReMoveLife()
    {
        if (LifeCount > default(int))                                //ライフが0より上なら
        {
            LifeCount--;                                             //ライフを - 1

            audioPlayer?.Invoke(StageClip.LifeReMove);//ライフ減少SEを再生
        }
    }

    /// <summary></summary>
    public void ReSetLife()
    {
        LifeCount = default;
    }
}
