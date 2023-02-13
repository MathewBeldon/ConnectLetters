namespace ConnectLetters
{
    public class Eoghan
    {
        public static int ProcessInput(string input)
        {
            var pairsFound = 0;
            var list = new LinkedList(input);
            var maxSkip = input.Length - 2;

            for (int skip = 0; skip <= maxSkip; skip++)
            {
                var (newPairs, removed, _) = FindPairs(list.First, list, maxSkip, skip);
                maxSkip -= removed;
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
                        removed += 2 + skip;
                        pairsFound++;
                        if (TryRemoveSection(current, next, list)){
                            
                            // we've rewound
                            if (current.Previous is not null){
                                current = current.Previous;
                                currentIndex--;
                                maxDistance -= (2 + skip);

                                // check whether we can make any new pairs now that the (skip + 2) items ahead are gone
                                for (int i = 0; i <= skip; i++)
                                {
                                    // maxDistance passed here is 0 - this means we only ever try to find pairs from 'current' onwards
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
                            // we've fast-forwarded
                            else if (next.Next is not null){
                                current = next.Next;
                                currentIndex += (2 + skip);
                            }
                        }
                        else{
                            break;
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

        static bool TryRemoveSection(LinkedListNode start, LinkedListNode end, LinkedList list)
        {
            // if we can rewind (we're not at the start of the list)
            if (start.Previous is not null)
            {
                // remove the gap
                start.Previous.Next = end.Next;
                
                if (end.Next is not null)
                {
                    end.Next.Previous = start.Previous;
                }

                return true;
            }

            // we're removing a section from the start of the list
            if (end.Next is not null)
            {
                end.Next.Previous = null;
                list.First = end.Next;
                return true;
            }

            // start.previous is null, end.next is null - we have no more
            return false;
        }

        static LinkedListNode? GetNodeAfterSkip(LinkedListNode current, int skip = 0)
        {
            var next = current.Next;
            while(skip > 0){
                next = next?.Next;
                skip--;
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
        public LinkedList(string str)
        {
            First = new LinkedListNode(str[0]);
            var previous = First;
            for (int i = 1; i < str.Length; i++)
            {
                var newNode = new LinkedListNode(str[i]);
                newNode.Previous = previous;
                previous.Next = newNode;
                previous = newNode;
            }
        }

        public LinkedListNode First { get; set; }
    }
}