Dictionary<(int p1, int p2, int s1, int s2),(long sc1, long sc2)> states = new();

var result = Roll(5,6,0,0);


Console.WriteLine(result.s1);
Console.WriteLine(result.s2);

(long s1, long s2) Roll(int p1, int p2, int s1, int s2)
{
    if (s1 >= 21)
        return (1, 0);

    if (s2 >= 21)
        return (0, 1);

    if (states.ContainsKey((p1, p2, s1, s2)))
        return states[(p1, p2, s1, s2)];

    (long s1, long s2) ans = (0, 0);

    for (int i = 1; i <= 3; i++)
    {
        for (int j = 1; j <= 3; j++)
        {
            for (int k = 1; k <= 3; k++)
            {
                int sum = i + j + k;

                int pos = p1 + sum;
                int newp1 = pos % 10 == 0 ? 10 : pos % 10;
                int news1 = s1 + newp1;

                (long sc1, long sc2) = Roll(p2, newp1, s2, news1);
                ans = (ans.s1 + sc2, ans.s2 + sc1);
            }
        }
    }
    states.Add((p1, p2, s1, s2), ans);
    return ans;
}

//class Quantum
//{
//    public int P1Score { get; set; }
//    public int P2Score { get; set; }

//    public int P1Pos { get; set; }
//    public int P2Pos { get; set; }

//    public bool Player1 { get; set; }

//    public (int p1, int p2) Roll()
//    {
//        if (P1Score >= 21)
//            return (1, 0);

//        if (P2Score >= 21)
//            return (0, 1);

//        (int s1, int s2) ans = (0, 0);

//        for (int i = 1; i <= 3; i++)
//        {
//            for (int j = 1; j <= 3; j++)
//            {
//                for (int k = 1; k <= 3; k++)
//                {
//                    int sum = i + j + k;

//                    if (Player1)
//                    {
//                        int pos = P1Pos + sum;
//                        P1Pos = pos % 10 == 0 ? 10 : pos % 10;
//                        P1Score += P1Pos;
//                    }
//                    else
//                    {
//                        int pos = P2Pos + sum;
//                        P2Pos = pos % 10 == 0 ? 10 : pos % 10;
//                        P2Score += P2Pos;
//                    }

//                    (int s1, int s2) = this.Copy().Roll();
//                    ans = (ans.s1 + s1, ans.s2 + s2);
//                }
//            }
//        }

//        return ans;
//    }

//    public Quantum Copy() => new Quantum
//    {
//        P1Score = P1Score,
//        P2Score = P2Score,
//        P1Pos = P1Pos,
//        P2Pos = P2Pos,
//        Player1 = !Player1
//    };
//}