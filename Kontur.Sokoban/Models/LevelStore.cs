using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kontur.Sokoban.Models
{
    public class LevelStore
    {
        public string Name;
        public LevelObject[] Objects;
        public class LevelObject
        {
            public string Name;
            public Vec Pos;
        }
    }
}
