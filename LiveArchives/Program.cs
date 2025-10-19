using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Linq;

Dictionary<string, Action<string[]>> commands = new()
{
    {"open", (string[] args) => {
        Console.WriteLine($"Open {string.Join(", ", args)}"); } },
    {"ls", (string[] args) => {
        Console.WriteLine("List");
    } }
};
Console.WriteLine("Welcome to ObfuscatedArchive");

while (true)
{
    Console.Write(">> ");
    var cmd = Console.ReadLine();
    if (cmd == "exit" || cmd == "quit")
    {
        break;
    }
    // break up the cmd into command and its arguments.
    var cmd_args = cmd.Split(" ");
    var command = cmd_args.First();
    var arguments = cmd_args.Skip(1).ToArray();

    if (!string.IsNullOrEmpty(cmd) && commands.ContainsKey(cmd))
    {
        commands[cmd](arguments);
    }

}

return;
// See https://aka.ms/new-console-template for more information

try
{
    var archive = ObfuscatedArchive.ObfuscatedArchive.From(path);

    //using var indexFile = File.CreateText("index.csv");
    //indexFile.WriteLine("offset,size,name,nameBytes");
    //archive.Entries.ForEach(entry =>
    //{
    //    var hexValues = entry.Name.RawValue.Select(b => $"0x{b:X2}");
    //    indexFile.WriteLine($"{entry.Offset},{entry.Size},\"{entry.Name.Value}\",\"[{string.Join(",", hexValues)}]\"");
    //});

}
catch (Exception e)
{
    Console.WriteLine(e);
}


