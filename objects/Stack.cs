class Stack<T>
{
    private Node<T>? last;

    public T Pop()
    {
        T value = last!.GetValue();
        last = last.GetNext();
        return value;
    }

    public T Top()
    {
        return last!.GetValue();
    }

    public void Push(T value)
    {
        last = new Node<T>(value, last);
    }

    public override string ToString()
    {
        if (last == null) return "[]";
        return $"[{string.Join(", ", last.Enumerate())}]";
    }

    public bool IsEmpty() => last == null;
}

class StackInt : Stack<int>
{
    
}