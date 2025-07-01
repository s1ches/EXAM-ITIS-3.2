using Text.API.Data;

namespace Text.API.Gql;

// ReSharper disable once ClassNeverInstantiated.Global
public class Query
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    // ReSharper disable once UnusedMember.Global
    public IEnumerable<Data.Entities.Text> GetTexts()
        => DataContext.Texts;
}