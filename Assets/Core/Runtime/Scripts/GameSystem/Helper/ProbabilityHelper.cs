using System.Collections.Generic;
using System.Linq;
using KKSFramework;
using UnityEngine;

namespace KKSFramework.GameSystem
{
    public class ProbabilityHelper : Singleton<ProbabilityHelper>
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        /// <summary>
        /// 단일 확률에 해당하는지.
        /// </summary>
        private bool IsRelevant (int percentage, int probability)
        {
            var grantedProbability = Random.Range (1, 1 + percentage);
            return grantedProbability <= probability;
        }
        
        
        /// <summary>
        /// 단일 확률에 해당하는지.
        /// </summary>
        private bool IsRelevant (float percentage, float probability)
        {
            var grantedProbability = Random.Range (1, 1 + percentage);
            return grantedProbability <= probability;
        }

        
        /// <summary>
        /// 단일 확률이 백분율에 해당하는지.
        /// </summary>
        public bool IsRelevantFor100 (int probability)
        {
            return IsRelevant (100, probability);
        }
        
        
        /// <summary>
        /// 단일 확률이 백분율에 해당하는지.
        /// </summary>
        public bool IsRelevantFor100 (float probability)
        {
            return IsRelevant (100f, probability);
        }


        /// <summary>
        /// 단일 확률이 만분율에 해당하는지.
        /// </summary>
        public bool IsRelevantFor10000 (int probability)
        {
            return IsRelevant (10000, probability);
        }


        /// <summary>
        /// 확률에 해당하는 인덱스 리턴.
        /// </summary>
        private int IndexOfRelevantOfProbability (int percentage, IEnumerable<int> probabilities)
        {
            var grantedProbability = Random.Range (1, 1 + percentage);
            var stackedProb = 0;

            return probabilities.Select ((prob, index) => (prob, index)).First (probTuple =>
            {
                stackedProb += probTuple.prob;
                return grantedProbability <= stackedProb;
            }).index;
        }


        /// <summary>
        /// 원하는 10의 제곱수분율로 확인함.
        /// </summary>
        public int RelevantOfProbability (int percentage, IEnumerable<int> probabilities)
        {
            return IndexOfRelevantOfProbability (percentage, probabilities);
        }


        /// <summary>
        /// 정수 백분율로 확인함.
        /// 확률 정보는 100까지 들어와야 한다.
        /// 확률 정보는 개별의 확률을 가지고 있으며 합산이 100가 되어야 한다. ex -> (10, 20, 30, 40).
        /// </summary>
        public int RelevantOfProbabilityFor100 (IEnumerable<int> probabilities)
        {
            return RelevantOfProbability (100, probabilities);
        }


        /// <summary>
        /// 정수 만분율로 확인함.
        /// 확률 정보는 10000까지 들어와야 한다.
        /// 확률 정보는 개별의 확률을 가지고 있으며 합산이 10000이 되어야 한다. ex -> (1000, 2000, 3000, 4000).
        /// </summary>
        public int RelevantOfProbabilityFor10000 (IEnumerable<int> probabilities)
        {
            return RelevantOfProbability (10000, probabilities);
        }

        #endregion


        #region EventMethods

        #endregion
    }
}