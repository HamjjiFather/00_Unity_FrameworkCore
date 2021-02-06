using KKSFramework.DataBind;
using KKSFramework.Management;
using UnityEngine;

namespace KKSFramework.Sound
{
    /// <summary>
    /// 사운드 관리 컴포넌트.
    /// </summary>
    public class SoundPlayComponent : ComponentBase, IResolveTarget
    {
        #region Constructor

        #endregion


        #region Fields & Property

#pragma warning disable CS0649

        /// <summary>
        /// 타입에 따른 오디오 소스 리턴.
        /// </summary>
        public AudioSource AudioSource (SoundType soundType)
        {
            return soundType == SoundType.Bgm ? _audioSourceBGM : _audioSourceSfx;
        }

        /// <summary>
        /// 배경음 실행 오디오 소스.
        /// </summary>
        [Resolver]
        private AudioSource _audioSourceBGM;
        
        public AudioSource AudioSourceBGM => _audioSourceBGM;

        
        /// <summary>
        /// 효과음 실행 오디오 소스.
        /// </summary>
        [Resolver]
        private AudioSource _audioSourceSfx;

        public AudioSource AudioSourceSfx => _audioSourceSfx;

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        #endregion


        #region EventMethods

        #endregion
    }
}