using System.IO;
using System.Linq;

int horizontal = 0;
int depth = 0;
int aim = 0;
List<(string Instruction, int Value)> instructions = File.ReadAllLines("input.txt").Select(x => (x.Split()[0], int.Parse(x.Split()[1]))).ToList();

foreach (var entry in instructions) 
{
    switch (entry.Instruction)
    {
        case "forward":
            horizontal+=entry.Value;
            depth += aim*entry.Value;
            break;
        case "down":
            aim += entry.Value;
            break;
        case "up":
            aim -= entry.Value;
            break;
    }
}

int result = horizontal * depth;
File.WriteAllText("output.txt", result.ToString());
Console.WriteLine(result);