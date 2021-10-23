using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    [Route("api/library/{libraryId}/publication")]
    [ApiController]
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationService _service;

        public PublicationController(IPublicationService publicationService)
        {
            _service = publicationService;
        }
        [HttpPost]
        public ActionResult Post([FromRoute] int libraryId, [FromBody] CreatePublicationDto dto)
        {
            int id = _service.Create(libraryId, dto);
            return Created($"api/library/{libraryId}/publication/{id}", null);
        }

        [HttpGet("{publicationId}")]
        public ActionResult Get([FromRoute] int libraryId, [FromRoute] int publicationId)
        {
            PublicationDto publication = _service.Get(libraryId, publicationId);
            return Ok(publication);
        }

        [HttpGet]
        public ActionResult<IEnumerable<PublicationDto>> Get([FromRoute] int libraryId,[FromQuery] PublicationQuery publicationQuery )
        {
            var publications = _service.Get(libraryId, publicationQuery);
            return Ok(publications);
        }

        [HttpDelete("{publicationId}")]
        public ActionResult Delete([FromRoute] int libraryId, [FromRoute] int publicationId)
        {
             _service.Delete(libraryId, publicationId);
            return NoContent();
        }
    }
}
