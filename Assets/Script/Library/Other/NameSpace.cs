#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Linq;
#endif

namespace MyEditor
{
#if UNITY_EDITOR
    [InitializeOnLoad]
    internal static class HierarchyStripes
    {
        private const int RowHeight = 16;
        private const int OffSetY = -4;
        private const int X = 32;

        private const float ColorAlpha = 0.05f;

        static HierarchyStripes() => EditorApplication.hierarchyWindowItemOnGUI += OnGUI;

        private static void OnGUI(int instanceID, Rect rect)
        {
            if (MathEx.IsEven((int)(rect.y + OffSetY) / RowHeight)) return;

            rect.x = X;

            rect.xMax += RowHeight;

            EditorGUI.DrawRect(rect, new(default, default, default, ColorAlpha));
        }
    }
#endif
}
//Input.anyKey    /�����̃L�[��������Ă��邩
//Input.anyKeyDown/�����̃L�[�������ꂽ�ŏ���1F����true��Ԃ�
//Input.inputString/���͂��ꂽ�L�[��Ԃ�

// �A�Z�b�g�̃^�C�v�ɉ������A�C�R�����擾������@
//Texture2D icon = EditorGUIUtility.ObjectContent(null, typeof(UnityEngine.Sprite)).image as Texture2D;

//���j���[�̎��s                      : EditorApplication.ExecuteMenuItem
//�A�Z�b�g�C���|�[�g�̎��O / ���㏈�� : AssetPostprocessor
//���̃t���[���ŃA�N�V���������s����  : EditorApplication.delayCall
//�c�[���o�[�Ƀ��j���[��ǉ�����      : MenuItem
//�R���e�L�X�g���j���[��ǉ�����      : ContextMenu
//�R���p�C����ɃX�N���v�g�����s����  : Callbacks.DidReloadScripts
//�p�b�P�[�W�}�l�[�W��UI�̊g��        : PackageManager.UI.IPackageManagerExtension
//������Ń��\�b�h���Ăԋ@�\          : SendMessage
