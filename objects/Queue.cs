using OldQueue = System.Collections.Queue;

class Queue<T>
{
    OldQueue queue = new OldQueue();

    public Queue()
    {
        queue = new OldQueue();
    }

    public bool IsEmpty()
    {
        return queue.Count == 0;
    }

    public T Remove()
    {
        return (T)queue.Dequeue()!;
    }

    public T Head()
    {
        return (T)queue.Peek()!;
    }

    public void Insert(T value)
    {
        queue.Enqueue(value);
    }

    public override string ToString()
    {
        return string.Join(", ", from s in queue.Cast<T>() select s);
    }
}