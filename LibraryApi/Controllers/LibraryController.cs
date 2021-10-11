using AutoMapper;
using LibraryApi.Entieties;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("api/library")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService )
        {
            _libraryService = libraryService;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<LibraryDto>> Get()
        {
            var libraries = _libraryService.Get();
            return Ok(libraries);
        }

        [HttpGet("{id}")]
        public ActionResult<LibraryDto> Get([FromRoute]int id)
        {
            var library = _libraryService.Get(id);
            if (library == null)
            {
                return NotFound();
            }
            return Ok(library);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateLibraryDto dto)
        {
            int libraryId = _libraryService.Create(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Created($"api/library/{libraryId}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            bool isDeleted = _libraryService.Delete(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
