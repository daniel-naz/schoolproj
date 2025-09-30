using System.Collections.Concurrent;

class Node<T>
{
    T value;
    Node<T>? next;

    public Node(T v)
    {
        value = v;
    }

    public Node(T v, Node<T> next) : this(v)
    {
        this.next = next;
    }

    public T GeValue()
    {
        return value;
    }

    public Node<T>? GetNext()
    {
        return next;
    }

    public bool HasNext()
    {
        return next != null;
    }

    public void SetValue(T v)
    {
        value = v;
    }

    public void SetNext(Node<T> next)
    {
        this.next = next;
    }

    public override string ToString()
    {
        return value?.ToString() + (next != null ? " -> " + next.ToString() : "");
    }
}