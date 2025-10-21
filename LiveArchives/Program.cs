using System.Diagnostics;
using LiveArchives;

Archive archive = Archive.Instance;

Dictionary<string, Action<string[]>> commands = new()
{
    {"open", (string[] args) => {
        Console.WriteLine($"Open {string.Join(", ", args)}");
        try
        {
            archive.Open(args.First());
        }catch(Exception e)
        {
            Console.WriteLine($"Error opening archive: {e.Message}");
        }

    } },

    {"ls", (string[] args) => {
        Console.WriteLine("List");
        try
        {
            archive.List();
        }catch(Exception e)
        {
            Console.WriteLine($"Error listing archive: {e.Message}");
        }
    } },
    {"extract", (string[] args) => {
        Console.WriteLine($"Extract {string.Join(", ", args)}");
        try
        {

        }catch(Exception e)
        {
                        Console.WriteLine($"Error extracting from archive: {e.Message}");
        }
    } },
    {"help", (string[] args) => {
        Console.WriteLine("Available commands: open, ls, extract, help, exit, quit");
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

    ArgumentParser.From(cmd);
    // break up the cmd into command and its arguments.
    var cmd_args = cmd.Split(" ");
    var command = cmd_args.First();
    var arguments = cmd_args.Skip(1).ToArray();
    Debug.WriteLine($"command {command}");
    Debug.WriteLine($"cmd_args {cmd_args}");
    Debug.WriteLine($"arguments {arguments}");
    if (!string.IsNullOrEmpty(command) && commands.ContainsKey(command))
    {
        commands[command](arguments);
    }

}

return;

// See https://aka.ms/new-console-template for more information

//try
//{
//    var path = "";
//    var archive = ObfuscatedArchive.ObfuscatedArchive.From(path);

//    //using var indexFile = File.CreateText("index.csv");
//    //indexFile.WriteLine("offset,size,name,nameBytes");
//    //archive.Entries.ForEach(entry =>
//    //{
//    //    var hexValues = entry.Name.RawValue.Select(b => $"0x{b:X2}");
//    //    indexFile.WriteLine($"{entry.Offset},{entry.Size},\"{entry.Name.Value}\",\"[{string.Join(",", hexValues)}]\"");
//    //});

//}
//catch (Exception e)
//{
//    Console.WriteLine(e);
//}


