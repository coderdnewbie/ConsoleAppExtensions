namespace ConsoleAppExtensions.Extensions;

public static class BoolExtensions
{
    public static string ToYesNo(this bool b, string yes = "Yes", string no = "No")
    {
        return b ? yes : no;
    }
}
