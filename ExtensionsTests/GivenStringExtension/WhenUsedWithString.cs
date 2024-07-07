using ConsoleAppExtensions.Extensions;

namespace ExtensionsTests.GivenStringExtension;

public class WhenUsedWithString
{
    #region IsEmpty Tests
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void AndCalls_IsEmpty_WithEmptyValueThenTrue(string? value)
    {
        Assert.True(value.IsEmpty());
    }

    [Theory]
    [InlineData("q")]
    public void AndCalls_IsEmpty_WithNonEmptyValueThenFalse(string? value)
    {
        Assert.False(value.IsEmpty());
    }
    #endregion

    #region IsNotEmpty Tests
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void AndCalls_IsNotEmpty_WithEmptyValueThenFalse(string? value)
    {
        Assert.False(value.IsNotEmpty());
    }

    [Theory]
    [InlineData("q")]
    [InlineData("This is a string")]
    public void AndCalls_IsNotEmpty_WithNonEmptyValueThenTrue(string? value)
    {
        Assert.True(value.IsNotEmpty());
    }
    #endregion

    #region HasValue Tests
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void AndCalls_HasValue_WithEmptyValueThenFalse(string? value)
    {
        Assert.False(value.HasValue());
    }

    [Theory]
    [InlineData("q")]
    public void AndCalls_HasValue_WithNonEmptyValueThenTrue(string? value)
    {
        Assert.True(value.HasValue());
    }
    #endregion

    #region IsEmptyThenDefault Tests
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void AndCalls_IsEmptyThenDefault_WithEmptyValueThenReturnDefault(string value)
    {
        Assert.Equal("default_val", value.IsEmptyThenDefault("default_val"));
    }

    [Theory]
    [InlineData("test1")]
    [InlineData("t")]
    public void AndCalls_IsEmptyThenDefault_WithActualValueThenReturnActualValue(string value)
    {
        Assert.Equal(value, value.IsEmptyThenDefault("default_val"));
    }
    #endregion

    #region EmptyIfNullOtherwiseTrimmed Tests
    [Fact]
    public void AndCalls_EmptyIfNullOtherwiseTrimmed_IsNullThenReturnEmpty()
    {
        string? value = null;
        Assert.Equal("", value.EmptyIfNullOtherwiseTrimmed());
    }

    [Theory]
    [InlineData(" ")]
    [InlineData(" test1 ")]
    [InlineData("test2 ")]
    [InlineData(" test3")]
    public void AndCalls_EmptyIfNullOtherwiseTrimmed_WithActualValueThenReturnTrimmed(string value)
    {
        Assert.Equal(value.Trim(), value.EmptyIfNullOtherwiseTrimmed());
    }
    #endregion

    #region StrToIntDef Tests
    [Theory]
    [InlineData(null, -1, -1)]
    [InlineData("", -1, -1)]
    [InlineData("12", -1, 12)]
    public void AndCalls_StrToIntDef_WithActualValueThenStrToIntDef(string? value, int defaultValue, int expected)
    {
        Assert.Equal(expected, value.StrToIntDef(defaultValue));
    }
    #endregion

    #region ToTitleCase Tests
    [Fact]
    public void AndCalls_ToTitleCase_IsNullThenReturnEmpty()
    {
        string? value = null;
        Assert.Equal("", value.ToTitleCase());
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", "")]
    [InlineData("test this sentence", "Test This Sentence")]
    [InlineData(" test2", " Test2")]
    public void AndCalls_ToTitleCase_WithActualValueThenTitleCase(string value, string expected)
    {
        Assert.Equal(expected, value.ToTitleCase());
    }
    #endregion

    #region ToCsv Tests

    [Fact]
    public void AndCalls_ToCsv_WithEmptyListThenReturnEmpty()
    {
        List<string> emptyValue = [];
        Assert.Equal(string.Empty, emptyValue.ToCsv());
    }

    // This is how you test a List<string> as InlineData cannot do that
    public static IEnumerable<object[]> TestList()
    {
        yield return new object[] { new List<string>() { "", "", "" }, ",," };
        yield return new object[] { new List<string>() { "A", "B" }, "A,B" };
    }

    [Theory]
    [MemberData(nameof(TestList))]
    public void AndCalls_ToCsv_WithActualListThenReturnCsv(List<string> value, string expected)
    {
        Assert.Equal(expected, value.ToCsv());
    }
    #endregion

    #region ToSentenceCase Tests

    [Fact]
    public void AndCalls_ToSentenceCase_IsNullThenReturnEmpty()
    {
        string? value = null;
        Assert.Equal("", value.ToSentenceCase());
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", "")]
    [InlineData("test this sentence", "Test this sentence")]
    [InlineData(" test2", " Test2")]
    [InlineData("hello, how are you? i'm fine, you? i'm good. nice weather!", "Hello, how are you? I'm fine, you? I'm good. Nice weather!")]
    public void AndCalls_ToSentenceCase_WithActualValueThenSentenceCase(string value, string expected)
    {
        Assert.Equal(expected, value.ToSentenceCase());
    }
    #endregion

    #region MakeListfromSingleItem Tests

    [Fact]
    public void AndCalls_MakeListfromSingleItem_IsNullThenReturnEmptyList()
    {
        // arrange
        string? value = null;
        // act
        var resultList = value.MakeListfromSingleItem();
        // assert
        Assert.Empty(resultList);
    }

    [Fact]
    public void AndCalls_MakeListfromSingleItem_IsEmptyThenReturnEmptyList()
    {
        // arrange
        string? value = string.Empty;
        // act
        var resultList = value.MakeListfromSingleItem();
        // assert
        Assert.Empty(resultList);
    }

    [Fact]
    public void AndCalls_MakeListfromSingleItem_HasActualValueThenReturnList()
    {
        // arrange
        string? value = "Default Item";
        // act
        var resultList = value.MakeListfromSingleItem();
        // assert
        Assert.Single(resultList);
        Assert.Equal("Default Item", resultList[0]);
    }

    #endregion

    #region GetExtensionElseTxt Tests

    [Theory]
    [InlineData(null, "txt")]
    [InlineData("", "txt")]
    [InlineData("sample", "txt")]
    [InlineData("sample.", "txt")]
    [InlineData("sample.pdf", "pdf")]
    [InlineData("image sample.jpeg", "jpeg")]
    [InlineData("image sample2.jpg", "jpg")]
    [InlineData("image sample3.png", "png")]
    public void AndCalls_GetExtensionElseTxt_ThenReturnActualOrInnerDefaultExtension(string? value, string expected)
    {
        Assert.Equal(expected, value.GetExtensionElseTxt());
    }

    [Theory]
    [InlineData(null, "rtf", "rtf")]
    [InlineData("", "rtf", "rtf")]
    [InlineData("sample", "rtf", "rtf")]
    [InlineData("sample.", "rtf", "rtf")]
    [InlineData("sample.pdf", "rtf", "pdf")]
    [InlineData("image sample.jpeg", "rtf", "jpeg")]
    [InlineData("image sample2.jpg", "rtf", "jpg")]
    [InlineData("image sample3.png", "rtf", "png")]
    public void AndCalls_GetExtensionElseTxt_ThenReturnActualOrSpecifiedDefaultExtension(string? value, string defValue, string expected)
    {
        Assert.Equal(expected, value.GetExtensionElseTxt(defValue));
    }

    #endregion

    #region NormalizeLineEndings Tests     

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void AndCalls_NormalizeLineEndings_WithEmptyOrNullThenReturnEmptyString(string? value)
    {
        Assert.Equal(string.Empty, value.NormalizeLineEndings());
    }

    [Theory]
    [InlineData("This is a test. \\r\\n")]
    [InlineData("This is a test. \\n")]
    [InlineData("This is a test. \\r")]
    public void AndCalls_NormalizeLineEndings_ThenReturnNormailizedEndings(string value)
    {
        string expected = $"This is a test. {Environment.NewLine}";
        Assert.Equal(expected, value.NormalizeLineEndings());
    }
    #endregion

    #region RemoveExtraSpaces Tests
    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData("This is a test.", "This is a test.")]
    [InlineData("This  is  a  test.", "This is a test.")]
    [InlineData("This  is     a  test.", "This is a test.")]
    public void AndCalls_RemoveExtraSpaces_ThenReturnCleanedString(string? value, string expected)
    {
        Assert.Equal(expected, value.RemoveExtraSpaces());
    }

    #endregion

    #region WordCount Tests
    [Theory]
    [InlineData(null, 0)]
    [InlineData("", 0)]
    [InlineData("This is a test.", 4)]
    [InlineData("This  is  a  test of the word counting algorithm.", 9)]
    public void AndCalls_WordCount_ThenReturnCountOfWords(string? value, int expected)
    {
        Assert.Equal(expected, value.WordCount());
    }

    #endregion

    #region ChopString tests
    [Theory]
    [InlineData(null, 14, "")]
    [InlineData("", 14, "")]
    [InlineData("This is a", 14, "This is a")]
    [InlineData("This is a demo", 14, "This is a demo")]
    [InlineData("This is a demonstration", 14, "This is a demo")]
    public void AndCalls_ChopString_ThenReturnChoppedString(string? value, int maxNoOfChars, string expected)
    {
        Assert.Equal(expected, value.ChopString(maxNoOfChars));
    }
    #endregion

    #region GetFromMiddle tests
    [Theory]
    [InlineData(null, 14, "")]
    [InlineData("", 14, "")]
    [InlineData("FromBeginningWithAMiddleThatIsThereAndGetsToTheEnd", 20, "FromBeg...tsToTheEnd")]
    public void AndCalls_GetFromMiddleDefaults_ThenReturnChoppedString(string? value, int maxNoOfChars, string expected)
    {
        Assert.Equal(expected, value.GetFromMiddle(maxNoOfChars));
    }

    [Theory]
    [InlineData(null, 14, "", 5, "")]
    [InlineData("", 14, "", 5, "")]
    [InlineData("FromBeginningWithAMiddleThatIsThereAndGetsToTheEnd", 21, "....", 10, "FromBeg....tsToTheEnd")]
    public void AndCalls_GetFromMiddle_ThenReturnChoppedString(string? value, int maxNoOfChars, string fill, int endCharsToKeep, string expected)
    {
        Assert.Equal(expected, value.GetFromMiddle(maxNoOfChars, fill, endCharsToKeep));
    }
    #endregion

    #region GetFromBeginning tests
    [Theory]
    [InlineData(null, 14, "")]
    [InlineData("", 14, "")]
    [InlineData("FromBeginningWithAMiddleThatIsThereAndGetsToTheEnd", 16, "FromBeginning...")]
    public void AndCalls_GetFromBeginningDefaults_ThenReturnChoppedString(string? value, int maxNoOfChars, string expected)
    {
        Assert.Equal(expected, value.GetFromBeginning(maxNoOfChars));
    }

    [Theory]
    [InlineData(null, 14, "", "")]
    [InlineData("", 14, "", "")]
    [InlineData("FromBeginningWithAMiddleThatIsThereAndGetsToTheEnd", 17, "....", "FromBeginning....")]
    public void AndCalls_GetFromBeginning_ThenReturnChoppedString(string? value, int maxNoOfChars, string fill, string expected)
    {
        Assert.Equal(expected, value.GetFromBeginning(maxNoOfChars, fill));
    }
    #endregion

    #region GetFromEnd tests
    [Theory]
    [InlineData(null, 14, "")]
    [InlineData("", 14, "")]
    [InlineData("FromBeginningWithAMiddleThatIsThereAndGetsToTheEnd", 18, "...AndGetsToTheEnd")]
    public void AndCalls_GetFromEndDefaults_ThenReturnChoppedString(string? value, int maxNoOfChars, string expected)
    {
        Assert.Equal(expected, value.GetFromEnd(maxNoOfChars));
    }

    [Theory]
    [InlineData(null, 14, "", "")]
    [InlineData("", 14, "", "")]
    [InlineData("FromBeginningWithAMiddleThatIsThereAndGetsToTheEnd", 19, "....", "....AndGetsToTheEnd")]
    public void AndCalls_GetFromEnd_ThenReturnChoppedString(string? value, int maxNoOfChars, string fill, string expected)
    {
        Assert.Equal(expected, value.GetFromEnd(maxNoOfChars, fill));
    }
    #endregion

    #region BlankIfNullOrEmpty tests
    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData(" ", "")]
    [InlineData("abc", "abc")]
    public void AndCalls_BlankIfNullOrEmpty_ThenReturnBlankIfNullOrEmpty(string? value, string expected)
    {
        Assert.Equal(expected, value.BlankIfNullOrEmpty());
    }
    #endregion

    #region BlankIfNullOrWhiteSpace tests
    [Theory]
    [InlineData(null, "")]
    [InlineData("", "")]
    [InlineData(" ", "")]
    [InlineData("abc", "abc")]
    public void AndCalls_BlankIfNullOrWhiteSpace_ThenReturnBlankIfNullOrWhiteSpace(string? value, string expected)
    {
        Assert.Equal(expected, value.BlankIfNullOrWhiteSpace());
    }
    #endregion

    #region RemoveSuffixFromEnd tests
    [Theory]
    [InlineData(null, "pdf", "")]
    [InlineData("", "pdf", "")]
    [InlineData("test.txt", "pdf", "test.txt")]
    [InlineData("test.pdf", "pdf", "test.")]
    [InlineData("test.txt", ".pdf", "test.txt")]
    [InlineData("test.pdf", ".pdf", "test")]
    public void AndCalls_RemoveSuffixFromEnd_ThenRemoveSuffix(string? value, string suffix, string expected)
    {
        Assert.Equal(expected, value.RemoveSuffixFromEnd(suffix));
    }
    #endregion
}
