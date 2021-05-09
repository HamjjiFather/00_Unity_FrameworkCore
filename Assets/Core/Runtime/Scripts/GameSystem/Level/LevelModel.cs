using UniRx;

namespace KKSFramework.GameSystem
{
    public class LevelModel : ILevel
    {
        #region Fields & Property

        public int Level { get; set; }

        public int MaxLevel { get; set; }

        public readonly ReactiveCommand<(int, int)> LevelChangedCommand = new ReactiveCommand<(int, int)> ();

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetLevel (int level)
        {
            Level = level;
            LevelChangedCommand?.Execute ((MaxLevel, Level));
        }


        public void AddLevel (int levelAmount)
        {
            Level += levelAmount;
            LevelChangedCommand?.Execute ((MaxLevel, Level));
        }


        public void SetMaxLevel (int maxLevel)
        {
            MaxLevel = maxLevel;
            LevelChangedCommand?.Execute ((MaxLevel, Level));
        }


        public void AddMaxLevel (int maxLevel)
        {
            MaxLevel += maxLevel;
            LevelChangedCommand?.Execute ((MaxLevel, Level));
        }


        public bool IsMaxLevel => Level == MaxLevel;

        #endregion


        #region EventMethods

        #endregion
    }
}