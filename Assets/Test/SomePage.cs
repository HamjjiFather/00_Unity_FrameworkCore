using System.Linq;
using KKSFramework;
using KKSFramework.DataBind;
using UnityEngine;
using UnityEngine.UI;

public class SomePage : MonoBehaviour, IResolveTarget
{
    #region Fields & Property

    public Context context;

#pragma warning disable CS0649

    [Resolver]
    private Text _scoreText;

    [Resolver]
    private GameObject[] _textObj;

    [Resolver ("Elements")]
    private SomeElement[] _someElement;

#pragma warning restore CS0649

    #endregion


    #region UnityMethods

    private void Start ()
    {
        Debug.Log (_textObj.Length);
        _textObj[0].name = "ASD";
        _someElement.Foreach (x => x.Debug ());
    }

    #endregion


    #region Methods

    #endregion


    #region EventMethods

    #endregion
}