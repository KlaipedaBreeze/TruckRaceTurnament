using System.Collections.Generic;

namespace ResourceCompetition.Models
{
    public class Truck
    {
        private int _maxWeight = 20;
        public Truck()
        {
            if (AvailableColors.Count > 0)
            {
                Color = AvailableColors[0];
                AvailableColors.Remove(Color);
            }
            else
            {
                Color = "blue";
            }
        }
        
        public int Id { get; set; }
        public string Token { get; set; }
        public Stop Location { get; set; }
        public string Color { get; set; }
        public List<Mine> Mines { get; set; } = new List<Mine>();
        public List<Resource> Loaded { get; set; } = new List<Resource>();

        private static List<string> Colors = new List<string>()
        {
            "red","green", "Blue", "LightSalmon", "Crimson", "DeepSkyBlue", "GreenYellow"
        };

        private static List<string> AvailableColors = new List<string>(Colors);

        public static void Init()
        {
            AvailableColors = new List<string>(Colors);
        }

    }
}