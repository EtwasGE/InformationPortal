using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Portal.Core.Content.Entities;

namespace Portal.Data.Seed.Content
{
    public class BookCreator
    {
        private readonly PortalDbContext _context;

        public BookCreator(PortalDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            #region Authors

            var authors = _context.Set<Author>();

            var author1 = new Author { Name = "AuthorName#1" };
            var author2 = new Author { Name = "AuthorName#2" };
            var author3 = new Author { Name = "AuthorName#3" };

            authors.AddOrUpdate(x => x.Name, author1, author2, author3);

            #endregion

            #region Tags

            var tags = _context.Set<Tag>();

            var tag1 = new Tag { Name = "TagName#1" };
            var tag2 = new Tag { Name = "TagName#2" };
            var tag3 = new Tag { Name = "TagName#3" };
            var tag4 = new Tag { Name = "TagName#4" };

            tags.AddOrUpdate(x => x.Name, tag1, tag2, tag3, tag4);

            #endregion

            #region Language

            var languages = _context.Set<Language>();
            var language = new Language { Name = "LanguageName" };

            languages.AddOrUpdate(x => x.Name, language);

            #endregion

            #region Publisher

            var publishers = _context.Set<Publisher>();
            var publisher = new Publisher { Name = "PublisherName" };

            publishers.AddOrUpdate(x => x.Name, publisher);

            #endregion

            #region Issue

            var issues = _context.Set<Issue>();
            var issue = new Issue { Name = "IssueName" };

            issues.AddOrUpdate(x => x.Name, issue);

            #endregion

            AddBooks(
                catalogName: ".NET",
                size: 15,
                authors: new List<Author> { author1, author3 },
                tags: new List<Tag>(),
                language: null,
                publisher: publisher,
                issue: issue
            );

            AddBooks(
                catalogName: "Python",
                size: 5,
                authors: new List<Author>(),
                tags: new List<Tag> { tag1, tag4 },
                language: language,
                publisher: null,
                issue: issue
            );

            AddBooks(
                catalogName: "HTML",
                size: 12,
                authors: new List<Author> { author2, author1 },
                tags: new List<Tag> { tag3, tag2, tag1 },
                language: language,
                publisher: publisher,
                issue: null
            );

            AddBooks(
                catalogName: "Other",
                size: 2,
                authors: new List<Author> { author3 },
                tags: new List<Tag> { tag3, tag2, tag1, tag4 },
                language: language,
                publisher: publisher,
                issue: issue
            );

            _context.SaveChanges();
        }

        private void AddBooks(string catalogName, int size, IList<Author> authors,
            IList<Tag> tags, Language language, Publisher publisher, Issue issue)
        {
            var books = _context.Set<Book>();
            var catalog = _context.Set<BookCatalog>().Single(x => x.Name == catalogName);

            for (var i = 1; i <= size; i++)
            {
                var netBook = new Book
                {
                    Title = $"Book Title ({catalogName}) #" + i,
                    Catalog = catalog,
                    FilePath = "defaultBookFile.pdf",
                    Authors = authors,
                    Tags = tags,
                    Language = language,
                    Publisher = publisher,
                    Issue = issue,
                    DatePublication = DateTime.Now,
                    Description =
                    "Dig deep and master the intricacies of the common language runtime, " +
                    "C#, and .NET development. Led by programming expert Jeffrey Richter, a " +
                    "longtime consultant to the Microsoft .NET team - you’ll gain pragmatic insights for " +
                    "building robust, reliable, and responsive apps and components.",
                    IsApproved = true
                };

                books.AddOrUpdate(x => x.Title, netBook);
            }
        }
    }
}