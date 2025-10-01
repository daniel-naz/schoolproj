
class Program
{
    static void Main(string[] args)
    {
        var n1 = new Node<int>(10);
        var n2 = new Node<int>(20);
        var n3 = new Node<int>(30);
        var n4 = new Node<int>(40);

        // chain them
        n1.SetNext(n2);
        n2.SetNext(n3);
        n3.SetNext(n4);

        System.Console.WriteLine(n1);
    }
}