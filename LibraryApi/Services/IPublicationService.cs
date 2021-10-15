using LibraryApi.Models;
using System.Collections.Generic;

namespace LibraryApi.Services
{
    public interface IPublicationService
    {
        public int Create(int libraryId, CreatePublicationDto dto);
        PublicationDto Get(int libraryId, int publicationId);
        IEnumerable<PublicationDto> Get(int libraryId);
        void Delete(int libraryId, int publicationId);
    }
}
