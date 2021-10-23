using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            int libraryId = _libraryService.Create(dto);
            return Created($"api/library/{libraryId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
             _libraryService.Delete(id);
            return NoContent();
        }
    }
}
