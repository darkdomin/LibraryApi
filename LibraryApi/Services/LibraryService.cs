using AutoMapper;
using LibraryApi.Authorization;
using LibraryApi.Entieties;
using LibraryApi.Exceptions;
using LibraryApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public LibraryService(LibraryDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public int Create(CreateLibraryDto dto, int userId)
        {
            var library = _mapper.Map<Library>(dto);
            library.CreatedById = userId;
            _dbContext.Add(library);
            _dbContext.SaveChanges();
            return library.Id;
        }

        public void Delete(int id, ClaimsPrincipal user)
        {
            var library = _dbContext.Libraries.FirstOrDefault(l => l.Id == id);
            if(library == null)
            {
                throw new NotFoundException("Library not found");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(user, library,
                new ResourceOperationRequirement(ResourceOperation.DELETE)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
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
