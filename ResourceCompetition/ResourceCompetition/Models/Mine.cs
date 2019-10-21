using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceCompetition.Models
{
    public class Mine
    {
        private static int _maxId = 0;

        public Mine()
        {
            Id = ++_maxId;
        }

        public static void Reset()
        {
            _maxId = 0;
        }
        public int Id { get; set; }
        public Stop Location { get; set; }
        public List<Resource> Resources { get; set; }
    }

    
}