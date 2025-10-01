class Node<T>
{
    private T value;
    private Node<T>? next;

    public Node(T value)
    {
        this.value = value;
    }

    public Node(T value, Node<T>? next) : this(value)
    {
        this.next = next;
    }

    public override string ToString()
    {
        return string.Join(" -> ", Enumerate());
    }

    public T GetValue() => value;
    public Node<T>? GetNext() => next;
    public bool HasNext() => next != null;
    public void SetValue(T value) => this.value = value;
    public void SetNext(Node<T> next) => this.next = next;

    public IEnumerable<T> Enumerate()
    {
        for (var p = this; p != null; p = p.GetNext())
        {
            yield return p.GetValue();
        }
    }
}