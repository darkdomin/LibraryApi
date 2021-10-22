using LibraryApi.Models;
using System.Collections.Generic;

namespace LibraryApi.Services
{
    public interface ILibraryService
    {
        public IEnumerable<LibraryDto> Get();
        public LibraryDto Get(int id);
        public int Create(CreateLibraryDto dto);
        public void Delete(int id);
    }
}
