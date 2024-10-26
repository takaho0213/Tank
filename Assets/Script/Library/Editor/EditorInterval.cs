# if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class EditorInterval
{
    private static double TIME => EditorApplication.timeSinceStartup;

    /// <summary>����</summary>
    [SerializeField] private double time;
    /// <summary>�����Ń��Z�b�g���邩</summary>
    [SerializeField] private bool isAutoReSet;

    /// <summary>����</summary>
    public double Time { get => time; set => time = value; }

    /// <summary>�����Ń��Z�b�g���邩</summary>
    public bool IsAutoReSet { get => isAutoReSet; set => isAutoReSet = value; }

    /// <summary>���Z�b�g���̎���</summary>
    public double LastTime { get; private set; }

    /// <summary>�o�ߎ���</summary>
    public double ElapsedTime => TIME - LastTime;

    /// <summary>�c�莞��</summary>
    public double TimeLimit => time - ElapsedTime;

    /// <summary>�C���^�[�o�����z������</summary>
    public bool IsOver
    {
        get
        {
            bool isOver = ElapsedTime >= time;

            if (isOver && isAutoReSet) ReSet();

            return isOver;
        }
    }

    /// <summary>���Z�b�g</summary>
    public void ReSet() => LastTime = TIME;

    public EditorInterval(double time = default, bool isAutoReSet = default, double lastTime = default)
    {
        this.time = time;
        this.isAutoReSet = isAutoReSet;
        LastTime = lastTime;
    }

    public static implicit operator bool(EditorInterval i) => i.IsOver;
}
# endif