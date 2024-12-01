using UnityEngine;

public static class InputEx { }

public static class Axis
{
    /// <summary>����</summary>
    public const string Horizontal = nameof(Horizontal);

    /// <summary>�c��</summary>
    public const string Vertical = nameof(Vertical);

    /// <summary>�����̒l</summary>
    public static float ValueX => Input.GetAxis(Horizontal);
    /// <summary>��Ԗ����̉����̒l</summary>
    public static float ValueRawX => Input.GetAxisRaw(Horizontal);
    /// <summary>�����̒l(int)</summary>
    public static int ValueXInt => (int)Input.GetAxis(Horizontal);
    /// <summary>��Ԗ����̉����̒l(int)</summary>
    public static int ValueRawXInt => (int)Input.GetAxisRaw(Horizontal);

    /// <summary>�c���̒l</summary>
    public static float ValueY => Input.GetAxis(Vertical);
    /// <summary>��Ԗ����̏c���̒l</summary>
    public static float ValueRawY => Input.GetAxisRaw(Vertical);
    /// <summary>�c���̒l(int)</summary>
    public static int ValueYInt => (int)Input.GetAxis(Vertical);
    /// <summary>��Ԗ����̏c���̒l(int)</summary>
    public static int ValueRawYInt => (int)Input.GetAxisRaw(Vertical);

    /// <summary>�l</summary>
    public static Vector2 Value => new(ValueX, ValueY);
    /// <summary>��Ԗ����̒l</summary>
    public static Vector2 ValueRaw => new(ValueRawX, ValueRawY);
    /// <summary>�l(int)</summary>
    public static Vector2Int ValueInt => new(ValueXInt, ValueYInt);
    /// <summary>��Ԗ����̒l(int)</summary>
    public static Vector2Int ValueRawInt => new(ValueRawXInt, ValueRawYInt);
}

public static class Scroll
{
    /// <summary>�l</summary>
    public static Vector2 Value => Input.mouseScrollDelta;
    /// <summary>�l(int)</summary>
    public static Vector2Int ValueInt => Vector2Int.RoundToInt(Input.mouseScrollDelta);

    /// <summary>Y�l</summary>
    public static float ValueY => Input.mouseScrollDelta.y;
    /// <summary>X�l</summary>
    public static float ValueX => Input.mouseScrollDelta.x;

    /// <summary>Y�l(int)</summary>
    public static int ValueYInt => (int)Input.mouseScrollDelta.y;
    /// <summary>X�l(int)</summary>
    public static int ValueXInt => (int)Input.mouseScrollDelta.x;

    /// <summary>�X�N���[�����ꂽ���H</summary>
    public static bool Any => Input.mouseScrollDelta != default;
    /// <summary>Y�����X�N���[�����ꂽ���H</summary>
    public static bool AnyY => Input.mouseScrollDelta.y != default;
    /// <summary>X�����X�N���[�����ꂽ���H</summary>
    public static bool AnyX => Input.mouseScrollDelta.x != default;

    /// <summary>������ɃX�N���[�����ꂽ���H</summary>
    public static bool IsUp => Input.mouseScrollDelta.y > default(float);
    /// <summary>�������ɃX�N���[�����ꂽ���H</summary>
    public static bool IsDown => Input.mouseScrollDelta.y < default(float);
    /// <summary>�E�����ɃX�N���[�����ꂽ���H</summary>
    public static bool IsRight => Input.mouseScrollDelta.x > default(float);
    /// <summary>�������ɃX�N���[�����ꂽ���H</summary>
    public static bool IsLeft => Input.mouseScrollDelta.x < default(float);

    /// <summary>�X�N���[�����ꂽ���H</summary>
    /// <param name="value">�l</param>
    public static bool AnyValue(out Vector2 value) => (value = Input.mouseScrollDelta) != default;
    /// <summary>�X�N���[�����ꂽ���H</summary>
    /// <param name="value">�l(int)</param>
    public static bool AnyValueInt(out Vector2Int value) => (value = Vector2Int.RoundToInt(Input.mouseScrollDelta)) != default;

    /// <summary>Y�����X�N���[�����ꂽ���H</summary>
    /// <param name="value">�l</param>
    public static bool AnyValueY(out float value) => (value = Input.mouseScrollDelta.y) != default;
    /// <summary>X�����X�N���[�����ꂽ���H</summary>
    /// <param name="value">�l</param>
    public static bool AnyValueX(out float value) => (value = Input.mouseScrollDelta.x) != default;

    /// <summary>Y�����X�N���[�����ꂽ���H</summary>
    /// <param name="value">�l(int)</param>
    public static bool AnyValueY(out int value) => (value = (int)Input.mouseScrollDelta.y) != default;
    /// <summary>X�����X�N���[�����ꂽ���H</summary>
    /// <param name="value">�l(int)</param>
    public static bool AnyValueX(out int value) => (value = (int)Input.mouseScrollDelta.x) != default;
}

public static class Key
{
    /// <summary>KeyCode�̂��ׂĂ̗v�f</summary>
    public static readonly KeyCode[] KeyCodes = EnumEx<KeyCode>.Values;

    /// <summary>���͂��ꂽ������</summary>
    public static string String => Input.inputString;

    /// <summary>�����񂪓��͂��ꂽ���H</summary>
    /// <param name="text">���͂��ꂽ������</param>
    public static bool AnyString(out string text) => !string.IsNullOrEmpty(text = Input.inputString);

    /// <summary>��������̃L�[�������ꂽ��</summary>
    /// <param name="key">�����ꂽ�L�[</param>
    public static bool Any(out KeyCode key)
    {
        key = default;

        foreach (var code in KeyCodes)
        {
            if (Input.GetKey(code))
            {
                key = code;

                break;
            }
        }

        return key != default;
    }

    /// <summary>��������̃L�[�������ꂽ��</summary>
    /// <param name="key">�����ꂽ�L�[</param>
    public static bool AnyDown(out KeyCode key)
    {
        key = default;

        foreach (var code in KeyCodes)
        {
            if (Input.GetKeyDown(code))
            {
                key = code;

                break;
            }
        }

        return key != default;
    }

    /// <summary>��������̃L�[�������ꂽ��</summary>
    /// <param name="key">�����ꂽ�L�[</param>
    public static bool AnyUp(out KeyCode key)
    {
        key = default;

        foreach (var code in KeyCodes)
        {
            if (Input.GetKeyUp(code))
            {
                key = code;

                break;
            }
        }

        return key != default;
    }

    /// <summary>�L�[��������Ă��邩</summary>
    public static bool IsPush(KeyCode key) => Input.GetKey(key);
    /// <summary>�L�[��������Ă��邩</summary>
    public static bool IsPushDown(KeyCode key) => Input.GetKeyDown(key);
    /// <summary>�L�[��������Ă��邩</summary>
    public static bool IsPushUp(KeyCode key) => Input.GetKeyUp(key);
}