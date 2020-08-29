using KKSFramework.DataBind;
using UnityEngine;

public class SomePage : MonoBehaviour, IResolveTarget
{
    #region Fields & Property

    public Context context;

    public Sprite sprite;
#pragma warning disable CS0649
    
    [Resolver]
    private Property<string> _strings;

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

    public void ChangeString (string value)
    {
        _strings.Value = value;
    }
    

    #endregion


    #region EventMethods

    #endregion
}