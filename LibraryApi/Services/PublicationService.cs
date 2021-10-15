using AutoMapper;
using LibraryApi.Entieties;
using LibraryApi.Exceptions;
using LibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public PublicationService(LibraryDbContext dbContext, IMapper mapper )
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int libraryId, CreatePublicationDto dto)
        {
            var library = _dbContext.Libraries.FirstOrDefault(l => l.Id == libraryId);
            if (library == null)
            {
                throw new NotFoundException("Library not found");
            }
            var publication = _mapper.Map <Publication>(dto);

            publication.LibraryId = libraryId;
            _dbContext.Publications.Add(publication);
            _dbContext.SaveChanges();
            return publication.Id;
        }
    }
}
