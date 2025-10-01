class Queue<T>
{
    private Node<T>? last;
    private Node<T>? first;

    public T Remove()
    {
        T value = first!.GetValue();
        first = first.GetNext();
        if (first == null) last = null;
        return value;
    }

    public T Head()
    {
        return first!.GetValue();
    }

    public void Insert(T value)
    {
        var node = new Node<T>(value);
        if (last == null)
        {
            first = last = node;
        }
        else
        {
            last.SetNext(node);
            last = node;
        }
    }

    public override string ToString()
    {
        if (first == null) return "[]";
        return $"[{string.Join(", ", first.Enumerate())}]";
    }

    public bool IsEmpty() => first == null;
}