using UnityEngine;
using UnityEngine.EventSystems;

public class TankInputMoveScript : MonoBehaviour
{
    /// <summary>inputModule</summary>
    [SerializeField, LightColor] private StandaloneInputModule inputModule;

    /// <summary>移動に使うRigidbody</summary>
    [SerializeField, LightColor] private Rigidbody2D ribo;

    /// <summary>移動SEを再生するAudioSource</summary>
    [SerializeField, LightColor] private AudioSource moveSource;

    /// <summary>移動速度</summary>
    [SerializeField] private float moveSpeed;

    /// <summary>SEを再生するインターバル</summary>
    private Interval seInterval;

    /// <summary>入力ベクトル</summary>
    private Vector2 inputVector;

    /// <summary>場所</summary>
    public Vector2 Pos => ribo.position;

    /// <summary>移動ベクトル</summary>
    public Vector3 Velocity => ribo.velocity;

    /// <summary>移動停止</summary>
    public void Stop() => ribo.velocity = Vector2.zero;

    /// <summary>入力移動</summary>
    public void InputMove()
    {
        inputVector.x = Input.GetAxisRaw(inputModule.horizontalAxis);//入力ベクトルXを代入
        inputVector.y = Input.GetAxisRaw(inputModule.verticalAxis);  //入力ベクトルYを代入

        ribo.velocity = inputVector * moveSpeed;                     //(入力ベクトル * 移動速度)を移動ベクトルに代入

        if (ribo.velocity != Vector2.zero)                           //移動ベクトルが(0, 0)以外なら
        {
            var c = AudioScript.I.TankAudio.Dictionary[TankClip.Move];    //移動SEクリップ

            seInterval ??= new(c.Clip.length / moveSpeed, true);     //nullなら代入

            if (seInterval) moveSource.PlayOneShot(c.Clip, c.Volume);//SE再生間隔を越えていたら/SEを再生
        }
    }
}
