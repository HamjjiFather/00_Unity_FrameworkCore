using System;
using System.Collections.Generic;
using System.Linq;
using KKSFramework.DesignPattern;
using UniRx;
using UnityEngine;

namespace KKSFramework.GameSystem
{
    public enum QuestState
    {
        /// <summary>
        /// 수락하지 않음.
        /// </summary>
        NoAccept,

        /// <summary>
        /// 수락.
        /// </summary>
        Accept,

        /// <summary>
        /// 거절.
        /// </summary>
        Decline,


        /// <summary>
        /// 완료 대기.
        /// </summary>
        WaitForComplete,

        /// <summary>
        /// 완료.
        /// </summary>
        Complete,

        /// <summary>
        /// 실패.
        /// </summary>
        Failure,
    }


    public enum QuestProgressState
    {
        /// <summary>
        /// 단계 도달 못함.
        /// </summary>
        NoReachable,

        /// <summary>
        /// 단계 진행 중.
        /// </summary>
        OnProgress,

        /// <summary>
        /// 완료 대기 중.
        /// </summary>
        WaitForComplete,

        /// <summary>
        /// 단계 완료.
        /// </summary>
        Complete,

        /// <summary>
        /// 단계 실패.
        /// </summary>
        Failure,
    }

    public partial class QuestModelModule : ModelBase
    {
        #region Constructor

        public QuestModelModule (IEnumerable<float> progressValues)
        {
            QuestProgresses = progressValues.Select ((p, i) => new QuestProgressModelBase (i, p)).ToList ();
        }

        #endregion


        #region Fields & Property

        #region Core.

        /// <summary>
        /// 퀘스트 진행 상태.
        /// </summary>
        public QuestState QuestState = QuestState.NoAccept;

        /// <summary>
        /// 진행 단계들.
        /// </summary>
        public readonly List<QuestProgressModelBase> QuestProgresses;

        /// <summary>
        /// 현재 진행 중인 단계들.
        /// </summary>
        public QuestProgressModelBase NowProgressModel => QuestProgresses[_progressIndex];


        /// <summary>
        /// 현재 단계 요구 값.
        /// </summary>
        public float NowProgressReqValue => NowProgressModel.ReqProgressValue;

        /// <summary>
        /// 퀘스트 완료시 호출 액션.
        /// </summary>
        private Action _onCompleteQuestAction;

        /// <summary>
        /// 퀘스트 실패시 호출 액션.
        /// </summary>
        private Action _onFailureQuestAction;

        #endregion


        #region Progress

        /// <summary>
        /// 퀘스트 단계가 자동으로 완료 처리 되는지.
        /// 아닐 경우 단계 완료 함수 접근을 통해 완료해 주어야 한다.
        /// </summary>
        private bool _autoCompleteProgress;

        /// <summary>
        /// 퀘스트 단계 완료 후 이전 단계의 초과된 값이 초기화 되는지.
        /// </summary>
        private bool _resetValueOnNextProgress;

        #endregion


        #region Complete

        /// <summary>
        /// 퀘스트 완료 / 퀘스트 단계 완료 후에도 진행도가 추가되는지.
        /// </summary>
        private bool _addProgressOnComplete;

        /// <summary>
        /// 퀘스트 완료 후 자동으로 반복되는지.
        /// </summary>
        private bool _iterateQuest;

        /// <summary>
        /// 퀘스트 완료 후 반복 시 이전 단계의 초과된 값이 초기화 되는지.
        /// </summary>
        private bool _resetValueOnInterateQuest;

        #endregion


        /// <summary>
        /// 총 단계 수량.
        /// </summary>
        public int ProgressLength => QuestProgresses.Count;

        /// <summary>
        /// 현재 단계 진행도 (0 ~ 1).
        /// </summary>
        public float NowProgressRatio => _nowProgressValue.Value / NowProgressModel.ReqProgressValue;

        /// <summary>
        /// 전체 단계 진행도 (0 ~ 1).
        /// </summary>
        public float TotalProgressRatio => ProgressIndexForView / (float) QuestProgresses.Count;

        /// <summary>
        /// 모든 단계의 퀘스트 진행도.
        /// </summary>
        public float TotalProgressValue => QuestProgresses.Take (_progressIndex).Sum (q => q.ReqProgressValue) +
                                           _nowProgressValue.Value;

        /// <summary>
        /// 모든 단계의 퀘스트 요구량.
        /// </summary>
        public float TotalReqProgressValue => QuestProgresses.Sum (q => q.ReqProgressValue);

        /// <summary>
        /// 모든 단계의 퀘스트 요구량 배열.
        /// </summary>
        public float[] TotalReqProgresses => QuestProgresses.Select (q => q.ReqProgressValue).ToArray ();

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
        /// 현재 단계 진행 값.
        /// </summary>
        private readonly FloatReactiveProperty _nowProgressValue = new FloatReactiveProperty (0);

        #endregion


        #region Methods

        public void AcceptQuest ()
        {
            if (QuestState != QuestState.NoAccept)
                return;

            QuestState = QuestState.Accept;
            NowProgressModel.ReachProgress ();
        }


        public void CompleteQuest ()
        {
            if (QuestState != QuestState.WaitForComplete)
                return;

            QuestState = QuestState.Complete;
            NowProgressModel.CompleteProgress ();
            _onCompleteQuestAction.CallSafe ();

            if (!_iterateQuest)
            {
                _nowProgressValue.DisposeSafe ();
                return;
            }

            IterationQuest ();
        }


        /// <summary>
        /// 퀘스트 포기.
        /// </summary>
        public void DeclineQuest ()
        {
            if (!Utility.Or (QuestState, QuestState.Accept, QuestState.WaitForComplete))
                return;

            QuestState = QuestState.Decline;
            _onFailureQuestAction.CallSafe ();
        }


        /// <summary>
        /// 현재 단계 수동 완료.
        /// 이미 완료된 단계를 완료해야함.
        /// </summary>
        public void CompleteProgressManually ()
        {
            if (NowProgressModel.QuestProgressState != QuestProgressState.WaitForComplete)
                return;

            if (CompleteProgressProcess ())
                return;

            SetNextProgress ();
        }


        /// <summary>
        /// 단계 값 추가.
        /// </summary>
        public void AddProgressValue (float value)
        {
            if (value.IsZero ())
                return;

            if (QuestState == QuestState.Complete && _addProgressOnComplete)
            {
                _nowProgressValue.Value += value;
                return;
            }

            _nowProgressValue.Value += value;
            bool completeProgress;
            do
            {
                completeProgress = NowProgressModel.SetProgress (_nowProgressValue.Value);
                if (completeProgress)
                    CheckNextProgress ();
            } while (completeProgress);
        }


        /// <summary>
        /// 현재 단계 체크.
        /// </summary>
        private void CheckNextProgress ()
        {
            if (!_autoCompleteProgress)
            {
                NowProgressModel.WaitForCompleteProgress ();
                return;
            }

            if (CompleteProgressProcess ())
                return;

            SetNextProgress ();
        }


        /// <summary>
        /// 현재 단계 완료.
        /// 모든 단계 완료 여부 리턴.
        /// </summary>
        private bool CompleteProgressProcess ()
        {
            // 모든 단계가 완료됨.
            var isCompleteAllProgress = _progressIndex >= QuestProgresses.Count - 1;
            if (!isCompleteAllProgress) return false;
            NowProgressModel.CompleteProgress ();
            QuestState = QuestState.WaitForComplete;
            return true;
        }


        /// <summary>
        /// 다음 단계 세팅.
        /// </summary>
        private void SetNextProgress ()
        {
            var nextValue = _resetValueOnNextProgress ? 0 : _nowProgressValue.Value - NowProgressReqValue;
            _progressIndex = Mathf.Max (_progressIndex + 1, ProgressLength - 1);

            NowProgressModel.ReachProgress (nextValue);
            _nowProgressValue.Value = nextValue;
        }


        /// <summary>
        /// 퀘스트 반복 처리.
        /// </summary>
        private void IterationQuest ()
        {
            QuestState = QuestState.Accept;
            QuestProgresses.ForEach (q => { q.ResetProgress (); });

            var remainValue = _resetValueOnInterateQuest ? 0 : _nowProgressValue.Value - NowProgressReqValue;
            _progressIndex = 0;
            _nowProgressValue.Value = 0;
            NowProgressModel.ReachProgress ();
            AddProgressValue (remainValue);
        }


        public void Subscribe (Action<float> onNext)
        {
            _nowProgressValue.Subscribe (onNext);
        }

        #endregion
    }
}