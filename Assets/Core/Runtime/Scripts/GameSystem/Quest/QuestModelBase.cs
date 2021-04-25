using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.DesignPattern;
using UniRx;
using UnityEngine;

namespace KKSFramework.GameSystem
{
    public class QuestModelBase : ModelBase
    {
        #region Constructor

        public QuestModelBase (List<float> reqProgressesQueue, Action<int> onCompleteProgressActon,
            Action onCompleteQuestAction)
        {
            NowProgressValue = new FloatReactiveProperty (0);
            ReqProgressesQueue = reqProgressesQueue;
            OnCompleteProgressActon = onCompleteProgressActon;
            OnCompleteQuestAction = onCompleteQuestAction;
            IsResetValueOnNextProgress = false;
            IsIterateQuest = false;
            IsResetValueOnInterateQuest = false;
            _progressIndex = 0;
        }


        public QuestModelBase (IEnumerable<float> reqProgressesQueue, Action<int> onCompleteProgressActon,
            Action onCompleteQuestAction, bool isResetValueOnNextProgress, bool isIterateQuest,
            bool isResetValueOnInterateQuest)
        {
            NowProgressValue = new FloatReactiveProperty (0);
            ReqProgressesQueue = new List<float> (reqProgressesQueue);
            OnCompleteProgressActon = onCompleteProgressActon;
            OnCompleteQuestAction = onCompleteQuestAction;
            IsResetValueOnNextProgress = isResetValueOnNextProgress;
            IsIterateQuest = isIterateQuest;
            IsResetValueOnInterateQuest = isResetValueOnInterateQuest;
            _progressIndex = 0;
        }


        public QuestModelBase (FloatReactiveProperty nowProgressValue, List<float> reqProgressesQueue,
            Action<int> onCompleteProgressActon, Action onCompleteQuestAction, Action onFailureQuestAction,
            int progressIndex, bool isResetValueOnNextProgress, bool isIterateQuest, bool isResetValueOnInterateQuest,
            bool isOnProgress)
        {
            NowProgressValue = nowProgressValue;
            ReqProgressesQueue = reqProgressesQueue;
            OnCompleteProgressActon = onCompleteProgressActon;
            OnCompleteQuestAction = onCompleteQuestAction;
            OnFailureQuestAction = onFailureQuestAction;
            _progressIndex = progressIndex;
            IsResetValueOnNextProgress = isResetValueOnNextProgress;
            IsIterateQuest = isIterateQuest;
            IsResetValueOnInterateQuest = isResetValueOnInterateQuest;
            IsOnProgress = isOnProgress;
        }

        #endregion


        #region Fields & Property

        /// <summary>
        /// 현재 진행 값.
        /// </summary>
        public FloatReactiveProperty NowProgressValue;

        /// <summary>
        /// 단계별 요구 진행 값.
        /// </summary>
        public readonly List<float> ReqProgressesQueue;

        /// <summary>
        /// 단계 만족시 호출 액션.
        /// </summary>
        public readonly Action<int> OnCompleteProgressActon;

        /// <summary>
        /// 퀘스트 완료시 호출 액션.
        /// </summary>
        public readonly Action OnCompleteQuestAction;

        /// <summary>
        /// 퀘스트 실패시 호출 액션.
        /// </summary>
        public readonly Action OnFailureQuestAction;

        /// <summary>
        /// 퀘스트 단계 완료 후 이전 단계의 초과된 값이 초기화 되는지.
        /// </summary>
        public bool IsResetValueOnNextProgress { get; private set; }

        /// <summary>
        /// 퀘스트 완료 후 자동으로 반복되는지.
        /// </summary>
        public bool IsIterateQuest { get; private set; }

        /// <summary>
        /// 퀘스트 완료 후 반복 시 이전 단계의 초과된 값이 초기화 되는지.
        /// </summary>
        public bool IsResetValueOnInterateQuest { get; private set; }

        /// <summary>
        /// 퀘스트 진행 중.
        /// </summary>
        public bool IsOnProgress { get; private set; }

        /// <summary>
        /// 현재 단계 요구치.
        /// </summary>
        public float CurrentRequireValue => ReqProgressesQueue[_progressIndex];

        /// <summary>
        /// 현재 단계가 완료되었는지.
        /// </summary>
        public bool IsCompleteProgress => ReqProgressesQueue.Any () &&
                                          _progressIndex < ReqProgressesQueue.Count &&
                                          NowProgressValue.Value >= CurrentRequireValue;

        /// <summary>
        /// 전체 단계 진행도 (0 ~ 1).
        /// </summary>
        public float TotalProgressRatio => ProgressIndexForView / (float) ReqProgressesQueue.Count;
        
        /// <summary>
        /// 현재 단계 진행도 (0 ~ 1).
        /// </summary>
        public float CurrentProgressRatio => NowProgressValue.Value / CurrentRequireValue;

        /// <summary>
        /// 총 진행 단계.
        /// </summary>
        public int ProgressLength => ReqProgressesQueue.Count;
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        /// <summary>
        /// 진행 단계.
        /// </summary>
        private int _progressIndex;

        /// <summary>
        /// 인식용 진행 단계.
        /// </summary>
        public int ProgressIndexForView => _progressIndex + 1;

        /// <summary>
        /// 퀘스트 완료 여부.
        /// </summary>
        public bool TotalProgressCompleted { get; private set; }

        #endregion


        #region Methods

        private void ResetQuest (float resetValue)
        {
            _progressIndex = 0;
            NowProgressValue.SetValueAndForceNotify (resetValue);
        }


        /// <summary>
        /// 퀘스트 완료 처리.
        /// </summary>
        public void CompleteQuest (float resetValue)
        {
            if (TotalProgressCompleted)
                return;
            
            if (!IsIterateQuest)
            {
                OnCompleteQuestAction.CallSafe ();
                TotalProgressCompleted = true;
                NowProgressValue.SetValueAndForceNotify (ReqProgressesQueue.Last());
                return;
            }

            OnCompleteQuestAction.CallSafe ();
            ResetQuest (resetValue);
        }


        /// <summary>
        /// 퀘스트 실패, 포기 처리.
        /// </summary>
        public void FailureQuest ()
        {
            OnFailureQuestAction.CallSafe ();
            ResetQuest (0);
            IsOnProgress = false;
        }


        /// <summary>
        /// 단계 요구치 추가.
        /// </summary>
        public virtual void AddProgress (float progressValue)
        {
            if (TotalProgressCompleted)
                return;
            
            var value = NowProgressValue.Value + progressValue;
            if (CurrentRequireValue > value)
            {
                NowProgressValue.SetValueAndForceNotify (value);
                return;
            }
            
            var nextValue = IsResetValueOnNextProgress ? 0 : value - CurrentRequireValue;
            OnNext (nextValue);
        }


        /// <summary>
        /// 단계 값만 추가.
        /// </summary>
        public virtual void AddProgressOnly (float progressValue)
        {
            if (TotalProgressCompleted)
                return;
            
            var curReqValue = ReqProgressesQueue[_progressIndex];
            NowProgressValue.Value += progressValue;
        }


        /// <summary>
        /// 강제로 단계를 완료함.
        /// 자동으로 단계 값은 초기화 됨.
        /// </summary>
        public virtual void CompleteProgress (float nextValue)
        {
            if (TotalProgressCompleted)
                return;
            
            OnNext (nextValue);
        }


        /// <summary>
        /// 다음 단계 처리.
        /// </summary>
        private void OnNext (float nextValue)
        {
            OnCompleteProgressActon.CallSafe (_progressIndex);

            if (_progressIndex + 1 >= ReqProgressesQueue.Count)
            {
                var resetValue = IsIterateQuest ? IsResetValueOnInterateQuest ? 0 : nextValue : 0;
                CompleteQuest (resetValue);
                return;
            }

            _progressIndex++;
            NowProgressValue.SetValueAndForceNotify (nextValue);
        }

        #endregion
    }
}