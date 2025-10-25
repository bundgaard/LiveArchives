using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveArchives.Language
{
    public class Source(StreamReader reader)
    {
        public static readonly char EOL = '\n';
        public static readonly char EOF = '\0';

        private string _line = string.Empty;
        public int LineNum { get; private set; }
        public int Position { get; private set; } = -2;

        public char CurrentChar()
        {
            if (Position == -2)
            {
                ReadLine();
                return NextChar();
            }

            if (string.IsNullOrEmpty(_line))
            {
                return EOF;
            }

            if (Position == -1 || Position == _line.Length)
            {
                return EOL;
            }

            if (Position > _line.Length)
            {
                ReadLine();
                return NextChar();
            }

            return _line[Position];
        }

        public char NextChar()
        {
            ++Position;
            return CurrentChar();
        }



        public char PeekChar()
        {
            CurrentChar();
            if (string.IsNullOrEmpty(_line))
            {
                return EOF;
            }
            var nextPos = Position + 1;
            return nextPos < _line.Length ? _line[nextPos] : EOL;
        }

        public void ReadLine()
        {
            _line = reader.ReadLine() ?? string.Empty;
            Position = -1;
            if (string.IsNullOrEmpty(_line))
            {
                ++LineNum;
            }
        }

    }
}
