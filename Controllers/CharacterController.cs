using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace dotnet_pokemon.Controllers;
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new() {
            new Character(),
            new Character {Id = 1,Name = "Sam"}
        };
        
        [HttpGet]
        public ActionResult<List<Character>> GetCharacters()
        {
            return Ok(characters);
        }

        [HttpGet("{id}")]
        public ActionResult<Character> GetCharacter(int id) {
            return Ok(characters.FirstOrDefault(c => c.Id == id));
        }

        [HttpPost]
        public ActionResult<Character> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return Ok(newCharacter);
        }
    }