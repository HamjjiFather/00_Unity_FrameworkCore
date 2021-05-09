using System.Numerics;

namespace KKSFramework.GameSystem
{
    public static class BigIntegerHelper
    {
        #region Methods

        public static double DevideToDouble (BigInteger a, BigInteger b)
        {
            return (double) a / (double) b;
        }

        #endregion


        #region Fields & Property

        public static readonly BigInteger HundredBigInt = new BigInteger (100);

        public static readonly BigInteger TenThousandBigInt = new BigInteger (10000);

        #endregion


        #region UnityMethods

        #endregion


        #region EventMethods

        #endregion
    }
}