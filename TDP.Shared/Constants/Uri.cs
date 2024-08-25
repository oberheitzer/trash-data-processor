namespace TDP.Shared.Constants;

public static class Uri
{
    #region Area IV
    public static readonly string GardonyArea4 = "/1307/Gárdony_IV..pdf";
    public static readonly string GardonyArea4EmptyPlot = "/1504/Gárdony-IV_üres-telek.pdf";
    public static readonly string GardonyArea4HolidayHouse = "/1371/Gárdony_IV_üdülő.pdf";
    #endregion

    #region Area XV
    public static readonly string GardonyArea15 = "/1318/Gárdony_XV..pdf";
    public static readonly string GardonyArea15EmptyPlot = "/1515/Gárdony-XV_üres-telek.pdf";
    public static readonly string GardonyArea15HolidayHouse = "/1382/Gárdony_XV_üdülő.pdf";
    #endregion

    #region Area XVI
    public static readonly string GardonyArea16 = "/1319/Gárdony_XVI..pdf";
    public static readonly string GardonyArea16EmptyPlot = "/1516/Gárdony-XVI_üres-telek.pdf";
    public static readonly string GardonyArea16HolidayHouse = "/1383/Gárdony_XVI_üdülő.pdf";
    #endregion

    public static readonly Dictionary<string, string> Areas = new()
    {
        { File.GardonyIV, GardonyArea4 },
        { File.GardonyIVEP, GardonyArea4EmptyPlot },
        { File.GardonyIVHH, GardonyArea4HolidayHouse },
        { File.GardonyXV, GardonyArea15 },
        { File.GardonyXVEP, GardonyArea15EmptyPlot },
        { File.GardonyXVHH, GardonyArea15HolidayHouse },
        { File.GardonyXVI, GardonyArea16 },
        { File.GardonyXVIEP, GardonyArea16EmptyPlot },
        { File.GardonyXVIHH, GardonyArea16HolidayHouse },
    };
}
