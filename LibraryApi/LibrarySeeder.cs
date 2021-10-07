using LibraryApi.Entieties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi
{
    public class LibrarySeeder
    {
        private readonly LibraryDbContext _dbContext;

        public LibrarySeeder(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Libraries.Any())
                {
                    var libraries = GetLibraries();
                    _dbContext.Libraries.AddRange(libraries);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Library> GetLibraries()
        {
            return new List<Library>()
            {
                new Library
                {
                    Name = "Wojewodzka Biblioteka Publiczna im. Hieronima Lopacinskiego w Lublinie",
                    Email = "info@poczta.wbp.lublin.pl",
                    Phone = "81 528 74 00",
                    Address = new Address
                    {
                        City = "Lublin",
                        Street = "Narutowicza 4",
                        PostalCode = "20-950"
                    },
                    Publications = new List<Publication>()
                    {
                        new Publication
                        {
                            Title = "Modelowanie Danych",
                            ReleaseDate = 2006,
                            Publisher = "Helion",
                            Author = "Sharon Allen",
                            Pages = 558,
                            Isbn = "83-246-0184-8"
                        },
                        new Publication
                        {
                            Title = "C# 6.0. Kompletny przewodnik dla praktyków. Wydanie V",
                            ReleaseDate = 2016,
                            Publisher = "Helion",
                            Author = "Mark Michaelis, Eric Lippert",
                            Pages = 856,
                            Isbn = " 978-83-283-2518-0"
                        }
                    }
                }
            };
        }
    }
}
