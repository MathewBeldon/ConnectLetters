namespace LetterConnector
{
    public class Utilities{
        public static int ProcessInput(string input)
        {
            var pairsFound = 0;
            var list = new LinkedList(input);
            var itemCount = input.Length;

            for (int skip = 0; skip <= itemCount - 2; skip++)
            {
                var (newPairs, removed, _) = FindPairs(list.First, list, itemCount, skip);
                itemCount -= removed;
                pairsFound += newPairs;
            }

            return pairsFound;
        }

        static (int found, int removed, LinkedListNode newCursor) FindPairs(LinkedListNode start, LinkedList list, int maxDistance, int skip)
        {
            var pairsFound = 0;
            var removed = 0;
            var currentIndex = 0;
            LinkedListNode? current = start;
            do
            {
                var next = GetNodeAfterSkip(current, skip);
                if (next is not null)
                {
                    if (MatchingCharacter(current.Value) == next.Value)
                    {
                        (current, bool rewound) = RemoveSection(current, next, list);
                        
                        removed += 2 + skip;
                        pairsFound++;
                        
                        if (rewound){
                            // decrement cursor before the gap
                            currentIndex--;
                            // decrement maximum distance by # items removed
                            maxDistance -= 2 + skip;

                            // if we've removed a gap, find pairs that could now cross that gap
                            // 'current' is the place before the gap
                            if (current is not null)
                            {
                                for (int i = 0; i <= skip; i++)
                                {
                                    // maxDistance passed here is 0 - this means we only ever try to find pairs from 'currnet' onwards
                                    // with various values of i up to skip
                                    (var additionalPairs, var innerRemoved, var ptr) = FindPairs(current, list, 0, i);
                                    if (additionalPairs > 0){
                                        pairsFound += additionalPairs;
                                        removed += innerRemoved;
                                        maxDistance -= innerRemoved;
                                        break;
                                    }
                                }
                            }
                        }
                        else{
                            // increment cursor past the gap
                            currentIndex += 2 + skip;
                        }
                    }
                    else
                    {
                        current = current.Next;
                        currentIndex++;
                    }
                }
                else{
                    break;
                }
            }
            while (currentIndex < maxDistance - skip);

            return (pairsFound, removed, current);
        }

        static (LinkedListNode? cursor, bool rewind) RemoveSection(LinkedListNode start, LinkedListNode end, LinkedList list)
        {
            // if we can rewind (we're not at the start of the list)
            if (start.Previous is not null)
            {
                var newStart = start.Previous;

                // remove the gap
                newStart.Next = end.Next;
                if (newStart.Next is not null)
                {
                    newStart.Next.Previous = newStart;
                }

                return (newStart, true);
            }

            // we're removing a section from the start of the list
            if (end.Next is not null)
            {
                end.Next.Previous = null;
                list.First = end.Next;
                return (end.Next, false);
            }

            // start.previous is null, end.next is null
            return (null, false);
        }

        static LinkedListNode? GetNodeAfterSkip(LinkedListNode current, int skip = 0)
        {
            var next = current.Next;
            for (int i = 0; i < skip; i++)
            {
                next = next?.Next;
            }

            return next;
        }

        static char MatchingCharacter(char a) => a switch
        {
            'A' => 'B',
            'B' => 'A',
            'X' => 'Y',
            'Y' => 'X',
            _ => throw new NotImplementedException("should not be possible")
        };
    }

    public class LinkedListNode
    {
        public LinkedListNode(char value)
        {
            Value = value;
        }

        public LinkedListNode? Next { get; set; }

        public LinkedListNode? Previous { get; set; }

        public char Value { get; }
    }

    public class LinkedList
    {
        public LinkedList(IEnumerable<char> items)
        {
            First = new LinkedListNode(items.First());
            var previous = First;
            foreach (var item in items.Skip(1))
            {
                var newNode = new LinkedListNode(item);
                if (previous is not null)
                {
                    newNode.Previous = previous;
                    previous.Next = newNode;
                }
                previous = newNode;
            }
        }

        public LinkedListNode First { get; set; }
    }
}