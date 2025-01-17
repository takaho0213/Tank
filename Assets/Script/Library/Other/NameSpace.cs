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
//Input.anyKey    /何かのキーが押されているか
//Input.anyKeyDown/何かのキーが押された最初の1Fだけtrueを返す
//Input.inputString/入力されたキーを返す

// アセットのタイプに応じたアイコンを取得する方法
//Texture2D icon = EditorGUIUtility.ObjectContent(null, typeof(UnityEngine.Sprite)).image as Texture2D;

//メニューの実行                      : EditorApplication.ExecuteMenuItem
//アセットインポートの事前 / 事後処理 : AssetPostprocessor
//次のフレームでアクションを実行する  : EditorApplication.delayCall
//ツールバーにメニューを追加する      : MenuItem
//コンテキストメニューを追加する      : ContextMenu
//コンパイル後にスクリプトを実行する  : Callbacks.DidReloadScripts
//パッケージマネージャUIの拡張        : PackageManager.UI.IPackageManagerExtension
//文字列でメソッドを呼ぶ機能          : SendMessage
