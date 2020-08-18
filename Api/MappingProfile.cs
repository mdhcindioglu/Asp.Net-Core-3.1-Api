using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace AddressPersonServer
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>();
            CreateMap<Person, PersonForCreationDto>();
            CreateMap<PersonForCreationDto, Person>();
            CreateMap<PersonForUpdateDto, Person>();
            
            CreateMap<Address, AddressDto>();
            CreateMap<Address, AddressForCreationDto>();
            CreateMap<AddressForCreationDto, Address>();
            CreateMap<AddressForUpdateDto, Address>();
        }
    }
}
