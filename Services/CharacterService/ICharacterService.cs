using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_pokemon.Services
{
    public interface ICharacterService 
    {
        Task<ServiceResponse<List<Character>>> GetAllCharactersAsync();
        Task<ServiceResponse<Character?>> GetCharacterByIdAsync(int Id);
        Task<ServiceResponse<Character>> AddCharacterAsync(Character newCharacter);
    }
}