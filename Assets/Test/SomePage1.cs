using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UnityEngine;

public class SomePage1 : MonoBehaviour, IResolveTarget
{
    #region Fields & Property

    public Sprite sprite;
    
#pragma warning disable CS0649

    [Resolver]
    private Property<string> _strings;

#pragma warning restore CS0649

    #endregion


    #region UnityMethods

    #endregion


    #region Methods

    public void ChangeString (string value)
    {
        _strings.Value = value;
    }
    

    #endregion


    #region EventMethods

    #endregion
}