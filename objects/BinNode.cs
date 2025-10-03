class BinNode<T>
{
    private enum BinNodeType
    {
        SingleNode,
        DoubleLinkedList,
        LoopingDoubleLinkedList,
        Tree,
        GeneralGraph,
        Invalid
    }

    private T value;
    private BinNode<T>? left, right;

    public BinNode(T value)
    {
        this.value = value;
    }

    public BinNode(BinNode<T>? left, T value, BinNode<T>? right)
    {
        this.value = value;
        this.left = left;
        this.right = right;
    }

    public override string ToString()
    {
        return ToString(DetectType());
    }

    private BinNodeType DetectType()
    {
        if (left == null && right == null)
        {
            return BinNodeType.SingleNode;
        }

        HashSet<BinNode<T>> visited = new();

        bool looping = false;
        bool linkedList = true;

        void Traverse(BinNode<T>? curr, BinNode<T>? parent)
        {
            if (curr == null || !visited.Add(curr)) return;

            if (curr.left != null && curr.left != parent && visited.Contains(curr.left)) looping = true;
            else if (curr.left != null && curr.left.right != curr) linkedList = false;

            if (curr.right != null && curr.right != parent && visited.Contains(curr.right)) looping = true;
            else if (curr.right != null && curr.right.left != curr) linkedList = false;

            Traverse(curr.left, curr);
            Traverse(curr.right, curr);
        }

        Traverse(this, null);

        if (linkedList && !looping) return BinNodeType.DoubleLinkedList;
        if (linkedList && looping) return BinNodeType.LoopingDoubleLinkedList;
        if (!linkedList && !looping) return BinNodeType.Tree;
        if (!linkedList && looping) return BinNodeType.GeneralGraph;

        return BinNodeType.Invalid;
    }

    private string ToString(BinNodeType type)
    {
        if (type == BinNodeType.SingleNode) return $"null <- {value} -> null";
        if (type == BinNodeType.DoubleLinkedList) return PrintDoubleLinkedList(new());
        if (type == BinNodeType.Tree) return PrintTree("", true);
        if (type == BinNodeType.LoopingDoubleLinkedList) return PrintLoopingDoubleLinkedList(new(), null);



        return "Invalid";
    }

    private string PrintDoubleLinkedList(HashSet<BinNode<T>> visited)
    {
        visited.Add(this);

        bool goLeft = left != null && !visited.Contains(left);
        bool goRight = right != null && !visited.Contains(right);

        string leftStr = goLeft
            ? left!.PrintDoubleLinkedList(visited) + " <-> "
            : left == null ? "null <- " : "";

        string rightStr = goRight
            ? " <-> " + right!.PrintDoubleLinkedList(visited)
            : right == null ? " -> null" : "";

        return leftStr + value + rightStr;
    }

    private string PrintTree(string indent, bool last)
    {
        string s = indent + (last ? "└──" : "├──") + (value?.ToString() ?? "null") + "\n";
        string next = indent + (last ? "   " : "│  ");
        if (right != null) s += right.PrintTree(next, false);
        if (left != null) s += left.PrintTree(next, true);
        return s;
    }

    private string PrintLoopingDoubleLinkedList(HashSet<BinNode<T>> visited, BinNode<T>? parent)
    {
        //↺↻
        visited.Add(this);

        bool goLeft = left != null && !visited.Contains(left);
        bool goRight = right != null && !visited.Contains(right);

        string leftStr = goLeft
            ? left!.PrintLoopingDoubleLinkedList(visited, this) + " <-> "
            : left == null ? "null <- " : "";

        string rightStr = goRight
            ? " <-> " + right!.PrintLoopingDoubleLinkedList(visited, this)
            : right == null ? " -> null" : "";

        return leftStr + value + rightStr;
    }


    public void SetValue(T value) => this.value = value;
    public void SetLeft(BinNode<T>? left) => this.left = left;
    public void SetRight(BinNode<T>? right) => this.right = right;
    public T GetValue() => value;
    public BinNode<T>? GetLeft() => left;
    public BinNode<T>? GetRight() => right;
    public bool HasLeft() => left != null;
    public bool HasRight() => right != null;
}