using System;
using System.Linq.Expressions;
using Abp.Specifications;
using Portal.Core.Content.Entities.Common;

namespace Portal.Core.Specifications
{
    public class DeletedSpecif<TContent> : Specification<TContent>
        where TContent : ContentEntityBase
    {
        public override Expression<Func<TContent, bool>> ToExpression()
        {
            return content => content.IsDeleted;
        }
    }
}
