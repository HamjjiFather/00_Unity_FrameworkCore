using KKSFramework.DataBind;
using UnityEngine;

public class SomePage1 : MonoBehaviour, IResolveTarget
{
    #region Fields & Property

#pragma warning disable CS0649

    [Resolver]
    private Property<string[]> _strings;



    #endregion


    #region UnityMethods

    private void Start ()
    {
        _strings.Value = new[]
        {
            "a",
            "b",
            "c",
            "d"
        };
    }

    #endregion


    #region Methods
    

    #endregion


    #region EventMethods

    #endregion
}