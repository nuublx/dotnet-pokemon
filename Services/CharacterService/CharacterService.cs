using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static readonly List<Character> characters = new() {
            new Character(),
            new Character {Name = "Sam"}
        };

        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<CharacterResponseDto>> AddCharacterAsync(CharacterRequestDto newCharacter)
        {
            var character = _mapper.Map<Character>(newCharacter);
            
            characters.Add(character);
            
            var characterResponse = _mapper.Map<CharacterResponseDto>(character);

            return new ServiceResponse<CharacterResponseDto>{
                Data = characterResponse,
                Message = "Character is added successfully",
                Success = true
            };
        }
        public async Task<ServiceResponse<List<CharacterResponseDto>>> GetAllCharactersAsync()
        {
            var charactersResponse = characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            return new ServiceResponse<List<CharacterResponseDto>> {
                Data = charactersResponse,
                Message = $"Number of Records: {characters.Count}",
                Success = true
            };
        }

        public async Task<ServiceResponse<CharacterResponseDto?>> GetCharacterByIdAsync(Guid Id)
        {
            var characterResponse =  _mapper.Map<CharacterResponseDto>(characters.FirstOrDefault(c => c.Id == Id));
            if (characterResponse != null)
                return new ServiceResponse<CharacterResponseDto?>{
                    Data = characterResponse,
                    Message = "Character is found successfully",
                    Success = true
                };
            else 
                return new ServiceResponse<CharacterResponseDto?>{
                    Data = null,
                    Message = "Character is not found!",
                    Success = false
                };
        }
    }
}