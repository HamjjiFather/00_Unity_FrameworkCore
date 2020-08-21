using KKSFramework.DataBind;
using UnityEngine;
using UnityEngine.UI;

public class SomeElement : MonoBehaviour, IResolveTarget
{
#pragma warning disable CS0649

    [Resolver ("Images")]
    private Image[] _images;

    [Resolver ("Button")]
    private Button _button;

#pragma warning restore CS0649

    private void Start ()
    {
        _button.onClick.AddListener (() =>
        {
            _images[0].color = Color.red;
            _images[1].color = Color.blue;
        });
    }

    public void Debug ()
    {
        UnityEngine.Debug.Log (gameObject.name);
    }
}
