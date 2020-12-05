//#pragma warning disable S1172 // Unused method parameters should be removed
//#nullable enable
namespace LearningTests.Example
{
    // TODO can't get record running ???
    //public 
    // C# 9.0 new record
    ////public record HeightRecord
    ////{
    ////    ////private HeightRecord(float value, string unit)
    ////    ////{
    ////    ////    this.Unit = unit;
    ////    ////    this.Value = value;
    ////    ////}

    ////    ////public float Value { get; init; }
    ////    ////public string Unit { get; init; }

    ////    //public static HeightRecord TryParse(string height) =>
    ////    //    int.TryParse(height[..^2], out var value)
    ////    //        ? new HeightRecord(value, height[^2..])
    ////    //        : null;
    ////}

    public class ValidateHeightRecordTests
    {
        ////[Theory]
        ////[InlineData("mio km", false)]
        ////[InlineData("100 km", false)]
        ////[InlineData("100 cm", false)]
        ////[InlineData("160 cm", true)]
        ////[InlineData("50 in", false)]
        ////[InlineData("160 in", true)]
        ////public void Test_ValidateHeightV2(string heightString, bool expected)
        ////{
        ////    var cut = HeightRecord.TryParse(heightString);
        ////    var actual = ValidateHeight(cut);

        ////    Assert.Equal(expected, actual);
        ////}

        ////private static bool ValidateHeight(HeightRecord heightType) =>
        ////    (heightType?.Unit, heightType?.Value) switch
        ////    {
        ////        ("cm", >= 150 and <= 193) => true,
        ////        ("in", >= 59 and <= 76) => true,
        ////        _ => false
        ////    };

        ////private HeightRecord Create(string heightString)
        ////{

        ////}
    }
}