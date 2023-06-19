using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dto
{
    public class CharacterRequestDto
    {
        public string? Name { get; set; } = "Character";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        
        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}