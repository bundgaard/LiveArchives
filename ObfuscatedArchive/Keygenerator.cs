using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscatedArchive
{
    public class Keygenerator
    {
        uint _seed;
        public Keygenerator(uint seed)
        {
            _seed = seed;
        }

        public uint Current { get { return _seed; } }
        public uint Next()
        {
            uint key = _seed;
            _seed = _seed * 7 + 3;
            return key;
        }

    }
}
