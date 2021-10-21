using AutoMapper;
using LibraryApi.Entieties;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("api/library")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService )
        {
            _libraryService = libraryService;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<LibraryDto>> Get()
        {
            var libraries = _libraryService.Get();
            return Ok(libraries);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<LibraryDto> Get([FromRoute]int id)
        {
            var library = _libraryService.Get(id);
            return Ok(library);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateLibraryDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            int libraryId = _libraryService.Create(dto, userId);
            return Created($"api/library/{libraryId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
             _libraryService.Delete(id, User);
            return NoContent();
        }
    }
}
