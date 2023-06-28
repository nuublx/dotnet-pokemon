using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.AutoMapperprofiles
{
    public class CharacterMapperProfiles : Profile
    {
        public CharacterMapperProfiles()
        {
            CreateMap<Character, CharacterResponseDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
        }
    }
}