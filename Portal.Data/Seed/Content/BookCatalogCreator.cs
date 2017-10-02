using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Portal.Core.Content.Entities;

namespace Portal.Data.Seed.Content
{
    public class BookCatalogCreator
    {
        private readonly PortalDbContext _context;

        public BookCatalogCreator(PortalDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            var catalogs = _context.Set<BookCatalog>();

            var programming = new BookCatalog
            {
                Name = "Programming",
                Order = 1,
                Childrens = new List<BookCatalog>
                {
                    new BookCatalog {Name = ".NET", Order = 1},
                    new BookCatalog {Name = "Python", Order = 3},
                    new BookCatalog {Name = "JavaScript", Order = 2}
                }
            };

            var webDesign = new BookCatalog
            {
                Name = "Web Design",
                Order = 2,
                Childrens = new List<BookCatalog>
                {
                    new BookCatalog {Name = "HTML", Order = 1},
                    new BookCatalog {Name = "CSS", Order = 2}
                }
            };

            var other = new BookCatalog
            {
                Name = "Other",
                Order = 3
            };

            catalogs.AddOrUpdate(x => x.Name, programming, webDesign, other);
            _context.SaveChanges();
        }
    }
}