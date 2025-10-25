using System.Text;

namespace LiveArchives.Language
{

    internal class AgnosticParser
    {

        private int _index = 0;
        private int _count = 0;

        private string _data = string.Empty;


        private AgnosticParser(string data)
        {
            _data = data;
            _count = data.Length;
        }


        public bool IsAtEnd()
        {
            return _index >= _count;
        }

        public char Next()
        {
            if (IsAtEnd())
            {
                return '\0';
            }
            var c = CurrentChar();
            ++_index;
            return c;
        }

        public char CurrentChar()
        {
            if (IsAtEnd())
                return '\0';
            return _data[_index];
        }

        public char Peek()
        {
            return !IsAtEnd() ? _data[_index + 1] : '\0';
        }

        public void Skip()
        {
            while (char.IsWhiteSpace(CurrentChar()))
            {
                Next();
            }
        }
        public string Until(char stopDelimiter)
        {
            StringBuilder sb = new();
            while (!IsAtEnd())
            {
                var c = Next();
                if (c == stopDelimiter)
                {
                    --_index;
                    break;
                }
                sb.Append(c);
            }
            return sb.ToString();
        }
        public static AgnosticParser From(string data)
        {
            return new AgnosticParser(data);
        }
    }
}
