using System;
using UnityEngine;

[Serializable]
public class A
{
    public int a;
}

[Serializable]
public class B : A
{
    public int b;
}

[Serializable]
public class C : A
{
    public int c;
}

[Serializable]
public class D<T> : A
{
    public T d;
}

public class TestSerializeReference : MonoBehaviour
{
    [SerializeReference]
    public A a = null;

    public D<int> d = new D<int>();

    public bool changeType = false;

    private void OnValidate()
    {
        if (changeType)
        {
            if (a == null || a is D<int>)
            {
                a = new A();
            }
            else if (a is C)
            {
                a = new D<int>();
            }
            else if (a is B)
            {
                a = new C();
            }
            else
            {
                a = new B();
            }

            changeType = false;
        }
    }
}
