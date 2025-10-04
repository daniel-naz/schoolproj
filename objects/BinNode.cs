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
        if (left == null && right == null) return BinNodeType.SingleNode;

        var visited = new HashSet<BinNode<T>>();
        bool looping = false;
        bool linkedlist = true;

        void Traverse(BinNode<T>? curr, BinNode<T>? parent)
        {
            if (curr == null || !visited.Add(curr)) return;

            // Doubly-linked invariants (reciprocity)
            if (curr.left != null && curr.left.right != curr) linkedlist = false;
            if (curr.right != null && curr.right.left != curr) linkedlist = false;

            // Undirected-cycle detection (ignore the edge we came from)
            if (curr.left != null && curr.left != parent && visited.Contains(curr.left)) looping = true;
            if (curr.right != null && curr.right != parent && visited.Contains(curr.right)) looping = true;

            Traverse(curr.left, curr);
            Traverse(curr.right, curr);
        }

        Traverse(this, null);

        if (linkedlist)
            return looping ? BinNodeType.LoopingDoubleLinkedList
                           : BinNodeType.DoubleLinkedList;

        return looping ? BinNodeType.GeneralGraph
                       : BinNodeType.Tree;
    }

    private string ToString(BinNodeType type)
    {
        if (type == BinNodeType.SingleNode) return $"null <- {value} -> null";
        if (type == BinNodeType.DoubleLinkedList) return PrintDoubleLinkedList(new());
        if (type == BinNodeType.Tree) return PrintTree("", true);
        if (type == BinNodeType.LoopingDoubleLinkedList) return PrintLoopingDoubleLinkedList();
        if (type == BinNodeType.GeneralGraph) return PrintGeneralGraph(new());

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

    private string PrintLoopingDoubleLinkedList()
    {
        HashSet<BinNode<T>> visited = new();
        string str = "";

        void BuildString(BinNode<T> curr)
        {
            if (!visited.Add(curr)) return;
            str += (str == "" ? "" : " <-> ") + $"{curr.value}";
            BuildString(curr.right!);
        };

        BuildString(this);

        return $"↻({str})↺";
    }

    private string PrintGeneralGraph(HashSet<BinNode<T>> visited)
    {
        // Collect connected component (BFS) and assign stable IDs
        var seen = new HashSet<BinNode<T>>();
        var order = new List<BinNode<T>>();
        var ids = new Dictionary<BinNode<T>, int>();
        var q = new Queue<BinNode<T>>();

        void Enq(BinNode<T>? n)
        {
            if (n != null && seen.Add(n))
            {
                ids[n] = ids.Count + 1;
                order.Add(n);
                q.Insert(n);
                visited.Add(n);
            }
        }

        Enq(this);
        while (!q.IsEmpty())
        {
            var u = q.Remove();
            Enq(u.left);
            Enq(u.right);
        }

        string Label(BinNode<T>? n) => n == null ? "null" : $"#{ids[n]}";
        string Val(BinNode<T> n) => n.value?.ToString() ?? "null";

        // Build simple table: Id | Value | Left | Right
        var rows = new List<string[]>
        {
            (["Id", "Value", "Left", "Right"])
        };
        foreach (var n in order)
        {
            rows.Add([$"#{ids[n]}", Val(n), Label(n.left), Label(n.right)]);
        }

        // Column widths
        int cols = rows[0].Length;
        int[] w = new int[cols];
        for (int c = 0; c < cols; c++)
        {
            int mx = 0;
            foreach (var r in rows) if (r[c].Length > mx) mx = r[c].Length;
            w[c] = mx;
        }

        string Sep()
        {
            string s = "+";
            for (int c = 0; c < cols; c++) s += new string('-', w[c] + 2) + "+";
            return s;
        }

        // Render
        string sep = Sep();
        var sb = new System.Text.StringBuilder();
        sb.AppendLine(sep);
        sb.Append("|");
        for (int c = 0; c < cols; c++) sb.Append(" ").Append(rows[0][c].PadRight(w[c])).Append(" |");
        sb.AppendLine();
        sb.AppendLine(sep);
        for (int r = 1; r < rows.Count; r++)
        {
            sb.Append("|");
            for (int c = 0; c < cols; c++) sb.Append(" ").Append(rows[r][c].PadRight(w[c])).Append(" |");
            sb.AppendLine();
        }
        sb.Append(sep);
        return sb.ToString();
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