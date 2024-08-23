using CsvHelper.Configuration;
using TDP.Domain.Model;

namespace TDP.Extractor.Mappers;

public sealed class CollectionMapper : ClassMap<Collection>
{
    public CollectionMapper()
    {
        Map(collection => collection.Id).Name("id");
        Map(collection => collection.Date).Name("date");
        Map(collection => collection.Waste).Name("waste").Convert(c => ((int)c.Value.Waste).ToString());
        Map(collection => collection.Property).Name("property").Convert(c => ((int)c.Value.Property).ToString());
        Map(collection => collection.AreaId).Name("area_id");
        ReferenceMaps.Remove(ReferenceMaps.Find<Collection>(m => m.Area)!);
    }
}
