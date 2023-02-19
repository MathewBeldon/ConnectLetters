namespace ConnectLetters
{
    public class ChatGPT
    {
        public static int ProcessInput(string connections)
        {
            int count = 0;
            Stack<char> stackA = new Stack<char>();
            Stack<char> stackX = new Stack<char>();

            for (int i = 0; i < connections.Length; i++)
            {
                char current = connections[i];

                if (current == 'A')
                {
                    stackA.Push(current);
                }
                else if (current == 'B')
                {
                    if (stackA.Count > 0)
                    {
                        stackA.Pop();
                        count++;
                    }
                }
                else if (current == 'X')
                {
                    stackX.Push(current);
                }
                else if (current == 'Y')
                {
                    if (stackX.Count > 0)
                    {
                        stackX.Pop();
                        count++;
                    }
                }
            }

            return count;
        }
    }
}