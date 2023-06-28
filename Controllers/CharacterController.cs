using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers;

    [Authorize]
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
        public async Task<ActionResult<ServiceResponse<List<CharacterResponseDto>>>> GetCharacters()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            var charactersResponse = await _CharacterService.GetAllCharactersAsync(userId);
            return Ok(charactersResponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> GetCharacter(int id) {
            var characterResponse = await _CharacterService.GetCharacterByIdAsync(id);
            if(characterResponse.Data is null)
                return NotFound(characterResponse);
            
            return Ok(characterResponse);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            var CharacterCreatedResponse = await _CharacterService.AddCharacterAsync(newCharacter, userId);
            return Ok(CharacterCreatedResponse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter, int id){
            var response = await _CharacterService.UpdatedCharacterAsync(updatedCharacter, id);
            if (response.Data is null)
                return NotFound(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<CharacterResponseDto>>> DeleteCharacter(int id){
            var response = await _CharacterService.DeleteCharacterAsync(id);
            if (response.Data is null)
                return NotFound(response);

            return Ok(response);
        }
    }