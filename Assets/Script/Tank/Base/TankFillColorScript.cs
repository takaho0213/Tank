using UnityEngine;
using System.Collections;

public class TankFillColorScript : MonoBehaviour
{
    /// <summary>�F��ς���X�v���C�g�z��</summary>
    [SerializeField, LightColor] private SpriteRenderer[] fillSprites;

    /// <summary>�^���N�̐F</summary>
    [SerializeField] private Color fillColor;

    /// <summary>�ҋ@����</summary>
    private WaitForSeconds wait;

    /// <summary>�F</summary>
    public Color Color => fillColor;

    /// <summary>�G�l�~�[�̏����Z�b�g</summary>
    /// <param name="i">�G�l�~�[�̏��</param>
    public void SetInfo(TankEnemyInfoScript i) => SetColor(fillColor = i.FillColor);

    /// <summary>�F���O���[�ɃZ�b�g</summary>
    public void SetGray() => SetColor(Color.gray);

    /// <summary>�F���Z�b�g</summary>
    public void SetColor() => SetColor(fillColor);

    /// <summary>�F���Z�b�g</summary>
    /// <param name="color">�Z�b�g����F</param>
    public void SetColor(Color color)
    {
        foreach (var s in fillSprites)//FillSprites�̗v�f�����J��Ԃ�
        {
            s.color = color;          //�����̒l����
        }
    }

    /// <summary>�_���[�W���o</summary>
    /// <param name="waitTime">�ҋ@����</param>
    public IEnumerator DamageColor(float waitTime)
    {
        SetGray();                                         //�F���O���[�ɂ���

        yield return wait ??= new WaitForSeconds(waitTime);//�ҋ@

        SetColor();                                        //�F���Z�b�g
    }
}
