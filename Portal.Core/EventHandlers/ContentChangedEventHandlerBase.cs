using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Events.Bus.Entities;
using Abp.Events.Bus.Handlers;
using Nest;
using Portal.Core.Content.Entities.Common;
using Portal.Core.ElasticSearch;

namespace Portal.Core.EventHandlers
{
    public abstract class ContentChangedEventHandlerBase<TContent, TIndexer> 
        : IEventHandler<EntityCreatedEventData<TContent>>,
        IEventHandler<EntityUpdatedEventData<TContent>>,
        IEventHandler<EntityDeletedEventData<TContent>>,
        ITransientDependency
        where TContent: ContentEntityBase
        where TIndexer : BookIndexItem
    {
        protected ContentChangedEventHandlerBase(ElasticSearchConfiguration config)
        {
            CurrentIndexName = config.DefaultIndexName;
            Client = config.Client;
        }

        protected string CurrentIndexName { get; }
        protected ElasticClient Client { get; }

        protected TIndexer GetDocument(EntityEventData<TContent> eventData)
        {
            return eventData.Entity.MapTo<TIndexer>();
        }

        public void HandleEvent(EntityCreatedEventData<TContent> eventData)
        {
            var document = GetDocument(eventData);
            Client.Index(document, x => x.Index(CurrentIndexName));
        }

        public void HandleEvent(EntityUpdatedEventData<TContent> eventData)
        {
            var document = GetDocument(eventData);
            Client.Update<TIndexer>(document, x => x
                .Doc(document)
                .Index(CurrentIndexName));
        }

        public void HandleEvent(EntityDeletedEventData<TContent> eventData)
        {
            var document = GetDocument(eventData);
            Client.Update<TIndexer>(document, x => x
                .Doc(document)
                .Index(CurrentIndexName));
            //Client.Delete<TIndexer>(document, x => x.Doc(document));
        }
    }
}
