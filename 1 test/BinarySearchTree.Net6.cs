

public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
{
    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    private Node root;

    private BinarySearchTree(Node node)
    {
        this.PreOrderCopy(node);
    }

    public BinarySearchTree()
    {
    }

    public void Insert(T element)
    {
        this.root = this.Insert(element, this.root);
    }

    public bool Contains(T element)
    {
        Node current = this.FindElement(element);

        return current != null;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    public IBinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element);

        return new BinarySearchTree<T>(current);
    }

    public void Delete(T element)
    {
        if (this.root is null)
        {
            throw new InvalidOperationException();
        }
        this.root = this.Delete(element, this.root);
    }

    private Node Delete(T element, Node node)
    {
        if (node is null)
        {
            return node;
        }
        int compare = element.CompareTo(node.Value);
        if (compare < 0)
        {
            node.Left = this.Delete(element, node.Left);
        }
        else if (compare > 0)
        {
            node.Right = this.Delete(element, node.Right);
        }
        else
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            else if (node.Right == null)
            {
                return node.Left;
            }
            else if(node.Left != null)
            {
                Node temp = node;
                node = this.FindMin(node.Right);
                node.Right = this.DeleteMin(temp.Right);
                node.Left = temp.Left;
            }
            
        }
        return node;
    }

    private Node FindMin(Node node)
    {
        if(node.Left == null)
        {
            return node;
        }
        return this.FindMin(node.Left);

    }

   
    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }
        this.root = this.DeleteMax(this.root);
    }

    private Node DeleteMax(Node node)
    {
        if (node.Right == null)
        {
            return node.Left;
        }
        node.Right = this.DeleteMax(node.Right);
        return node;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }
        this.root = this.DeleteMin(this.root);
    }

    private Node DeleteMin(Node node)
    {

        if (node.Left == null)
        {
            return node.Right;
        }
        node.Left = this.DeleteMin(node.Left);

        return node;

    }

    public int Count()
    {
        int count = this.Count(this.root);
        return count;
    }

    private int Count(Node root)
    {

        if (root == null)
        {
            return 0;
        }
        return 1 + this.Count(root.Left) + this.Count(root.Right);
    }

    public int Rank(T element)
    {
        int count = this.Rank(element, this.root);
        return count;
    }

    private int Rank(T element, Node node)
    {
        if (node is null)
        {
            return 0;
        }
        if (element.CompareTo(node.Value) < 0)
        {
            return this.Rank(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            return 1 + this.Count(node.Left) + this.Rank(element, node.Right);
        }
        else
        {
            return this.Count(node.Left);
        }

    }

    public T Select(int rank)
    {
        Node node = this.Select(rank, this.root);

        if (node == null)
        {
            throw new InvalidOperationException();
        }
        return node.Value;
    }

    private Node Select(int rank, Node node)
    {
        if (node is null)
        {
            return null;
        }
        int leftCount = this.Count(node.Left);
        if (leftCount == rank)
        {
            return node;
        }
        else if (leftCount > rank)
        {
            return this.Select(rank, node.Left);
        }
        else
        {
            return this.Select(rank - (leftCount + 1), node.Right);
        }
    }

    public T Ceiling(T element)
    {
        return this.Select(this.Rank(element) + 1);
    }

    public T Floor(T element)
    {
        return this.Select(this.Rank(element) - 1);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        if (this.root == null)
        {
            return new List<T>();
        }
        List<T> result = new List<T>();
        this.Range(this.root, result, startRange, endRange);
        return result;
    }

    private void Range(Node node, List<T> result, T startRange, T endRange)
    {
        if (node is null)
        {
            return;
        }

        bool nodeInLowerRange = startRange.CompareTo(node.Value) < 0;
        bool nodeInHigherRange = endRange.CompareTo(node.Value) > 0;
        if (nodeInLowerRange)
        {
            this.Range(node.Left, result, startRange, endRange);
        }
        if (startRange.CompareTo(node.Value) <= 0 && endRange.CompareTo(node.Value) >= 0)
        {
            result.Add(node.Value);
        }

        if (nodeInHigherRange)
        {
            this.Range(node.Right, result, startRange, endRange);
        }
    }

    private Node FindElement(T element)
    {
        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }

    private Node Insert(T element, Node node)
    {
        if (node == null)
        {
            node = new Node(element);
        }
        else if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }

        return node;
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }
}

