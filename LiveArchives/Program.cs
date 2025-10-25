using System.Data.Common;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text;
using LiveArchives.Language;

using Microsoft.CodeAnalysis.CSharp.Syntax;



Console.WriteLine("LiveArchives");

bool running = true;
while (running)
{

    Console.Write(">> ");
    var line = Console.ReadLine();
    if (string.IsNullOrEmpty(line))
    {
        continue;
    }
    var command = line.ToLower();
    if (command == "quit" || command == "exit")
    {
        running = false;
        break;
    }

    using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(line));
    using var stream = new StreamReader(memoryStream);
    Source source = new(stream);
    Scanner scanner = new(source);
    scanner.Tokenize();

    foreach (var token in scanner.Tokens)
    {
        Console.WriteLine(token.ToString());
    }
    int debug = 0;
}