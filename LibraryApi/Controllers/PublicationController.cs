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
        public ActionResult Post([FromRoute] int libraryId,[FromBody] CreatePublicationDto dto)
        {
            int id = _service.Create(libraryId, dto);
            return Created($"api/{libraryId}/publication/{id}", null);
        }
    }
}
