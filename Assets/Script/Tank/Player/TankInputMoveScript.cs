using UnityEngine;
using UnityEngine.EventSystems;

public class TankInputMoveScript : TankMoveScript
{
    /// <summary>inputModule</summary>
    [SerializeField, LightColor] private StandaloneInputModule inputModule;

    /// <summary>入力ベクトル</summary>
    private Vector2 inputVector;

    /// <summary>入力移動</summary>
    public void InputMove()
    {
        inputVector.x = Input.GetAxisRaw(inputModule.horizontalAxis);//入力ベクトルXを代入
        inputVector.y = Input.GetAxisRaw(inputModule.verticalAxis);  //入力ベクトルYを代入

        ribo.velocity = inputVector * moveSpeed;                     //(入力ベクトル * 移動速度)を移動ベクトルに代入
    }
}
