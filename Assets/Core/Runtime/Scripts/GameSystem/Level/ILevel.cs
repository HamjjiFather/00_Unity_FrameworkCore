namespace KKSFramework.GameSystem
{
    public interface ILevelCore
    {
        public int Level { get; set; }

        public int MaxLevel { get; set; }
    }


    /// <summary>
    ///     레벨 인터페이스.
    /// </summary>
    public interface ILevel : ILevelCore
    {
        #region Fields & Property

        #endregion


        #region Methods

        public void SetLevel (int level);

        public void AddLevel (int levelAmount);

        public void SetMaxLevel (int maxLevel);

        public void AddMaxLevel (int maxLevelAmount);

        #endregion
    }


    /// <summary>
    ///     경험치 기반의 레벨 인터페이스.
    /// </summary>
    public interface ILevelBasedOnExp : ILevelCore
    {
        #region Fields & Property

        public int Exp { get; set; }

        public int RequireExp { get; }

        #endregion


        #region Methods

        public void SetExp (int expAmount);

        public bool VariExp (int expAmount);

        #endregion
    }
}