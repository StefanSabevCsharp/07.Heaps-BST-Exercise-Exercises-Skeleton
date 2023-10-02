﻿using System;
using System.Collections.Generic;

namespace _03.MinHeap
{
    public class PriorityQueue<T> : MinHeap<T> where T : IComparable<T>
    {
        public PriorityQueue()
        {
            this.elements = new List<T>();
        }

        public void Enqueue(T element)
        {
            base.Add(element);
        }

        public T Dequeue()
        {
            return base.ExtractMin();
        }

        public void DecreaseKey(T key)
        {

            int index = elements.IndexOf(key);

            if (index == -1)
            {
                throw new InvalidOperationException();
            }
            elements[index] = key;
            base.HeapifyUp(index);
        }
    }
}
