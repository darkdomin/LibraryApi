using AutoMapper;
using LibraryApi.Entieties;
using LibraryApi.Exceptions;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;
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

        public PublicationService(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(int libraryId, CreatePublicationDto dto)
        {
            GetPublicationById(libraryId);
            var publication = _mapper.Map<Publication>(dto);

            publication.LibraryId = libraryId;
            _dbContext.Publications.Add(publication);
            _dbContext.SaveChanges();
            return publication.Id;
        }

        public void Delete(int libraryId, int publicationId)
        {
            Library library = GetPublicationById(libraryId);

            var publication = library.Publications.FirstOrDefault(p => p.Id == publicationId);
            if (publication is null)
            {
                throw new NotFoundException("publication not found");
            }
            _dbContext.Publications.Remove(publication);
            _dbContext.SaveChanges();
        }

        public PublicationDto Get(int libraryId, int publicationId)
        {
            Library library = GetPublicationById(libraryId);

            var publication = library.Publications.FirstOrDefault(p => p.Id == publicationId);

            if (publication == null || publication.LibraryId != libraryId)
            {
                throw new NotFoundException("publication not found");
            }
            return _mapper.Map<PublicationDto>(publication);
        }

        public Pagedresult<PublicationDto> Get(int libraryId, PublicationQuery query)
        {
            Library library = GetPublicationById(libraryId);
            var allSortedPublication = library.Publications
                                  .Where(p => query.SearchPhrase == null ||
                                  (p.Title.ToLower().Contains(query.SearchPhrase.ToLower()) ||
                                   p.Author.Contains(query.SearchPhrase)));

            var  sortedPublications = allSortedPublication.Skip((query.PageNumber - 1) * query.PageSize)
                                  .Take(query.PageSize);
            var totalCount = allSortedPublication.Count();

            var publicationDtos = _mapper.Map<IEnumerable<PublicationDto>>(sortedPublications).ToList();
            var resulte = new Pagedresult<PublicationDto>(publicationDtos, totalCount, query.PageSize, query.PageNumber);
            return resulte;
        }

        private Library GetPublicationById(int libraryId)
        {
            var library = _dbContext
                         .Libraries
                         .Include(p => p.Publications)
                         .FirstOrDefault(l => l.Id == libraryId);
            if (library == null)
            {
                throw new NotFoundException("Library not found");
            }

            return library;
        }
    }
}
