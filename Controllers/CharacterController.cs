using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_pokemon.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_pokemon.Controllers;
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private  readonly ICharacterService _CharacterService;
        
        public CharacterController(ICharacterService characterService)
        {
            _CharacterService = characterService;
            
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Character>>>> GetCharacters()
        {
            var charactersResponse = await _CharacterService.GetAllCharactersAsync();
            return Ok(charactersResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Character>>> GetCharacter(int id) {
            var characterResponse = await _CharacterService.GetCharacterByIdAsync(id);
            return Ok(characterResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Character>>> AddCharacter(Character newCharacter)
        {
            var CharacterCreatedResponse = await _CharacterService.AddCharacterAsync(newCharacter);
            return Ok(CharacterCreatedResponse);
        }
    }