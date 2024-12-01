using UnityEngine;
using UnityEngine.EventSystems;

public class TankInputMoveScript : TankMoveScript
{
    /// <summary>inputModule</summary>
    [SerializeField, LightColor] private StandaloneInputModule inputModule;

    /// <summary>���̓x�N�g��</summary>
    private Vector2 inputVector;

    /// <summary>���͈ړ�</summary>
    public void InputMove()
    {
        inputVector.x = Input.GetAxisRaw(inputModule.horizontalAxis);//���̓x�N�g��X����
        inputVector.y = Input.GetAxisRaw(inputModule.verticalAxis);  //���̓x�N�g��Y����

        ribo.velocity = inputVector * moveSpeed;                     //(���̓x�N�g�� * �ړ����x)���ړ��x�N�g���ɑ��
    }
}
