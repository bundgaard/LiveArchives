using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveArchives.Language
{
    public class Position
    {
        public int Line { get; set; } = 0;
        public int Column { get; set; } = 0;
        public Position(Source source)
        {
            Line = source.LineNum;
            Column = source.Position;
        }

        public override string ToString()
        {
            return $"{Line},{Column}";
        }
    }
}
