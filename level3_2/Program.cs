﻿string[] lines = File.ReadAllLines("input.txt");

string oxygen = "";
string co2 = "";
string[] oxygen_list = lines;
string[] co2_list = lines;
int i = 0;

while (oxygen_list.Length > 1)
{
    int one = oxygen_list.Count(x => x[i] == '1');
    int zero = oxygen_list.Count(x => x[i] == '0');
    oxygen += one >= zero ? '1' : '0';
    oxygen_list = oxygen_list.Where(t => t.StartsWith(oxygen)).ToArray();
    i++;
}
i = 0;

while (co2_list.Length > 1)
{
    int one = co2_list.Count(x => x[i] == '1');
    int zero = co2_list.Count(x => x[i] == '0');

    co2 += one >= zero ? '0' : '1';
    co2_list = co2_list.Where(t => t.StartsWith(co2)).ToArray();
    i++;
}

string a = oxygen_list[0], b = co2_list[0];

Console.WriteLine($"oxygen: {a} {Convert.ToInt32(a,2)}");
Console.WriteLine($"co2: {b} {Convert.ToInt32(b,2)}");

int result = Convert.ToInt32(a,2) * Convert.ToInt32(b,2);

File.WriteAllText("output.txt", result.ToString());

Console.WriteLine(result);