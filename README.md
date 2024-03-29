```
ABA contains 1 matches
ABBA contains 2 matches
AABB contains 2 matches
ABABABAB contains 4 matches
AAAAAAAA contains 0 matches
BABBYYAYAAB contains 4 matches
XYABXYXYAB contains 5 matches
AXXBXYABXAYB contains 5 matches
ABBAXYYXXXBA contains 5 matches
XBXAYABXBYAY contains 6 matches
XYXXYAAXAYYXYYAABXAAXYBYYXBB contains 11 matches
XYABXYXYABABBAXYYXXXBAXBXAYABXBYAY contains 16 matches
YAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAX contains 1 matches
BAXBAAXYBAXXBYYBYAAAXAYBYBXAAXYXXYAAXAYYXYYAABXAAXYBYYXBB contains 23 matches
BAXBAAXYBAXXBYYBYAAAXAYBYBXAAXYXXYAAXAYYXYYAABXAAXYBYYXBBBBBYXYAXXXXBXXAAYXYYYYXXYYAYYYAXBXABYAAXXXA contains 42 matches
```

For `BAXBAAXYBAXXBYYBYAAAXAYBYBXAAXYXXYAAXAYYXYYAABXAAXYBYYXBB` my original solution found 22

```
                    1 1 1 1 1 1 1 1 1 1 2 2 2 2 2 2 2 2 2 2 3 3 3 3 3 3 3 3 3 3 4 4 4 4 4 4 4 4 4 4 5 5 5 5 5 5 5
0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6
B A X B A A X Y B A X X B Y Y B Y A A A X A Y B Y B X A A X Y X X Y A A X A Y Y X Y Y A A B X A A X Y B Y Y X B B
|_|   |_|   |_| |_| | |___| | |___|     | |___| | |___| | |_| | |_|     |___| | |_| |   |_| | | | |_| |   |_| | |
                    |_______|           |_______|       |     |_______________|     |_______| | |_____|       | |
                                                        |                                     |_______________| |
                                                        |_______________________________________________________|
```

My stratergy here was to find all pairs with a distance of 1 on the first pass and then increment the distance by 1 over each pass, this finds all the 
possible matches for most of the test cases but fails on the one above. 

```csharp
public int Pairwise(string connections)
{
    var matches = new List<(int, int)>();

    for (int i = connections.Length; i > 0; i--)
    {
        for (int j = 0; j < connections.Length; j++)
        {
            if (j + i + 1 >= connections.Length)
            {
                break;
            }

            var lhs = j;
            var rhs = j + i + 1;

            if (connections[lhs] - 1 == connections[rhs]
            || connections[lhs] + 1 == connections[rhs])
            {
                if (!IsIntersect(lhs, rhs))
                {
                    matches.Add((lhs, rhs));
                    j = rhs;
                }
            }
        }
    }

    return matches.Count;

    bool IsIntersect(int lhs, int rhs)
    {
        foreach (var match in matches)
        {
            if ((lhs >= match.Item1 && lhs <= match.Item2)
            || (rhs >= match.Item1 && rhs <= match.Item2))
            {
                return true;
            }
        }
        return false;
    }
}
```

The 'fixed' solution that finds 23 in the above test case finds these matches
```
                    1 1 1 1 1 1 1 1 1 1 2 2 2 2 2 2 2 2 2 2 3 3 3 3 3 3 3 3 3 3 4 4 4 4 4 4 4 4 4 4 5 5 5 5 5 5 5
0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6
B A X B A A X Y B A X X B Y Y B Y A A A X A Y B Y B X A A X Y X X Y A A X A Y Y X Y Y A A B X A A X Y B Y Y X B B
|_| | |_| | |_| | | | |___| | | |     | |___| | |___|   | |_| | |_|     |___| |_| | |   |_| | | | |_| |   |_| | |
    |     |_____| | |_______| | |     |_______|         |     |___________________| |_______| | |_____|       | |
    |             |___________| |                       |                                     |_______________| |
    |___________________________|                       |_______________________________________________________|
```

It works similar to the original solution but tries to find a new pair either side of one it just found, this is an attempt to condense as 
many pairs into the smallest space possible

```csharp
public int Pairwise(string connections)
{
    var matches = new List<(int, int)>();

    for (int i = 0; i < connections.Length; i++)
    {
        for (int j = 0; j < connections.Length; j++)
        {
            var lhs = j;
            var rhs = j + i + 1;

            if (rhs >= connections.Length)
            {
                break;
            }

            if (Check(lhs, rhs))
            {
                matches.Add((lhs, rhs));
                j = FindSurroundingPairs(lhs - 1, rhs + 1);
            }
        }
    }

    return matches.Count;

    int FindSurroundingPairs(int lhs, int rhs)
    {
        if (lhs > 0 && rhs < connections.Length
        && Check(lhs, rhs))
        {
            matches.Add((lhs, rhs));
            FindSurroundingPairs(lhs - 1, rhs + 1);
        }

        return rhs - 1;
    }

    bool Check(int lhs, int rhs)
    {
        if (IsMatch() && !IsIntersect())
        {
            return true;
        }

        return false;

        bool IsMatch()
        {
            if (connections[lhs] - 1 == connections[rhs]
                || connections[lhs] + 1 == connections[rhs])
            {
                return true;
            }
            return false;
        }

        bool IsIntersect()
        {
            for (int x = 0; x < matches.Count; x++)
            {
                if ((lhs >= matches[x].Item1 && lhs <= matches[x].Item2)
                || (rhs >= matches[x].Item1 && rhs <= matches[x].Item2))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
```
```
                    1 1 1 1 1 1 1 1 1 1 2 2 2 2 2 2 2 2 2 2 3 3 3 3 3 3 3 3 3 3 4 4 4 4 4 4 4 4 4 4 5 5 5 5 5 5 5 5 5 5 6 6 6 6 6 6 6 6 6 6 7 7 7 7 7 7 7 7 7 7 8 8 8 8 8 8 8 8 8 8 9 9 9 9 9 9 9 9 9 9
0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9
B A X B A A X Y B A X X B Y Y B Y A A A X A Y B Y B X A A X Y X X Y A A X A Y Y X Y Y A A B X A A X Y B Y Y X B B B B B Y X Y A X X X X B X X A A Y X Y Y Y Y X X Y Y A Y Y Y A X B X A B Y A A X X X A
|_| | |_| | |_| | | | |___| | | | | | | |___| | |___| | | |_| | |_|     |___| |_| | |   |_| | | | |_| |   |_| | | | | | |_| |___| | | | |_____|   |_| | | | |_| | | |   | | |___| | | |_| | |   | | |  
    |     |_____| | |_______| | | | | |_______|       | |     |___________________| |_______| | |_____|       | | | | |           | | |_______________| | |_____| | |   | |       | |_____| |   | | |  
    |             |___________| | | |                 | |                                     |_______________| | | | |           | |___________________|         | |   | |       |_________|   | | |  
    |___________________________| | |                 | |_______________________________________________________| | | |           |_______________________________| |   | |_____________________| | |                            
                                  | |                 |___________________________________________________________| | |                                             |   |_________________________| |                               
                                  | |_______________________________________________________________________________| |                                             |_______________________________|  
                                  |___________________________________________________________________________________|                                                                                 
```

|          connections |         Mean | Allocated |
|--------------------- |-------------:|----------:|
|             AAAAAAAA |     63.65 ns |      32 B |
|                 AABB |     30.88 ns |      88 B |
|                  ABA |     17.85 ns |      88 B |
|             ABABABAB |    106.20 ns |      88 B |
|                 ABBA |     30.80 ns |      88 B |
|         ABBAXYYXXXBA |    212.63 ns |     176 B |
|         AXXBXYABXAYB |    215.03 ns |     176 B |
|          BABBYYAYAAB |    169.22 ns |      88 B |
|  BAXB(...)XXXA [100] | 37,187.90 ns |    1144 B |
|         XBXAYABXBYAY |    241.20 ns |     176 B |
|           XYABXYXYAB |    150.27 ns |     176 B |
| XYABX(...)XBYAY [34] |  2,100.28 ns |     328 B |
| YAAAA(...)AAAAX [34] |  1,154.00 ns |      88 B |