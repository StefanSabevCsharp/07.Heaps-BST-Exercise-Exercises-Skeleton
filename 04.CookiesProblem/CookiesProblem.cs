using System;
using System.Linq;
using _03.MinHeap;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int minSweetness, int[] cookies)
        {
            int count = 0;

            PriorityQueue<int> queue = new PriorityQueue<int>();
            foreach (var item in cookies)
            {
                queue.Enqueue(item);
            }

            while (queue.Count > 1)
            {
                int first = queue.Dequeue();
                int second = queue.Dequeue();
                int result = first + 2 * second;
                queue.Enqueue(result);
                count++;
                if (queue.Peek() >= minSweetness)
                {
                    return count;
                }
            }
            return -1;
            
        }
    }
}
