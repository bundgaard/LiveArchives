namespace LiveArchives.Language
{
    internal class ArgumentParser
    {

        public string Command { get; private set; } = string.Empty;
        private AgnosticParser _parser;
        private ArgumentParser(string line)
        {
            // TODO: 
            // command line arguments is a string drive:\folder\app.exe [options]
            // [options] can be: 
            // "....." one single argument with spaces that is coherent.
            // -o value
            // -s
            // --option=value
            // --option value

            // For now we just need to separate the command from its arguments.
            // and separate "" from other arguments.

            // Solution is to call Win32 CommandLineToArgvW via P/Invoke. (cheating)
            _parser = AgnosticParser.From(line);
            var command = _parser.Until(' ');

            string[] args = [];


            Command = command;

        }

        private string[] Arguments(string line)
        {
            return [];
        }
        private string QuotedArgument()
        {

            return string.Empty;
        }



        public static ArgumentParser From(string line)
        {

            return new ArgumentParser(line);
        }
    }
}
