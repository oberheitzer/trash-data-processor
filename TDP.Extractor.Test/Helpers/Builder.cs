using TDP.Domain.Model;

namespace TDP.Extractor.Test;

internal static class Builder
{
    public static List<Area> BuildAreas()
        => [
            new Area
            {
                Id = 1,
                Name = "I",
                SettlementId = 1,
            },
            new Area
            {
                Id = 4,
                Name = "IV",
                SettlementId = 1,
            },
            new Area
            {
                Id = 13,
                Name = "XIII",
                SettlementId = 1,
            },
            new Area
            {
                Id = 17,
                Name = "Zártkert",
                SettlementId = 1,
            }
        ];
}
