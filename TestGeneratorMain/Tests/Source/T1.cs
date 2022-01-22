using System;
using System.Collections.Generic;

public class T1
{
    public IEnumerable<int> Interface { get; private set; }

    public T1(IDisposable a, ICloneable b, int c, string d){}

    public int Function1(int e, int f)
    {
        return 0;
    }

    public void Function2(){}
}

public class T2
{
    public IEnumerable<int> Interface { get; private set; }

    public void Function1() { }

    public void Function2() { }
}
