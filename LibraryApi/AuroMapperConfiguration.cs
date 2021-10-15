using AutoMapper;
using LibraryApi.Entieties;
using LibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi
{
    public class AuroMapperConfiguration : Profile
    {
        public AuroMapperConfiguration()
        {
            CreateMap<Library, LibraryDto>()
                .ForMember(cfg => cfg.City, l => l.MapFrom(m => m.Address.City))
                .ForMember(cfg => cfg.Street, l => l.MapFrom(m => m.Address.Street))
                .ForMember(cfg => cfg.PostalCode, l => l.MapFrom(m => m.Address.PostalCode));

            CreateMap<Publication, PublicationDto>();

            CreateMap<CreateLibraryDto, Library>()
                .ForMember(c => c.Address, l => l.MapFrom(dto => new Address()
                { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));

            CreateMap<CreatePublicationDto, Publication>();
              
        }
    }
}
