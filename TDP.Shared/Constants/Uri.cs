﻿namespace TDP.Shared.Constants;

public static class Uri
{
    #region Area IV
    public static readonly string GardonyArea4 = "/1319/Gárdony_IV..pdf";
    public static readonly string GardonyArea4EmptyPlot = "/1516/Gárdony-IV_üres-telek.pdf";
    public static readonly string GardonyArea4HolidayHouse = "/1383/Gárdony_IV_üdülő.pdf";
    #endregion

    #region Area XV
    public static readonly string GardonyArea15 = "/1319/Gárdony_XV..pdf";
    public static readonly string GardonyArea15EmptyPlot = "/1516/Gárdony-XV_üres-telek.pdf";
    public static readonly string GardonyArea15HolidayHouse = "/1383/Gárdony_XV_üdülő.pdf";
    #endregion

    #region Area XVI
    public static readonly string GardonyArea16 = "/1319/Gárdony_XVI..pdf";
    public static readonly string GardonyArea16EmptyPlot = "/1516/Gárdony-XVI_üres-telek.pdf";
    public static readonly string GardonyArea16HolidayHouse = "/1383/Gárdony_XVI_üdülő.pdf";
    #endregion

    public static readonly Dictionary<string, string> Areas = new()
    {
        { File.GardonyXVI, GardonyArea16 },
        { File.GardonyXVIEP, GardonyArea16EmptyPlot },
        { File.GardonyXVIHH, GardonyArea16HolidayHouse },
    };
}
