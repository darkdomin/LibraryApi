using LibraryApi.Entieties;
using Microsoft.AspNetCore.Mvc;
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

        public LibraryController(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Library>> Get()
        {
            var libraries = _dbContext
                            .Libraries
                            .ToList();
            return Ok(libraries);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Library>> Get([FromRoute]int id)
        {
            var library = _dbContext
                            .Libraries
                            .FirstOrDefault(l=>l.Id == id);
            if(library == null)
            {
                NotFound();
            }
            return Ok(library);
        }
    }
}
