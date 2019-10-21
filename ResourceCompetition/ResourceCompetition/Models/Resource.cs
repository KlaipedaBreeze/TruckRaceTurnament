using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceCompetition.Models
{
    public class Resource
    {
        private static int _maxId = 0;
        public Resource()
        {
            Id = ++_maxId;
        }
        public int Id { get; set; }
        public int Weight { get; set; }
        public int Value { get; set; }
        public ResourceType Type { get; set; }

    }
    public enum ResourceType
    {
        Gold,
        Fuel,
        Water
    }
}