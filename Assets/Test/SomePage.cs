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

        private Button _button;
        
#pragma warning restore CS0649

        #endregion

        
        #region UnityMethods

        private void Start ()
        {
            _scoreText.text = "Good";
        }

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}