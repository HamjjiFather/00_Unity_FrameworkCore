using KKSFramework.DataBind;
using KKSFramework.Navigation;
using UnityEngine;

public class SomePage : MonoBehaviour, IResolveTarget
{
    #region Fields & Property

    public Sprite sprite;
    
#pragma warning disable CS0649

    [Resolver]
    private Property<Color> _colors;

#pragma warning restore CS0649

    #endregion


    #region UnityMethods

    private void Start ()
    {
        _colors.Value = Color.red;
    }

    #endregion


    #region Methods

    #endregion


    #region EventMethods

    #endregion
}