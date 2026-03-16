namespace Yapper.Utilities;

internal static class MessageFormatter
{
    private const string SuccessColor = "#88cc88";
    private const string ErrorColor = "#cc8888";
    private const string InfoColor = "#88bbdd";
    private const string FlavorColor = "#b8d882";

    public static string Bold(object value)
    {
        return $"<font weight=\"bold\">{value}</font>";
    }

    public static string Flavor(string playerName, string flavorText)
    {
        return $"<font color=\"{FlavorColor}\" weight=\"bold\">{playerName} {flavorText}</font>";
    }

    public static string Success(string message)
    {
        return $"<font color=\"{SuccessColor}\">{message}</font>";
    }

    public static string Error(string message)
    {
        return $"<font color=\"{ErrorColor}\">{message}</font>";
    }

    public static string Info(string message)
    {
        return $"<font color=\"{InfoColor}\">{message}</font>";
    }
}
