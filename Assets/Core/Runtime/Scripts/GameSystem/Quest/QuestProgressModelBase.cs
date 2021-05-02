using System;

namespace KKSFramework.GameSystem
{
    public class QuestProgressModelBase
    {
        public QuestProgressModelBase (int index, float reqProgressValue)
        {
            ProgressIndex = index;
            ReqProgressValue = reqProgressValue;
        }


        public QuestProgressModelBase (int index, float reqProgressValue, Action<int> progressCompleteAction, Action<int> progressFailureAction)
        {
            ProgressIndex = index;
            ReqProgressValue = reqProgressValue;
            ProgressCompleteAction = progressCompleteAction;
            ProgressFailureAction = progressFailureAction;
        }


        #region Fields & Property

        public QuestProgressState QuestProgressState { get; private set; } = QuestProgressState.NoReachable;

        /// <summary>
        /// 단계 인덱스.
        /// </summary>
        public int ProgressIndex;

        /// <summary>
        /// 단계 진행도.
        /// </summary>
        public float ProgressValue;

        /// <summary>
        /// 단계 요구 진행도.
        /// </summary>
        public float ReqProgressValue;

        /// <summary>
        /// 단계 완료 액션.
        /// </summary>
        public Action<int> ProgressCompleteAction;

        /// <summary>
        /// 단계 실패 액션.
        /// </summary>
        public Action<int> ProgressFailureAction;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        /// <summary>
        /// 단계 도달이 됨.
        /// </summary>
        public void ReachProgress (float value = 0f)
        {
            if (QuestProgressState != QuestProgressState.NoReachable)
                return;

            QuestProgressState = QuestProgressState.OnProgress;
            ProgressValue = value;
        }
        

        public bool SetProgress (float value)
        {
            if (value.IsZero ())
                return false;
            
            ProgressValue = value;
            
            // 단계 완료시 초과분을 넘김.
            if (!(ProgressValue >= ReqProgressValue) || QuestProgressState != QuestProgressState.OnProgress)
                return false;
            QuestProgressState = QuestProgressState.WaitForComplete;
            return true;

        }
        
        
        public void WaitForCompleteProgress ()
        {
            if (QuestProgressState != QuestProgressState.OnProgress) return;
            QuestProgressState = QuestProgressState.WaitForComplete;
        }
        

        public void CompleteProgress ()
        {
            if (QuestProgressState != QuestProgressState.OnProgress) return;
            QuestProgressState = QuestProgressState.Complete;
            ProgressCompleteAction.CallSafe (ProgressIndex);
        }


        public void FailureProgress ()
        {
            if (QuestProgressState != QuestProgressState.OnProgress) return;
            QuestProgressState = QuestProgressState.Failure;
            ProgressFailureAction.CallSafe (ProgressIndex);
        }


        public void RestartProgress ()
        {
            if (QuestProgressState == QuestProgressState.NoReachable)
                return;

            QuestProgressState = QuestProgressState.OnProgress;
            ProgressValue = 0;
        }


        public void ResetProgress ()
        {
            QuestProgressState = QuestProgressState.NoReachable;
            ProgressValue = 0;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}