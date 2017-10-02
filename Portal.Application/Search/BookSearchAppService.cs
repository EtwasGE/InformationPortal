using Elasticsearch.Net;
using Nest;
using Portal.Core.ElasticSearch;

namespace Portal.Application.Search
{
    public class BookSearchAppService : SearchAppServiceBase<BookIndexItem>, ISearchAppService<BookIndexItem>
    {
        public BookSearchAppService(ElasticSearchConfiguration config)
            : base(config)
        {
        }

        protected override HighlightDescriptor<BookIndexItem> Highlight(
            HighlightDescriptor<BookIndexItem> highlight)
            => highlight
                .PreTags(PreHighlightTag)
                .PostTags(PostHighlightTag)
                .Fields(
                    fs => fs.Field(f => f.Title),
                    fs => fs.Field(f => f.Description),
                    fs => fs.Field(f => f.Authors),
                    fs => fs.Field(f => f.Tags),
                    fs => fs.Field(f => f.DatePublication),
                    fs => fs.Field(f => f.Issue),
                    fs => fs.Field(f => f.Publisher),
                    fs => fs.Field(f => f.Language));

        protected override FieldsDescriptor<BookIndexItem> Fields(
            FieldsDescriptor<BookIndexItem> fields)
            => fields
                .Field(f => f.Title)
                .Field(f => f.Description)
                .Field(f => f.Authors)
                .Field(f => f.Tags)
                .Field(f => f.DatePublication)
                .Field(f => f.Issue)
                .Field(f => f.Publisher)
                .Field(f => f.Language);

        protected override SuggestContainerDescriptor<BookIndexItem> Suggest(
            SuggestContainerDescriptor<BookIndexItem> suggest, Dto.SearchInput input)
            => suggest
                .Term("authors-suggest", t => t
                    //.MaxEdits(1)
                    //.MaxInspections(2)
                    //.MaxTermFrequency(3)
                    //.MinDocFrequency(4)
                    //.MinWordLength(5)
                    //.PrefixLength(6)
                    //.ShardSize(7)
                    //.Size(8)
                    .Analyzer("my-search-analyzer")
                    .SuggestMode(SuggestMode.Always)
                    .Text(input.Query)
                    .Field(f => f.Authors))
                .Term("title-suggest", t => t
                    .Analyzer("my-search-analyzer")
                    .SuggestMode(SuggestMode.Always)
                    .Text(input.Query)
                    .Field(f => f.Title));
    }
}
