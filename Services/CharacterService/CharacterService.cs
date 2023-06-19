using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_pokemon.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static readonly List<Character> characters = new() {
            new Character(),
            new Character {Id = 1,Name = "Sam"}
        };
        public async Task<ServiceResponse<Character>> AddCharacterAsync(Character newCharacter)
        {
           characters.Add(newCharacter);



           return new ServiceResponse<Character>{
            Data = newCharacter,
            Message = "Character is added successfully",
            Success = true
           };
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharactersAsync()
        {
            return new ServiceResponse<List<Character>> {
                Data = characters,
                Message = $"Number of Records: {characters.Count}",
                Success = true
            };
        }

        public async Task<ServiceResponse<Character?>> GetCharacterByIdAsync(int Id)
        {
            var character = characters.FirstOrDefault(c => c.Id == Id);

            return new ServiceResponse<Character?>{
                Data = character,
                Message = "Character is found successfully",
                Success = true
            };
        }
    }
}