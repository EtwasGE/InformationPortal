using System;
using Abp.Dependency;
using Nest;

namespace Portal.Core.ElasticSearch
{
    public class ElasticSearchConfiguration : ISingletonDependency
    {
        public ElasticSearchConfiguration()
        {
            var uri = new Uri("http://localhost:9200");
            var connectionSettings = new ConnectionSettings(uri)
                .DefaultIndex(DefaultIndexName);

            Client = new ElasticClient(connectionSettings);
        }

        public string DefaultIndexName => "application";
        public ElasticClient Client { get; }

        public void CreateIndex()
        {
            if (!Client.IndexExists(DefaultIndexName).Exists)
            {
                var response = Client.CreateIndex(DefaultIndexName, i => i
                    .Settings(s => s
                        .Analysis(Analysis))

                    .Mappings(m => m
                        .Map<BookIndexItem>(BookMap)));

                if (!response.IsValid)
                {
                    throw response.OriginalException;
                }
            }
        }

        public void DeleteIndex()
        {
            if (Client.IndexExists(DefaultIndexName).Exists)
            {
                var response = Client.DeleteIndex(DefaultIndexName);

                if (!response.IsValid)
                {
                    throw response.OriginalException;
                }
            }
        }

        private static AnalysisDescriptor Analysis(AnalysisDescriptor analysis)
            => analysis
                .Analyzers(a => a
                    .Custom("my-search-analyzer", ca => ca
                        .Tokenizer("standard")
                        .Filters("lowercase", "russian_morphology", "english_morphology", "my_stopwords")
                    ))
                .TokenFilters(tf => tf
                    .Stop("my_stopwords", s => s
                        .StopWords("а", "без", "более", "бы", "был", "была", "были", "было", "быть", "в", "вам", "вас",
                            "весь", "во", "вот", "все", "всего", "всех", "вы", "где", "да", "даже", "для", "до", "его",
                            "ее", "если", "есть", "еще", "же", "за", "здесь", "и", "из", "или", "им", "их", "к", "как",
                            "ко", "когда", "кто", "ли", "либо", "мне", "может", "мы", "на", "надо", "наш", "не", "него",
                            "нее", "нет", "ни", "них", "но", "ну", "о", "об", "однако", "он", "она", "они", "оно", "от",
                            "очень", "по", "под", "при", "с", "со", "так", "также", "такой", "там", "те", "тем", "то",
                            "того", "тоже", "той", "только", "том", "ты", "у", "уже", "хотя", "чего", "чей", "чем",
                            "что", "чтобы", "чье", "чья", "эта", "эти", "это", "я", "a", "an", "and", "are", "as", "at",
                            "be", "but", "by", "for", "if", "in", "into", "is", "it", "no", "not", "of", "on", "or",
                            "such", "that", "the", "their", "then", "there", "these", "they", "this", "to", "was",
                            "will", "with"
                        )));

        private static TypeMappingDescriptor<BookIndexItem> BookMap(TypeMappingDescriptor<BookIndexItem> map)
            => map
                .AutoMap()
                .Properties(p => p
                    .Text(t => t
                        .Name(n => n.Title)
                        .Boost(3)
                        .Analyzer("my-search-analyzer"))
                    .Text(t => t
                        .Name(n => n.Authors)
                        .Boost(3)
                        .Analyzer("my-search-analyzer"))
                    .Text(t => t
                        .Name(n => n.Description)
                        .Boost(2)
                        .Analyzer("my-search-analyzer"))
                    .Text(t => t
                        .Name(n => n.Tags)
                        .Boost(1)
                        .Analyzer("my-search-analyzer"))
                    .Text(t => t
                        .Name(n => n.DatePublication)
                        .Boost(0.5)
                        .Analyzer("my-search-analyzer"))
                    .Text(t => t
                        .Name(n => n.Issue)
                        .Boost(0.5)
                        .Analyzer("my-search-analyzer"))
                    .Text(t => t
                        .Name(n => n.Language)
                        .Boost(0.5)
                        .Analyzer("my-search-analyzer"))
                    .Text(t => t
                        .Name(n => n.Publisher)
                        .Boost(0.5)
                        .Analyzer("my-search-analyzer")));
    }
}