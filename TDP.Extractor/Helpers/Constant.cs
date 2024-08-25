namespace TDP.Extractor;

public static class Constant
{
    public static readonly string WasteCalendar = "hulladéknaptár";

    public static readonly string EmptyPlot = "üres";
    public static readonly string GardenPlot = "zártkert";
    public static readonly string HolidayHouse = "üdülő";

    public const string OrganicWaste = "Z";
    public const string RecyclableWaste = "SZ";
    public const string SolidWaste = "V";

    public const int FirstDayOfMonth = 1;
    public const int LastDayOfLongerMonth = 31;
    public const int LastDayOfShorterMonth = 31;
    public const int LastDayOfShortestMonthInLeapYear = 29;

    public const int IndexOfJanuary = 1;
    public const int IndexOfJuly = 4;

    public const int FirstPage = 1;
}
