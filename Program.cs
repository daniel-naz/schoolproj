
class Program
{
    static void Main(string[] args)
    {
        var tree =
            new BinNode<int>(
                new BinNode<int>(
                    new BinNode<int>(
                        new BinNode<int>(1), 2, new BinNode<int>(3)
                    ),
                    4,
                    new BinNode<int>(
                        new BinNode<int>(5), 6, new BinNode<int>(7)
                    )
                ),
                8,
                new BinNode<int>(
                    new BinNode<int>(
                        new BinNode<int>(9), 10, new BinNode<int>(11)
                    ),
                    12,
                    new BinNode<int>(
                        new BinNode<int>(13), 14, new BinNode<int>(15)
                    )
                )
            );

        var n1 = new BinNode<int>(1);
        var n2 = new BinNode<int>(2);
        var n3 = new BinNode<int>(3);
        var n4 = new BinNode<int>(4);
        var n5 = new BinNode<int>(5);

        n1.SetRight(n2); n2.SetLeft(n1);
        n2.SetRight(n3); n3.SetLeft(n2);
        n3.SetRight(n4); n4.SetLeft(n3);
        n4.SetRight(n5); n5.SetLeft(n4);

        // close the list properly (both directions consistent)
        n5.SetRight(n1);
        n1.SetLeft(n5);

        System.Console.WriteLine(n2);
    }
}