using LibraryApi.Models;

namespace LibraryApi.Services
{
    public interface IPublicationService
    {
        public int Create(int libraryId, CreatePublicationDto dto);
    }
}
