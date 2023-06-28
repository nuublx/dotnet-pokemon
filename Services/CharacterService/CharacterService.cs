using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService;
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _Context;
        public CharacterService(IMapper mapper,DataContext context)
        {
            _mapper = mapper;
            _Context = context;
        }
        // Get All
        public async Task<ServiceResponse<List<CharacterResponseDto>>> GetAllCharactersAsync(int userId)
        {
            // Query Table
            var dbCharacters = await _Context.Characters.Where(ch => ch.User!.Id == userId).ToListAsync();
            // Map them to Response Dto
            var charactersResponse = dbCharacters.Select(c => _mapper.Map<CharacterResponseDto>(c)).ToList();
            return new ServiceResponse<List<CharacterResponseDto>> {
                Data = charactersResponse,
                Message = $"Number of Records: {charactersResponse.Count}",
                Success = true
            };
        }

        // Get Character By Id
        public async Task<ServiceResponse<CharacterResponseDto?>> GetCharacterByIdAsync(int Id)
        {
            Character dbCharacter;
            try {
                dbCharacter = await RetrieveCharacterAsync(Id);

            }catch (Exception ex)
            {
                return new ServiceResponse<CharacterResponseDto?>{
                    Message = ex.Message,
                    Success = false
                };
            }

            var characterResponse =  _mapper.Map<CharacterResponseDto>(dbCharacter);

            return new ServiceResponse<CharacterResponseDto?>
            {
                Data = characterResponse,
                Message = "Character is found successfully",
                Success = true
            };
        }

        // Add Character
        public async Task<ServiceResponse<CharacterResponseDto>> AddCharacterAsync(AddCharacterDto newCharacter, int userId)
        {
            var character = _mapper.Map<Character>(newCharacter);
            character.User = await _Context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            _Context.Characters.Add(character);
            await _Context.SaveChangesAsync();
            var characterResponse = _mapper.Map<CharacterResponseDto>(character);
            return new ServiceResponse<CharacterResponseDto>{
                Data = characterResponse,
                Message = "Character is added successfully",
                Success = true
            };
        }

        // Update Character By Id
        public async Task<ServiceResponse<CharacterResponseDto?>> UpdatedCharacterAsync(UpdateCharacterDto updatedCharacter, int Id)
        {
            Character dbCharacter;
            try {
                dbCharacter = await RetrieveCharacterAsync(Id);

            }catch (Exception ex)
            {
                return new ServiceResponse<CharacterResponseDto?>{
                    Message = ex.Message,
                    Success = false
                };
            }

            _mapper.Map(updatedCharacter, dbCharacter);

            await _Context.SaveChangesAsync();

            var responseCharacter = _mapper.Map<CharacterResponseDto>(dbCharacter);

            return new ServiceResponse<CharacterResponseDto?>
            {
                Data = responseCharacter,
                Success = true,
                Message = $"Character with Id '{Id}' is updated successfully"
            };
        }
        
        // Delete Character By Id
        public async Task<ServiceResponse<CharacterResponseDto?>> DeleteCharacterAsync(int Id)
        {    
            Character dbCharacter;
            try {
                dbCharacter = await RetrieveCharacterAsync(Id);

            }catch (Exception ex)
            {
                return new ServiceResponse<CharacterResponseDto?>{
                    Message = ex.Message,
                    Success = false
                };
            }

            var responseCharacter = _mapper.Map<CharacterResponseDto>(dbCharacter);
                
            _Context.Characters.Remove(dbCharacter);

            await _Context.SaveChangesAsync();

            return new ServiceResponse<CharacterResponseDto?>
            {
                Data = responseCharacter,
                Success = true,
                Message = $"Character with Id '{Id}' is deleted successfully"
            };

        }
        private async Task<Character> RetrieveCharacterAsync(int Id) {

            var dbCharacter = await _Context.Characters.FirstOrDefaultAsync(c => c.Id == Id) ?? throw new Exception($"Character with ID '{Id}' is not found.");

            return dbCharacter;
        }
}