﻿string[] lines = File.ReadAllLines("sample.in");

int count = 0;

for (int i = 0; i < lines.Length; i++)
{
    string line = lines[i];

    if (string.IsNullOrWhiteSpace(line)) continue;

    /* write code here */

    count++;
}

Console.WriteLine(count);