using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    public class SomePage : MonoBehaviour, IBinder
    {
        #region Fields & Property

#pragma warning disable CS0649

        [ResolveUI("ScoreText")]
        private Text _scoreText;

        [ResolveUI("ScoreTexts")]
        private Text[] _texts;

        [ResolveUI("Images")]
        private Image[] _images;

        [ResolveUI("Button")]
        private Button _button;
        
#pragma warning restore CS0649

        #endregion

        
        #region UnityMethods

        private void Start ()
        {
            _scoreText.text = "Good";
            _texts[0].text = "haha";
            _button.onClick.AddListener (() =>
            {
                _images[0].color = Color.red;
            });
            Debug.Log (_texts.Length);
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}