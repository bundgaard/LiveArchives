using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace LiveArchives.Language
{
    public class Scanner(Source source)
    {
        public List<Token> Tokens { get; private set; } = new();

        public void Tokenize()
        {
            var currentChar = source.CurrentChar();
            while (currentChar != '\0')
            {
                currentChar = source.CurrentChar();

                if (char.IsWhiteSpace(currentChar))
                {
                    source.NextChar();
                    continue;
                }
                if (currentChar == '\0')
                {
                    Tokens.Add(Token.Factory.Eof(source));
                    break;
                }
                if (currentChar == '"')
                {
                    Tokens.Add(Token.Factory.String(source));
                    continue;
                }
                if (char.IsLetter(currentChar))
                {
                    Tokens.Add(Token.Factory.Identifier(source));
                    continue;
                }
                if (currentChar == ';')
                {
                    Tokens.Add(Token.Factory.Semicolon(source));
                    continue;
                }
                if (char.IsDigit(currentChar))
                {
                    Tokens.Add(Token.Factory.Number(source));
                    continue;
                }
                if (Token.MultiTokens.Contains(currentChar))
                {
                    // For future expansion: arithmetic operators
                    Tokens.Add(Token.Factory.MultiToken(source));
                    continue;
                }
                if (Token.Brackets.Contains(currentChar))
                {
                    Tokens.Add(Token.Factory.Brackets(source));
                    continue;
                }
                Console.WriteLine($"{currentChar} is not defined");
                source.NextChar();


            }
        }

    }
}
