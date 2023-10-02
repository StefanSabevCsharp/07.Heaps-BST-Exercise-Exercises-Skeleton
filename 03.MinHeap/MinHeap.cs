using System;
using System.Collections.Generic;
using System.Text;

namespace _03.MinHeap
{
    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        protected List<T> elements;

        public MinHeap()
        {
            this.elements = new List<T>();
        }

        public int Count => elements.Count;

        public void Add(T element)
        {
            elements.Add(element);
            this.HeapifyUp(elements.Count - 1);

        }

        protected void HeapifyUp(int index)
        {
            int parentIndex = (index - 1) / 2;
            while (index > 0 && IsLess(index, parentIndex))
            {
                Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        private void Swap(int index, int parentIndex)
        {
            T temp = elements[index];
            elements[index] = elements[parentIndex];
            elements[parentIndex] = temp;
        }

        private bool IsLess(int index, int parentIndex)
        {
            if (elements[index].CompareTo(elements[parentIndex]) < 0)
            {
                return true;
            }
            return false;
        }

        public T ExtractMin()
        {
            if(elements.Count == 0)
            {
                throw new InvalidOperationException();
            }
            T result = elements[0];
            this.HeapifyDown();

            return result;
        }

        private void HeapifyDown()
        {
            this.Swap(0, elements.Count - 1);
            elements.RemoveAt(elements.Count - 1);
            int index = 0;

            while (index < elements.Count / 2)
            {
                int childIndex = 2 * index + 1;
                if (childIndex + 1 < elements.Count && IsLess(childIndex + 1, childIndex))
                {
                    childIndex++;
                }
                if (IsLess(childIndex, index))
                {
                    Swap(childIndex, index);
                }
                index = childIndex;
            }


        }

        public T Peek()
        {
            if (elements.Count == 0)
            {
                throw new InvalidOperationException();
            }
            return elements[0];
        }
    }
}
