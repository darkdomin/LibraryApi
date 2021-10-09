using AutoMapper;
using LibraryApi.Entieties;
using LibraryApi.Models;
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
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public LibraryController(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<LibraryDto>> Get()
        {
            var libraries = _dbContext
                            .Libraries
                            .Include(a=>a.Address)
                            .Include(p=>p.Publications)
                            .ToList();
            var librariesDto = _mapper.Map<List<LibraryDto>>(libraries);
            return Ok(librariesDto);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<LibraryDto>> Get([FromRoute]int id)
        {
            var library = _dbContext
                            .Libraries
                            .Include(a => a.Address)
                            .Include(p => p.Publications)
                            .FirstOrDefault(l=>l.Id == id);
            if(library == null)
            {
                NotFound();
            }
            var libraryDto = _mapper.Map<LibraryDto>(library);
            return Ok(libraryDto);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateLibraryDto dto)
        {
            var library = _mapper.Map<Library>(dto);
            _dbContext.Add(library);
            _dbContext.SaveChanges();
            return Created($"api/library/{library.Id}", null);
        }
    }
}
