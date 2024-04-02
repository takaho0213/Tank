using UnityEngine;
using UnityEngine.Events;

/// <summary>�x�[�X�̃t�F�[�_�[</summary>
public abstract class BaseFaderScript : FaderScript, IFade
{
    public abstract Color FadeColor { get; set; }

    /// <summary>�t�F�[�h���J�n</summary>
    public void Run() => Run(this, null, null, null);

    /// <summary>�t�F�[�h���J�n</summary>
    /// <param name="i">�t�F�[�h�C�������s</param>
    public void Run(UnityAction i) => Run(this, i, null, null);

    /// <summary>�t�F�[�h���J�n</summary>
    /// <param name="i">�t�F�[�h�C�������s</param>
    /// <param name="o">�t�F�[�h�A�E�g�����s</param>
    public void Run(UnityAction i, UnityAction o) => Run(this, i, o, null);

    /// <summary>�t�F�[�h���J�n</summary>
    /// <param name="i">�t�F�[�h�C�������s</param>
    /// <param name="o">�t�F�[�h�A�E�g�����s</param>
    /// <param name="w">�ҋ@����</param>
    public void Run(UnityAction i, UnityAction o, System.Func<bool> w) => Run(this, i, o, w);
}
