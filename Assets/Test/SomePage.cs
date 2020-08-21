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

    [Resolver ("ScoreTexts")]
    private Text[] _texts;

    private Button[] _textButtons;

    [Resolver ("Elements")]
    private SomeElement[] _someElement;

#pragma warning restore CS0649

    #endregion


    #region UnityMethods

    private void Start ()
    {
        _textButtons = context.Resolve<GameObject[]> ("ScoreTexts").Select (x => x.GetComponent<Button> ()).ToArray ();
        
        _scoreText.text = "Good";
        _texts[0].text = "haha";
        _textButtons[0].onClick.AddListener (() =>
        {
            _textButtons[0].gameObject.SetActive (false);
        });
        _someElement.Foreach (x => x.Debug ());
    }

    #endregion


    #region Methods

    #endregion


    #region EventMethods

    #endregion
}