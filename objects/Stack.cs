using OldStack = System.Collections.Stack;

class Stack<T>
{
    OldStack stack = new OldStack();

    public Stack()
    {
        stack = new OldStack();
    }

    public bool IsEmpty()
    {
        return stack.Count == 0;
    }

    public T Pop()
    {
        return (T)stack.Pop()!;
    }

    public T Top()
    {
        return (T)stack.Peek()!;
    }

    public void Push(T value)
    {
        stack.Push(value);
    }

    public override string ToString()
    {
        return string.Join(", ", from s in stack.Cast<T>().Reverse() select s);
    }
}

class StackInt : Stack<int>
{

}