using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Nest;
using Portal.Core.ElasticSearch;

namespace Portal.Application.Search
{
    public abstract class SearchAppServiceBase<TContentIndexItem> : AppServiceBase
        where TContentIndexItem : EntityDto, IPassivable
    {
        protected SearchAppServiceBase(ElasticSearchConfiguration config)
        {
            Client = config.Client;
	        CurrentIndexName = config.DefaultIndexName;
        }

        protected ElasticClient Client { get; }
        protected string CurrentIndexName { get; }

        protected string PreHighlightTag => @"<strong>";
        protected string PostHighlightTag => @"</strong>";

        public async Task<ISearchResponse<TContentIndexItem>> SearchAsync(Dto.SearchInput input)
        {
            return await Client
                .SearchAsync<TContentIndexItem>(s => s
                    .Index(CurrentIndexName)
                    .From(input.PageIndex * input.PageSize)
                    .Size(input.PageSize)
                    .PostFilter(filter => filter
                        .Term(field => field.IsActive, true))
                    .Highlight(Highlight)
                    .Query(q => QueryContainer(q, input))
                    .Suggest(sg => Suggest(sg, input)));
        }

        private QueryContainer QueryContainer(
            QueryContainerDescriptor<TContentIndexItem> query, Dto.SearchInput input)
            => query
                .MultiMatch(qs => qs
                    .Analyzer("my-search-analyzer")
                    .Fields(Fields)
                    .Query(input.Query));

        protected abstract HighlightDescriptor<TContentIndexItem> Highlight(
            HighlightDescriptor<TContentIndexItem> highlight);

        protected abstract FieldsDescriptor<TContentIndexItem> Fields(
            FieldsDescriptor<TContentIndexItem> fields);

        protected abstract SuggestContainerDescriptor<TContentIndexItem> Suggest(
            SuggestContainerDescriptor<TContentIndexItem> suggest, Dto.SearchInput input);
    }
}
