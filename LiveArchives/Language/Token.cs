using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LiveArchives.Language
{
    public enum TokenType
    {
        Number,
        Assignment,
        Identifier,
        Semicolon,
        Plus, Minus, Divide, Multiply,
        OpenParen, CloseParen, OpenBracket, CloseBracket, OpenCurly, CloseCurly,
        String,
        Eof,
        Equal,
        Greater,
        Bang,
        GreaterEqual,
        LessEqual,
        Less,
        LogicalAnd,
        BitwiseAnd
    }
    public class Token
    {
        public static readonly HashSet<char> Brackets = [
            '(',
            ')',
            '[',
            ']',
            '{',
            '}',
        ];
        public static readonly HashSet<char> MultiTokens = [
            '!',    // !, !=, !==
            '=',    // =, ==, ===
            '<',    // <, <=
            '>',    // >, >=
            '&',    // &, &&
            '|',    // |, ||
            '+',    // +, ++
            '-',    // -, --
            '.' ,   // ., ...
            '?',    // ?, ??
            '/',    // /, //, /*
            '*',    // *, */
        ];
        public string Text { get; protected set; }

        public Position Position { get; protected set; }
        public TokenType Type { get; protected set; }
        private Token(Position position)
        {

            Position = position;
            Text = string.Empty;
        }

        public override string ToString()
        {
            return $"<{Type},\"{Text}\",{Position}>";
        }
        public static class Factory
        {


            public static Token Number(Source source)
            {
                var pos = new Position(source);
                var token = new Token(pos);
                StringBuilder sb = new();
                while (char.IsDigit(source.CurrentChar()))
                {
                    sb.Append(source.CurrentChar());
                    source.NextChar();
                }
                token.Text = sb.ToString();
                token.Type = TokenType.Number;
                return token;
            }

            public static Token Assignment(Source source)
            {
                var pos = new Position(source);
                var token = new Token(pos);

                StringBuilder sb = new();
                if (source.CurrentChar() == '=')
                {
                    sb.Append(source.CurrentChar());
                    source.NextChar();
                }
                token.Text = sb.ToString();
                token.Type = TokenType.Assignment;
                return token;
            }

            public static Token Identifier(Source source)
            {
                var pos = new Position(source);
                var token = new Token(pos);

                StringBuilder stringBuilder = new StringBuilder();
                while (char.IsLetter(source.CurrentChar()))
                {
                    stringBuilder.Append(source.CurrentChar());
                    source.NextChar();
                }
                token.Text = stringBuilder.ToString();
                token.Type = TokenType.Identifier;
                return token;
            }
            public static Token Semicolon(Source source)
            {
                var pos = new Position(source);
                var token = new Token(pos);

                if (source.CurrentChar() == ';')
                {
                    token.Text = source.CurrentChar().ToString();
                    source.NextChar();
                }
                token.Type = TokenType.Semicolon;

                return token;
            }

            public static Token String(Source source)
            {
                var pos = new Position(source);
                var token = new Token(pos);

                source.NextChar(); // Skip quote
                StringBuilder sb = new();
                while (source.CurrentChar() != '"')
                {
                    if (source.CurrentChar() == '\\')
                    {
                        throw new InvalidDataException("null determined escape sequence");
                    }
                    else
                    {
                        sb.Append(source.CurrentChar());
                    }

                    source.NextChar();
                }

                token.Text = sb.ToString();
                token.Type = TokenType.String;
                source.NextChar(); // Skip quote
                return token;
            }
            public static Token Eof(Source source)
            {
                var pos = new Position(source);
                var token = new Token(pos)
                {
                    Text = "<EOF>",
                    Type = TokenType.Eof,
                };
                return token;
            }

            public static Token Brackets(Source source)
            {


                var pos = new Position(source);
                var (glyph, type) = source.CurrentChar() switch
                {
                    '(' => ('(', TokenType.OpenParen),
                    ')' => (')', TokenType.CloseParen),
                    '[' => ('[', TokenType.OpenBracket),
                    ']' => (']', TokenType.CloseBracket),
                    '{' => ('{', TokenType.OpenCurly),
                    '}' => ('}', TokenType.CloseCurly),
                    _ => throw new InvalidDataException($"Unknown bracket: {source.CurrentChar()}")
                };
                source.NextChar();
                var token = new Token(pos)
                {
                    Text = glyph.ToString(),
                    Type = type,
                };

                return token;
            }

            public static Token MultiToken(Source source)
            {
                /*
                 *            '!',    // !, !=, !== third level not supported yet
                 *            '=',    // =, ==, === third level not supported yet
                 *            '<',    // <, <=
                 *            '>',    // >, >=
                 *            '&',    // &, &&
                 *            '|',    // |, ||
                 *            '+',    // +, ++
                 *            '-',    // -, --
                 *            '.' ,   // ., ...
                 *            '?',    // ?, ??
                 *            '/',    // /, //, / *
                 *            '*',    // *, * /
                */

                var currentChar = source.CurrentChar();
                TokenType type = TokenType.Eof;
                StringBuilder sb = new();
                do
                {
                    if (currentChar == '>')
                    {
                        sb.Append(currentChar);
                        if (source.PeekChar() == '=')
                        {
                            sb.Append(source.NextChar());
                            
                            currentChar = source.NextChar();
                            type = TokenType.GreaterEqual; // Placeholder
                            break;
                        }
                        type = TokenType.Greater; // Placeholder
                        source.NextChar();
                        break;
                    }
                    if (currentChar == '<')
                    {
                        sb.Append(currentChar);
                        if (source.PeekChar() == '=')
                        {
                            sb.Append(source.NextChar());
                            currentChar = source.NextChar();
                            type = TokenType.LessEqual; // Placeholder
                            break;
                        }
                        type = TokenType.Less; // Placeholder
                        source.NextChar();
                        break;
                    }
                    if (currentChar == '!')
                    {
                        sb.Append(currentChar);
                        if (source.PeekChar() == '=')
                        {
                            sb.Append(source.NextChar());
                            currentChar = source.NextChar();
                            type = TokenType.Greater;
                            break;
                        }
                        type = TokenType.Bang;
                        source.NextChar();
                        break;
                    }
                    if (currentChar == '=')
                    {
                        sb.Append(currentChar);
                        if (source.PeekChar() == '=')
                        {
                            sb.Append(source.NextChar());
                            currentChar = source.NextChar();
                            type = TokenType.Equal;
                            break;
                        }
                        type = TokenType.Assignment;
                        source.NextChar();
                        break;
                    }
                    if (currentChar == '&')
                    {
                        sb.Append(currentChar);
                        if (source.PeekChar() == '&')
                        {
                            sb.Append(source.NextChar());
                            currentChar = source.NextChar();
                            type = TokenType.LogicalAnd;
                            // type = TokenType.LogicalAnd; // Placeholder
                            break;
                        }
                        type = TokenType.BitwiseAnd;
                        source.NextChar();
                        break;
                    }
                    currentChar = source.NextChar();

                } while (currentChar != '\0');




                var pos = new Position(source);
                var token = new Token(pos)
                {
                    Type = type,
                    Text = sb.ToString()
                };

                return token;
            }
        }
    }

}
