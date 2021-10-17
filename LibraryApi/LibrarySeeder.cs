using LibraryApi.Entieties;
using System;
using System.Collections.Generic;
using System.Linq;

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
                    IEnumerable<Role> roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Libraries.Any())
                {
                    var libraries = GetLibraries();
                    _dbContext.Libraries.AddRange(libraries);
                    _dbContext.SaveChanges();
                }
            }
        }

        private static IEnumerable<Role> GetRoles()
        {
            return new List<Role>()
            {
                new Role
                {
                    Name = "Admin"
                },
                new Role
                {
                    Name = "User"
                }
            };
        }

        private static IEnumerable<Library> GetLibraries()
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
                },
                new Library
                {
                    Name = "Wojewódzka i Miejska Biblioteka Publiczna im. Josepha Conrada Korzeniowskiego w Gdańsku",
                    Email = "wbp@wbpg.org.pl",
                    Phone = "58 301 48 11",
                    Address = new Address
                    {
                        City = "Gdańsk ",
                        Street = "Targ Rakowy 5/6",
                        PostalCode = " 80-806"
                    },
                    Publications = new List<Publication>()
                    {
                        new Publication
                        {
                            Title = "Książęce skarby",
                            ReleaseDate = 2006,
                            Publisher = "Szkoła Podstawowa Nr 5",
                            Author = "Nitkowska-Węglarz, Jolanta",
                            Pages = 258,
                            Isbn = "83-913-129-6-8"
                        },
                        new Publication
                        {
                            Title = "Historia i kultura Ziemi Sławieńskiej. T. 14, Darłowo : migawki z historii miasta i okolic",
                            ReleaseDate = 2016,
                            Publisher = "Fundacja 'Dziedzictwo'",
                            Author = "Rączkowski, Włodzimierz",
                            Pages = 332,
                            Isbn = "9788395711527"
                        }
                    }
                }
            };
        }
    }
}
