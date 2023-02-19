namespace ConnectLetters
{
    public static class Matt
    {
        public static int ProcessInput(string connections)
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
    }
}
