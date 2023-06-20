using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace dotnet_rpg.Services.CharacterService;
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
        // Get All
        public async Task<ServiceResponse<List<CharacterResponseDto>>> GetAllCharactersAsync()
        {
            var charactersResponse = characters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            return new ServiceResponse<List<CharacterResponseDto>> {
                Data = charactersResponse,
                Message = $"Number of Records: {characters.Count}",
                Success = true
            };
        }

        // Get Character By Id
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

        // Add Character
        public async Task<ServiceResponse<CharacterResponseDto>> AddCharacterAsync(AddCharacterDto newCharacter)
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

        // Update Character By Id
        public async Task<ServiceResponse<CharacterResponseDto?>> UpdatedCharacterAsync(UpdateCharacterDto updatedCharacter, Guid Id)
        {
            var character = characters.FirstOrDefault(c => c.Id == Id);
            try {
                if (character is null)
                    throw new Exception($"The character with ID '{Id}' is not found.");
                
                _mapper.Map(updatedCharacter, character);

                var responseCharacter = _mapper.Map<CharacterResponseDto>(character);

                return new ServiceResponse<CharacterResponseDto?>
                {
                    Data = responseCharacter,
                    Success = true,
                    Message = $"Character with Id '{Id}' is updated successfully"
                };
            }catch (Exception ex)
            {
                return new ServiceResponse<CharacterResponseDto?>{
                    Message = ex.Message,
                    Success = false
                };
            }
        }
        
        // Delete Character By Id
        public async Task<ServiceResponse<CharacterResponseDto?>> DeleteCharacterAsync(Guid id)
        {    
            var character = characters.FirstOrDefault(c => c.Id == id);
            try {
                if (character is null)
                    throw new Exception($"The character with ID '{id}' is not found.");
                

                var responseCharacter = _mapper.Map<CharacterResponseDto>(character);

                characters.Remove(character);

                return new ServiceResponse<CharacterResponseDto?>
                {
                    Data = responseCharacter,
                    Success = true,
                    Message = $"Character with Id '{id}' is deleted successfully"
                };
            }catch (Exception ex)
            {
                return new ServiceResponse<CharacterResponseDto?>{
                    Message = ex.Message,
                    Success = false
                };
            }
        }
    }