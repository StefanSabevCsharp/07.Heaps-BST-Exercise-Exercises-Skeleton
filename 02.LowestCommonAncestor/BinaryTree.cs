namespace _02.LowestCommonAncestor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
            if (leftChild != null)
            {
                this.LeftChild.Parent = this;
            }

            if (rightChild != null)
            {
                this.RightChild.Parent = this;
            }
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            var firstTree = this.FindNode(first);
            var secondTree = this.FindNode(second);

            if(firstTree == null || secondTree == null)
            {
                throw new InvalidOperationException();
            }

            List<BinaryTree<T>> firstList = new List<BinaryTree<T>>();
            List<BinaryTree<T>> secondList = new List<BinaryTree<T>>();
            this.FillTheList(firstTree, firstList);
            this.FillTheList(secondTree, secondList);
            var result = firstList.Intersect(secondList).ToList().First();
            return result.Value;
        }

        private void FillTheList(BinaryTree<T> firstTree, List<BinaryTree<T>> firstList)
        {
           var current = firstTree;
            while(current != null)
            {
                firstList.Add(current);
                current = current.Parent;
            }
        }

        private BinaryTree<T> FindNode(T element)
        {
           if(this.Value.Equals(element))
            {
                return this;
            }

            var left = this.LeftChild?.FindNode(element);
            var right = this.RightChild?.FindNode(element);

            return left ?? right;
        }
    }
}
