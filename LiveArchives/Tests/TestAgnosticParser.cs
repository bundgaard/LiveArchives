using Xunit;
namespace LiveArchives.test
{
    public class TestAgnosticParser
    {
        static string argument = "c:\\world\\app.exe \"something incredible\" --help";
        [Fact]
        public void TestIsNotAtEnd()
        {
            var agnosticParser = AgnosticParser.From(argument);
            Assert.False(agnosticParser.IsAtEnd(), "we are not at the end");
        }

        [Fact]
        public void TestCommand()
        {
            var agnosticParser = AgnosticParser.From(argument);
            var command = agnosticParser.Until(' ');
            Assert.Equal("c:\\world\\app.exe", command);



        }


        [Fact]
        public void TestSkip()
        {
            var agnostParser = AgnosticParser.From("1 + 1 =         2");
            Assert.Equal('1', agnostParser.CurrentChar());
            agnostParser.Next();
            agnostParser.Skip();
            Assert.Equal('+', agnostParser.CurrentChar());
            agnostParser.Next();
            agnostParser.Skip();
            Assert.Equal('1', agnostParser.CurrentChar());
            agnostParser.Next();
            agnostParser.Skip();
            Assert.Equal('=', agnostParser.CurrentChar());
            agnostParser.Next();
            agnostParser.Skip();
            Assert.Equal('2', agnostParser.CurrentChar());
            agnostParser.Next();
            Assert.True(agnostParser.IsAtEnd());
            Assert.Equal('\0', agnostParser.CurrentChar());
        }

    }
}
