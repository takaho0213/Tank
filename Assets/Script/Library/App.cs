/// <summary>�A�v���P�[�V�����N���X</summary>
public static class App
{
    /// <summary>�A�v���P�[�V�������I��</summary>
    public static void QuitGame()
    {
        #if UNITY_EDITOR                                //Editor���Ȃ�

        UnityEditor.EditorApplication.isPlaying = false;//�v���C���~

        #else                                           //����ȊO�Ȃ�

        UnityEngine.Application.Quit();                 //�A�v���P�[�V�������I��

        #endif                                          //�I��
    }
}
