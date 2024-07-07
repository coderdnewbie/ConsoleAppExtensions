using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleAppExtensions.Extensions;

public static partial class StringExtensions // partial needed due to generatedregex pattern
{
    public static bool IsEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value?.Trim());
    }

    public static bool IsNotEmpty(this string? value)
    {
        return !string.IsNullOrEmpty(value?.Trim());
    }

    // IsNotEmpty and HasValue are the same but these variations improve readability
    public static bool HasValue(this string? value)
    {
        return !string.IsNullOrEmpty(value?.Trim());
    }

    public static string IsEmptyThenDefault(this string? value, string defaultValue)
    {
        switch (value)
        {
            case null:
                return defaultValue;
            default:
                {
                    if (value.IsEmpty())
                        return defaultValue;

                    return value;
                }
        }
    }

    public static string EmptyIfNullOtherwiseTrimmed(this string? value)
    {
        return value is null ? string.Empty : value.Trim();
    }

    public static string BlankIfNullOrEmpty(this string? value)
    {
        return string.IsNullOrEmpty(value?.Trim()) ? string.Empty : value;
    }

    public static string BlankIfNullOrWhiteSpace(this string? value)
    {
        return string.IsNullOrWhiteSpace(value?.Trim()) ? string.Empty : value;
    }

    public static string RemoveSuffixFromEnd(this string? original, string suffix)
    {
        if (string.IsNullOrWhiteSpace(original))
        {
            return string.Empty;
        }

        if (string.IsNullOrWhiteSpace(suffix))
        {
            return original;
        }

        if (!original.EndsWith(suffix))
        {
            return original;
        }

        return original[0..(original.Length - suffix.Length)];
    }


    public static int StrToIntDef(this string? value, int defaultValue)
    {
        switch (value)
        {
            case null:
                return defaultValue;
            default:
                {
                    if (!int.TryParse(value, out int result))
                        return defaultValue;

                    return result;
                }
        }
    }

    public static string ToTitleCase(this string? value)
    {
        switch (value)
        {
            case null:
                return string.Empty;
            default:
                {
                    if (value.IsEmpty())
                        return string.Empty;

                    return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
                }
        }
    }

    public static string ToCsv(this IEnumerable<string?> itemList)
    {
        return string.Join(",", itemList);
    }

    // Actual sentence case is a lot more complex than this, this is for basic strings for <h1> etc
    public static string ToSentenceCase(this string? value)
    {
        if (value.IsEmpty())
        {
            return string.Empty;
        }
        else
        {
            value ??= string.Empty;
            bool IsNewSentense = true;
            var result = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                if (IsNewSentense && char.IsLetter(value[i]))
                {
                    result.Append(char.ToUpper(value[i]));
                    IsNewSentense = false;
                }
                else
                    result.Append(value[i]);

                if (value[i] == '!' || value[i] == '?' || value[i] == '.')
                {
                    IsNewSentense = true;
                }
            }

            return result.ToString();
        }
    }

    public static List<string> MakeListfromSingleItem(this string? item)
    {
        var result = new List<string>();
        if (!string.IsNullOrWhiteSpace(item))
        {
            result.Add(item);
        }
        return result;
    }

    // Gets the valid 3 digit image extension or returns txt (note: handles 'jpeg' correctly)
    public static string GetExtensionElseTxt(this string? filename, string defaultExtension = "txt")
    {
        if (filename.IsEmpty()) { return defaultExtension; }

        if (filename is not null)
        {
            int index = filename.LastIndexOf('.');

            if (index < 0) { return (defaultExtension); }

            var res = filename[index..].ToLower().Trim('.');

            if (res == "jpeg") { return ("jpeg"); }
            if (res.Length != 3) { return (defaultExtension); }
            return res;
        }

        return defaultExtension;
    }

    public static string NormalizeLineEndings(this string? item) //** unit tests done **
    {
        if (item.IsEmpty()) { return string.Empty; }
        if (item is not null)
        {
            return item.Replace("\\r\\n", "\\n").Replace("\\r", "\\n").Replace("\\n", Environment.NewLine);
        }
        return string.Empty;
    }

    // makes double spaces into a single space etc 
    public static string RemoveExtraSpaces(this string? item) //** unit tests done **
    {
        if (item.IsEmpty()) { return string.Empty; }
        if (item is not null)
        {
            return string.IsNullOrEmpty(item) ? string.Empty : ExtraSpacesRegex().Replace(item, " ");
        }
        return string.Empty;
    }

    [GeneratedRegex("\\s+")]
    private static partial Regex ExtraSpacesRegex();

    // count the number of words in the string
    public static int WordCount(this string? input)
    {
        if (input.IsEmpty()) { return 0; }

        if (input is not null && input.Length > 0)
        {
            var count = 0;
            try
            {
                // Exclude whitespace, Tabs and line breaks
                var re = WordCountRegex();
                var matches = re.Matches(input);
                count = matches.Count;
            }
            catch
            {
            }
            return count;
        }

        return 0;
    }

    [GeneratedRegex("[^\\s]+")]
    private static partial Regex WordCountRegex();

    public static string ChopString(this string? item, int maxLength)
    {
        if (item.IsEmpty()) { return string.Empty; }
        return GetFromBeginning(item, maxLength, "");
    }

    // Example is:  FromBeg...tsToTheEnd ("FromBeginningWithAMiddleThatIsThereAndGetsToTheEnd") [maxLengthIncludingFill = 20]
    public static string GetFromMiddle(this string? item, int maxLengthIncludingFill, string fill = "...", int endCharsToKeep = 10)
    {
        if (item.IsEmpty()) { return string.Empty; }

        if (item is not null)
        {
            if (item.Length <= maxLengthIncludingFill) { return item; }
            if (maxLengthIncludingFill <= (endCharsToKeep + fill.Length)) { return item; }
            fill ??= string.Empty;
            return item[0..(maxLengthIncludingFill - endCharsToKeep - fill.Length)] + string.Concat(fill, item.AsSpan(item.Length - endCharsToKeep));
        }
        return string.Empty;
    }

    // Example is:  FromBeginning...  ("FromBeginningWithAMiddleThatIsThereAndGetsToTheEnd") [maxLengthIncludingFill = 16]
    public static string GetFromBeginning(this string? item, int maxLengthIncludingFill, string fill = "...")
    {
        if (item.IsEmpty()) { return string.Empty; }

        if (item is not null)
        {
            if (item.Length <= maxLengthIncludingFill) { return item; }
            fill ??= string.Empty;
            return item[0..(maxLengthIncludingFill - fill.Length)] + fill;
        }
        return string.Empty;
    }

    // Example is:  ...AndGetsToTheEnd  ("FromBeginningWithAMiddleThatIsThereAndGetsToTheEnd") [maxLengthIncludingFill = 18]
    public static string GetFromEnd(this string? item, int maxLengthIncludingFill, string fill = "...")
    {
        if (item.IsEmpty()) { return string.Empty; }
        if (item is not null)
        {
            if (item.Length <= maxLengthIncludingFill) { return item; }
            fill ??= string.Empty;
            return string.Concat(fill, item.AsSpan(item.Length - (maxLengthIncludingFill - fill.Length)));
        }
        return string.Empty;
    }
}
