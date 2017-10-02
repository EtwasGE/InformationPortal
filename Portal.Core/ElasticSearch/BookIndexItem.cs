using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using Portal.Core.Content.Entities;

namespace Portal.Core.ElasticSearch
{
    [AutoMapFrom(typeof(Book))]
    public class BookIndexItem : EntityDto, IPassivable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Issue { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string DatePublication { get; set; }

        public string Picture { get; set; }

        //public IList<string> Authors { get; set; }
        public string Authors { get; set; }
        public IList<string> Tags { get; set; }

        public bool IsRecommend { get; set; }
        public bool IsActive { get; set; }
    }
}
