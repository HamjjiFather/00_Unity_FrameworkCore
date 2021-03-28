using System.Linq;
using UniRx;

namespace KKSFramework.GameSystem
{
    public class ExpLevelModel : ILevelBasedOnExp
    {
        #region Fields & Property
        
        public int Level { get; set; }
        
        public int MaxLevel { get; set; }
        
        public int Exp { get; set; }
        
        public int RequireExp  => RequireExps[Level];
        
        public int[] RequireExps { get; set; }

        /// <summary>
        /// 경험치 비율.
        /// </summary>
        public float ExpRatio => Exp / (float) RequireExp;

        /// <summary>
        /// 총 획득 경험치 리턴.
        /// </summary>
        public int TotalExp => RequireExps.Take (Level).Sum () + Exp;
        
        /// <summary>
        /// 총 필요 경험치 리턴.
        /// </summary>
        public int TotalRequireExp => RequireExps.Take (Level).Sum ();

        /// <summary>
        /// 경험치 변경 커맨드.
        /// </summary>
        public ReactiveCommand ExpChangeCommand = new ReactiveCommand ();
        
        /// <summary>
        /// 레벨업 커맨드.
        /// 변경 전 레벨, 변경 후 레벨.
        /// </summary>
        public ReactiveCommand<(int, int)> LevelUpCommand = new ReactiveCommand<(int, int)> ();
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public void SetLevelData ()
        {
            
        }

        #endregion


        #region EventMethods

        #endregion

        
        public void SetLevel (int level, int maxLevel)
        {
            Level = level;
            MaxLevel = maxLevel;
        }
        

        public void SetReqLevels (int[] reqExps)
        {
            RequireExps = reqExps;
        }
        

        public void SetModel (int level, int maxLevel, int[] reqExps)
        {
            SetLevel (level, maxLevel);
            SetReqLevels (reqExps);
        }
        

        /// <summary>
        /// 경험치 세팅.
        /// </summary>
        public void SetExp (int expAmount)
        {
            Exp = expAmount;
        }
        
        
        /// <summary>
        /// 경험치 변경.
        /// </summary>
        public bool VariExp (int expAmount)
        {
            Exp += expAmount;
            var isLevelUp = CheckLevel ();
            ExpChangeCommand.Execute ();

            return isLevelUp;
        }

        
        /// <summary>
        /// 레벨업 체크.
        /// </summary>
        public bool CheckLevel ()
        {
            var prevLevel = Level;
            var isLevelUp = false;
            while (Exp >= RequireExp)
            {
                Exp -= RequireExp;
                Level++;
                isLevelUp = true;
            }

            if (!isLevelUp)
                return false;

            LevelUpCommand.Execute ((prevLevel, Level));
            return true;
        }
    }
}