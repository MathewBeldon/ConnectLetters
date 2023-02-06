var input = File.ReadLines(@"input.txt").Select(x => x.Trim()).ToList();

foreach (var line in input)
{
    var result = Pairwise(line);
    Console.WriteLine($"{line} contains {result} matches");
}

int Pairwise(string connections)
{
    //get all pairs
    var pairs = new List<(int, int)>();
    for (int i = 0; i < connections.Length; i++)
    {
        for (int j = i; j < connections.Length; j++)
        {
            if ((int)connections[i] - 1 == (int)connections[j]
            || (int)connections[i] + 1 == (int)connections[j])
            {
                pairs.Add((i, j));
            }
        }
    }

    //find max valid pairs
    var matches = new List<(int, int)>();
    for (int i = 0; i < connections.Length; i++)
    {
        foreach (var pair in pairs.Where(x => x.Item2 - x.Item1 == i))
        {
            if (!IsIntersect(pair)
            && !matches.Contains(pair))
            {
                matches.Add(pair);
            }
        }
    }

    return matches.Count;

    bool IsIntersect((int, int) pair)
    {
        foreach (var match in matches)
        {
            if ((pair.Item1 >= match.Item1 && pair.Item1 <= match.Item2)
            || (pair.Item2 >= match.Item1 && pair.Item2 <= match.Item2))
            {
                return true;
            }
        }
        return false;
    }
}