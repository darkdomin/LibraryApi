using AutoMapper;
using LibraryApi.Entieties;
using LibraryApi.Exceptions;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public LibraryService(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(CreateLibraryDto dto)
        {
            var library = _mapper.Map<Library>(dto);
            _dbContext.Add(library);
            _dbContext.SaveChanges();
            return library.Id;
        }

        public void Delete(int id)
        {
            var library = _dbContext.Libraries.FirstOrDefault(l => l.Id == id);
            if(library == null)
            {
                throw new NotFoundException("Library not found");
            }
            _dbContext.Libraries.Remove(library);
            _dbContext.SaveChanges();
        }

        public IEnumerable<LibraryDto> Get()
        {
            var libraries = _dbContext
                            .Libraries
                            .Include(a => a.Address)
                            .Include(p => p.Publications)
                            .ToList();
            var result = _mapper.Map<List<LibraryDto>>(libraries);
            return result;
        }
        public LibraryDto Get(int id)
        {
            var library = _dbContext
                .Libraries
                .Include(a => a.Address)
                .Include(p => p.Publications)
                .FirstOrDefault(l => l.Id == id);
            if (library == null)
            {
                throw new NotFoundException("Library not found");
            }
            var result = _mapper.Map<LibraryDto>(library);
            return result;
        }
    }
}
