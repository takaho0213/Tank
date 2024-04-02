using System;
using System.Collections.Generic;
using UnityEngine;

# if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>オブジェクトプールを容易に実装するクラス</summary>
[System.Serializable]
public class PoolList<T> where T : MonoBehaviour
{
    /// <summary>生成元</summary>
    [SerializeField] private T original;

    /// <summary>プールリスト</summary>
    private List<T> list = new();

    /// <summary>生成元</summary>
    public T Original
    {
        get => original;
        set => original = value;
    }

    /// <summary>読み取り専用のリスト</summary>
    public IReadOnlyList<T> List => list;

    /// <summary>アクティブか</summary>
    private bool IsActive(T v) => v.gameObject.activeSelf;

    /// <summary>インスタンスを生成</summary>
    private T Instantiate() => UnityEngine.Object.Instantiate(original);

    /// <summary>非アクティブなプールされているインスタンスを返す</summary>
    public T Pool() => Pool(list, IsActive, Instantiate);

    /// <summary>引数の条件のプールされているインスタンスを返す</summary>
    /// <param name="isActive">プールされているインスタンスを返す条件の関数</param>
    public T Pool(Func<T, bool> isActive) => Pool(list, isActive, Instantiate);

    /// <summary>非アクティブなプールされているインスタンスを返す</summary>
    /// <param name="instantiate">インスタンスを生成する関数</param>
    public T Pool(Func<T> instantiate) => Pool(list, IsActive, instantiate);

    /// <summary>引数の条件のプールされているインスタンスを返す</summary>
    /// <param name="isActive">プールされているインスタンスを返す条件の関数</param>
    /// <param name="instantiate">インスタンスを生成する関数</param>
    public T Pool(Func<T, bool> isActive, Func<T> instantiate) => Pool(list, isActive, instantiate);

    /// <summary>引数の条件のプールされているインスタンスを返す</summary>
    /// <param name="list">プールリスト</param>
    /// <param name="isActive">プールされているインスタンスを返す条件の関数</param>
    /// <param name="instantiate">インスタンスを生成する関数</param>
    /// <returns></returns>
    public static T Pool(List<T> list, Func<T, bool> isActive, Func<T> instantiate)
    {
        foreach (var v in list) if (!isActive.Invoke(v)) return v;

        var value = instantiate.Invoke();

        list.Add(value);

        return value;
    }

}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(PoolList<>))]
public class PoolListDrawer : PropertyDrawer
{
    public override void OnGUI(Rect r, SerializedProperty p, GUIContent l)
    {
        var o = p.FindPropertyRelative("original");

        if (o != null) EditorGUI.PropertyField(r, o, l);
    }
}
#endif
