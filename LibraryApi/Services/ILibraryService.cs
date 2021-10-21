using LibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public interface ILibraryService
    {
        public IEnumerable<LibraryDto> Get();
        public LibraryDto Get(int id);
        public int Create(CreateLibraryDto dto, int userId);
        public void Delete(int id, ClaimsPrincipal user);
    }
}
