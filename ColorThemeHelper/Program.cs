// See https://aka.ms/new-console-template for more information

Console.WriteLine("Color theme converter");


while (true)
{
    Console.WriteLine(">> ");

    var line = Console.ReadLine();
    if (string.IsNullOrEmpty(line))
    {
        Console.WriteLine("Empty line");
        continue;
    }

    List<double> hsl = new();
    int count = 3;
    int idx = 0;
    char c = line[idx];
    while (c != ' ' || c != ',')
    {

    }
}

