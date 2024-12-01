using UnityEngine;

public static class InputEx { }

public static class Axis
{
    /// <summary>横軸</summary>
    public const string Horizontal = nameof(Horizontal);

    /// <summary>縦軸</summary>
    public const string Vertical = nameof(Vertical);

    /// <summary>横軸の値</summary>
    public static float ValueX => Input.GetAxis(Horizontal);
    /// <summary>補間無しの横軸の値</summary>
    public static float ValueRawX => Input.GetAxisRaw(Horizontal);
    /// <summary>横軸の値(int)</summary>
    public static int ValueXInt => (int)Input.GetAxis(Horizontal);
    /// <summary>補間無しの横軸の値(int)</summary>
    public static int ValueRawXInt => (int)Input.GetAxisRaw(Horizontal);

    /// <summary>縦軸の値</summary>
    public static float ValueY => Input.GetAxis(Vertical);
    /// <summary>補間無しの縦軸の値</summary>
    public static float ValueRawY => Input.GetAxisRaw(Vertical);
    /// <summary>縦軸の値(int)</summary>
    public static int ValueYInt => (int)Input.GetAxis(Vertical);
    /// <summary>補間無しの縦軸の値(int)</summary>
    public static int ValueRawYInt => (int)Input.GetAxisRaw(Vertical);

    /// <summary>値</summary>
    public static Vector2 Value => new(ValueX, ValueY);
    /// <summary>補間無しの値</summary>
    public static Vector2 ValueRaw => new(ValueRawX, ValueRawY);
    /// <summary>値(int)</summary>
    public static Vector2Int ValueInt => new(ValueXInt, ValueYInt);
    /// <summary>補間無しの値(int)</summary>
    public static Vector2Int ValueRawInt => new(ValueRawXInt, ValueRawYInt);
}

public static class Scroll
{
    /// <summary>値</summary>
    public static Vector2 Value => Input.mouseScrollDelta;
    /// <summary>値(int)</summary>
    public static Vector2Int ValueInt => Vector2Int.RoundToInt(Input.mouseScrollDelta);

    /// <summary>Y値</summary>
    public static float ValueY => Input.mouseScrollDelta.y;
    /// <summary>X値</summary>
    public static float ValueX => Input.mouseScrollDelta.x;

    /// <summary>Y値(int)</summary>
    public static int ValueYInt => (int)Input.mouseScrollDelta.y;
    /// <summary>X値(int)</summary>
    public static int ValueXInt => (int)Input.mouseScrollDelta.x;

    /// <summary>スクロールされたか？</summary>
    public static bool Any => Input.mouseScrollDelta != default;
    /// <summary>Y軸がスクロールされたか？</summary>
    public static bool AnyY => Input.mouseScrollDelta.y != default;
    /// <summary>X軸がスクロールされたか？</summary>
    public static bool AnyX => Input.mouseScrollDelta.x != default;

    /// <summary>上方向にスクロールされたか？</summary>
    public static bool IsUp => Input.mouseScrollDelta.y > default(float);
    /// <summary>下方向にスクロールされたか？</summary>
    public static bool IsDown => Input.mouseScrollDelta.y < default(float);
    /// <summary>右方向にスクロールされたか？</summary>
    public static bool IsRight => Input.mouseScrollDelta.x > default(float);
    /// <summary>左方向にスクロールされたか？</summary>
    public static bool IsLeft => Input.mouseScrollDelta.x < default(float);

    /// <summary>スクロールされたか？</summary>
    /// <param name="value">値</param>
    public static bool AnyValue(out Vector2 value) => (value = Input.mouseScrollDelta) != default;
    /// <summary>スクロールされたか？</summary>
    /// <param name="value">値(int)</param>
    public static bool AnyValueInt(out Vector2Int value) => (value = Vector2Int.RoundToInt(Input.mouseScrollDelta)) != default;

    /// <summary>Y軸がスクロールされたか？</summary>
    /// <param name="value">値</param>
    public static bool AnyValueY(out float value) => (value = Input.mouseScrollDelta.y) != default;
    /// <summary>X軸がスクロールされたか？</summary>
    /// <param name="value">値</param>
    public static bool AnyValueX(out float value) => (value = Input.mouseScrollDelta.x) != default;

    /// <summary>Y軸がスクロールされたか？</summary>
    /// <param name="value">値(int)</param>
    public static bool AnyValueY(out int value) => (value = (int)Input.mouseScrollDelta.y) != default;
    /// <summary>X軸がスクロールされたか？</summary>
    /// <param name="value">値(int)</param>
    public static bool AnyValueX(out int value) => (value = (int)Input.mouseScrollDelta.x) != default;
}

public static class Key
{
    /// <summary>KeyCodeのすべての要素</summary>
    public static readonly KeyCode[] KeyCodes = EnumEx<KeyCode>.Values;

    /// <summary>入力された文字列</summary>
    public static string String => Input.inputString;

    /// <summary>文字列が入力されたか？</summary>
    /// <param name="text">入力された文字列</param>
    public static bool AnyString(out string text) => !string.IsNullOrEmpty(text = Input.inputString);

    /// <summary>何かしらのキーが押されたか</summary>
    /// <param name="key">押されたキー</param>
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

    /// <summary>何かしらのキーが押されたか</summary>
    /// <param name="key">押されたキー</param>
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

    /// <summary>何かしらのキーが押されたか</summary>
    /// <param name="key">押されたキー</param>
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

    /// <summary>キーが押されているか</summary>
    public static bool IsPush(KeyCode key) => Input.GetKey(key);
    /// <summary>キーが押されているか</summary>
    public static bool IsPushDown(KeyCode key) => Input.GetKeyDown(key);
    /// <summary>キーが押されているか</summary>
    public static bool IsPushUp(KeyCode key) => Input.GetKeyUp(key);
}