class BinNode<T>
{
    private enum BinNodeType
    {
        SingleNode,
        DoubleLinkedList,
        LoopingDoubleLinkedList,
        Tree,
        LoopingTree,
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
        // Chcek single node
        if (left == null && right == null) return BinNodeType.SingleNode;

        var rightRunner = this;
        var rightWalker = this;
        bool rightCycle = false;
        var leftRunner = this;
        var leftWalker = this;
        bool leftCycle = false;

        bool isLeftList = true;
        bool isRightList = true;

        // Check looping double linked list
        while (rightRunner.right != null && rightRunner.right.right != null)
        {
            if (rightRunner.right.left != rightRunner) isRightList = false;

            rightRunner = rightRunner.right.right;
            rightWalker = rightWalker!.right;

            if (rightRunner == rightWalker) { rightCycle = true; break; }

        }

        while (leftRunner.left != null && leftRunner.left.left != null)
        {
            if (leftRunner.left.right != leftRunner) isLeftList = false;

            leftRunner = leftRunner.left.left;
            leftWalker = leftWalker!.left;

            if (leftRunner == leftWalker) { leftCycle = true; break; }
        }

        bool isLinkedList = isLeftList && isRightList;
        if (isLinkedList)
            return (rightCycle || leftCycle)
                ? BinNodeType.LoopingDoubleLinkedList
                : BinNodeType.DoubleLinkedList;


        return BinNodeType.Invalid;
    }

    private string ToString(BinNodeType type, HashSet<BinNode<T>>? visited = null)
    {
        if (type == BinNodeType.SingleNode)
        {
            return $"null<-({value})->null";
        }

        if (visited == null) visited = new HashSet<BinNode<T>>();
        else if (visited.Contains(this)) return "";
        visited.Add(this);

        if (type == BinNodeType.DoubleLinkedList)
        {
            System.Console.WriteLine("going left");
            string leftstr = (left != null && !visited.Contains(left))
               ? left.ToString(type, visited) + "<->"
               : "null<-";

            System.Console.WriteLine("going right");
            bool goRight = right != null && !visited.Contains(right);
            string rightstr = goRight
                ? right!.ToString(type, visited)       // no "<->" here
                : "->null";

            return leftstr + value + (goRight ? "<->" : "") + rightstr;
        }

        else if (type == BinNodeType.LoopingDoubleLinkedList)
        {
            // Choose a consistent next pointer (prefer right if it round-trips, else left)
            BinNode<T>? Next(BinNode<T> n) =>
                (n.right != null && n.right.left == n) ? n.right :
                (n.left != null && n.left.right == n) ? n.left : null;

            // Floyd cycle detection
            var slow = this;
            var fast = this;
            while (slow != null && fast != null)
            {
                slow = Next(slow);
                fast = Next(fast);
                if (fast != null) fast = Next(fast);
                if (slow != null && slow == fast) break;
            }
            if (slow == null || fast == null)
                return "[expected loop, none found]";

            // Find cycle entry (may not be 'this')
            var meet = slow!;
            var entry = this;
            while (entry != meet)
            {
                entry = Next(entry)!;
                meet = Next(meet)!;
            }

            // Build string without StringBuilder

            // 1) path from start to entry (if any)
            string s = "";
            var curr = this;
            while (curr != entry)
            {
                s += "[" + curr.value + "] <-> ";
                curr = Next(curr)!;
            }

            // 2) the cycle once, bracketed, then loop marker
            s += "(" + "[" + entry.value + "]";
            curr = Next(entry)!;
            while (curr != entry)
            {
                s += " <-> [" + curr.value + "]";
                curr = Next(curr)!;
            }
            s += ")↺";

            return s;
        }

        return "Invalid";
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