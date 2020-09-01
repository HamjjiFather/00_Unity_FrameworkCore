using UnityEngine;
using KKSFramework.DataBind;

public class SomeData
{
    public string Good;

    public SomeData (string good)
    {
        Good = good;
    }
}

public class SomeElement : MonoBehaviour
{
#pragma warning disable CS0649

    public int value;

#pragma warning restore CS0649

    [Bind]
    public void A ()
    {
        Debug.Log ("A");
    }

    [Bind]
    public void B (int i)
    {
        value = i;
        Debug.Log (i);
    }

    [Bind]
    public void C (int i, int z)
    {
        Debug.Log (i + z);
    }

    [Bind]
    public int AA ()
    {
        return 0;
    }

    [Bind]
    public int BB (int i)
    {
        return i + 5;
    }

    public int CC (int i, int z)
    {
        return i + z;
    }

    [Bind]
    public SomeData SomeData (string str)
    {
        return new SomeData (str);
    }
}