using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.AutoMapperProfiles
{
    public class CharacterMapperProfile : Profile
    {
        
        public CharacterMapperProfile()
        {
            CreateMap<Character, CharacterResponseDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
        }
    }
}