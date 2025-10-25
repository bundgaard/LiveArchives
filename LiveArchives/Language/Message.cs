using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveArchives.Language
{
    internal enum MessageType
    {
        SOURCE_LINE, SYNTAX_ERROR, PARSER_SUMMARY, INTERPRETER_SUMMARY, COMPILER_SUMMARY, MISCELLANEOUS_SUMMARY, TOKEN, ASSIGN, FETCH, BREAKPOINT, RUNTIME_ERROR, CALL, RETURN
    }
    internal class Message
    {
        private readonly MessageType _type;
        private readonly object _body;
        public Message(MessageType type, object body)
        {
            _type = type;
            _body = body;
        }
    }
}
