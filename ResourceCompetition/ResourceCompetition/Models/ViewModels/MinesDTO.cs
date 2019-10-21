using System.Collections.Generic;

namespace ResourceCompetition.Models.ViewModels
{
    public class MinesDTO
    {
        public double Distance { get; set; }
        public int Id { get; set; }
        public List<ResourceDTO> ResourcesLeft { get; set; }
    }
}