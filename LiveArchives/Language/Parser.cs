namespace LiveArchives.Language
{
    internal class Parser(Scanner scanner)
    {
        public Expression Expression { get; private set; };

        private readonly Token _currentToken;
        private readonly Token _nextToken;

        public void Parse()
        {

        }



    }
}
