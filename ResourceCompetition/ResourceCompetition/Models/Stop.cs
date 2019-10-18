using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceCompetition.Models
{
    public class Stop
    {
        private static int _maxId = 0;

        public Stop()
        {
            Id = ++_maxId;
        }

        public static void Reset()
        {
            _maxId = 0;
        }
        public string Name { get; set; }
        public int Id { get; set; }
        public int CordX { get; set; }
        public int CordY { get; set; }
        public bool IsHidden { get; set; }

    }


}