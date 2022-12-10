int p1pos = 5;
int p2pos = 6;

int p1score = 0;
int p2score = 0;

int cur = 1;

bool player1 = true;

while (p1score < 1000 && p2score < 1000)
{
    int sum = cur * 3 + 3;
    Console.WriteLine(sum);
    if (player1)
    {
        Console.WriteLine(p1pos);
        int pos = p1pos + sum;
        p1pos = pos%10 == 0 ? 10 : pos%10;
        p1score += p1pos;
        Console.WriteLine(p1pos);
    }
    else
    {
        Console.WriteLine(p2pos);
        int pos = p2pos + sum;
        p2pos = pos%10 == 0 ? 10 : pos%10;
        p2score += p2pos;
        Console.WriteLine(p2pos);
    }
    cur += 3;
    player1 = !player1;
}

if (p1score >= 1000)
{
    Console.WriteLine("Player 2 lost");
    Console.WriteLine(p2score);
    Console.WriteLine(cur-1);
    Console.WriteLine(p2score * (cur-1));
}
else
{
    Console.WriteLine("Player 1 lost");
    Console.WriteLine(p1score);
    Console.WriteLine(cur-1);
    Console.WriteLine(p1score * (cur-1));
}