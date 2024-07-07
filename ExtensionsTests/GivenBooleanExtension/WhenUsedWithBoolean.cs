using ConsoleAppExtensions.Extensions;

namespace ExtensionsTests.GivenBooleanExtension;

public class WhenUsedWithBoolean
{
    #region ToYesNo Tests
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void AndCalls_ToYesNo_UsingInternalDefaultValue(bool value)
    {
        if (value)
        {
            Assert.Equal("Yes", value.ToYesNo());
        }
        else
        {
            Assert.Equal("No", value.ToYesNo());
        }
    }

    [Theory]
    [InlineData(true, "Positive", "Negative")]
    [InlineData(true, "Ok", "Cancel")]
    [InlineData(false, "Positive", "Negative")]
    [InlineData(false, "Ok", "Cancel")]
    public void AndCalls_ToYesNo_UsingSpecifiedDefaultValue(bool value, string yesValue, string noValue)
    {
        if (value)
        {
            Assert.Equal(yesValue, value.ToYesNo(yesValue, noValue));
        }
        else
        {
            Assert.Equal(noValue, value.ToYesNo(yesValue, noValue));
        }
    }
    #endregion
}
