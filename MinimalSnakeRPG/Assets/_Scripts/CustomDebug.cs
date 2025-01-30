using UnityEngine;

public static class CustomDebug
{
    public static bool IsDisableJDebug
    {
        get => _isDisableJDebug;
        set => _isDisableJDebug = value;
    }

    private static bool _isDisableJDebug;

    public static void SetMessage(string message, Color color = default, bool isDebug = true)
    {
        if (_isDisableJDebug) return;
        if (!isDebug) return;
        var m = (color == default)
            ? message
            : $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{message}</color>";
        MonoBehaviour.print(m);
    }
    
    public static void SetWarningMessage(string message, bool isDebug)
    {
        if (_isDisableJDebug) return;
        if (!isDebug) return;
        var m = $"<color=#{ColorUtility.ToHtmlStringRGBA(Color.yellow)}> ?? WARNING : {message}</color>";
        MonoBehaviour.print(m);
    }
    
    public static void SetErrorMessage(string message, bool isForceBrake = false, bool isDebug = true)
    {
        if (_isDisableJDebug) return;
        if (!isDebug) return;
        var m = $"<color=#{ColorUtility.ToHtmlStringRGBA(Color.red)}> ?? ERROR : {message}</color>";
        MonoBehaviour.print(m);
        if (isForceBrake) Debug.Break();
    }
}