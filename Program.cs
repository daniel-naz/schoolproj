
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

        // 5-node doubly linked list: 1 <-> 2 <-> 3 <-> 4 <-> 5
        var n1 = new BinNode<int>(1);
        var n2 = new BinNode<int>(2);
        var n3 = new BinNode<int>(3);
        var n4 = new BinNode<int>(4);
        var n5 = new BinNode<int>(5);

        n1.SetRight(n2); n2.SetLeft(n1);
        n2.SetRight(n3); n3.SetLeft(n2);
        n3.SetRight(n4); n4.SetLeft(n3);
        n4.SetRight(n5); n5.SetLeft(n4);
        n5.SetRight(n1);

        var a = new BinNode<int>(1);
        var b = new BinNode<int>(2);
        var c = new BinNode<int>(3);
        var d = new BinNode<int>(4);
        var e = new BinNode<int>(5);

        // Bidirectional links
        a.SetLeft(b); b.SetRight(a);   // OK
        a.SetRight(c); c.SetLeft(a);    // OK

        // Diamond: B and C both lead to D
        b.SetRight(d);                  // BROKEN back-pointer on purpose (D.left != B)
        c.SetRight(d); d.SetLeft(c);    // OK

        // Tail + cycle
        d.SetRight(e); e.SetLeft(d);    // OK
        e.SetRight(b); b.SetLeft(e);    // OK, creates cycle E -> B -> ... with B.left = E

        // Test (your PrintGeneralGraph expects a visited set)
        Console.WriteLine(a);

        //System.Console.WriteLine(n1);
    }
}