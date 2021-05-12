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
        private QuestProgressModelBase NowProgressModel => QuestProgresses[_progressIndex];


        /// <summary>
        /// 현재 단계 요구 값.
        /// </summary>
        public float NowProgressReqValue => NowProgressModel.ReqProgressValue;

        /// <summary>
        /// 퀘스트 완료시 호출 액션.
        /// 완료 후 반복되는 경우에도 호출됨.
        /// </summary>
        private Action _onCompleteQuestAction;
        
        /// <summary>
        /// 퀘스트 반복시 호출 액션.
        /// 값은 반복 처리 완료 후 현재 진행 값을 리턴한다.
        /// </summary>
        private Action<float> _onIteratedQuestAction;

        /// <summary>
        /// 퀘스트 종료시 호출 액션.
        /// 상태 구분없이 퀘스트가 끝날 경우 호출되며 성공시에는 반복이 되지 않을 경우 호출됨. 
        /// </summary>
        private Action _onFinishQuestAction;

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
        /// 현재 단계 진행 값.
        /// </summary>
        public float NowProgressValue => _nowProgressValue;
        
        /// <summary>
        /// 현재 단계 진행도 (0 ~ 1f).
        /// </summary>
        public float NowProgressRatio => _nowProgressValue / NowProgressModel.ReqProgressValue;

        /// <summary>
        /// 전체 단계 진행도 (0 ~ 1).
        /// </summary>
        public float TotalProgressRatio => ProgressIndexForView / (float) QuestProgresses.Count;

        /// <summary>
        /// 모든 단계의 퀘스트 진행도.
        /// </summary>
        public float TotalProgressValue => QuestProgresses.Take (_progressIndex).Sum (q => q.ReqProgressValue) +
                                           _nowProgressValue;

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
        private float _nowProgressValue;

        /// <summary>
        /// 구독 전달.
        /// </summary>
        private readonly Subject<QuestModelModule> _subscribeQuestModule = new Subject<QuestModelModule> ();

        #endregion


        #region Methods

        public void AcceptQuest ()
        {
            if (QuestState != QuestState.NoAccept)
                return;

            QuestState = QuestState.Accept;
            NowProgressModel.ReachProgress ();
        }


        public bool CompleteQuest ()
        {
            if (QuestState != QuestState.WaitForComplete)
                return false;

            QuestState = QuestState.Complete;
            NowProgressModel.CompleteProgress ();
            _onCompleteQuestAction.CallSafe ();

            if (!_iterateQuest)
            {
                _subscribeQuestModule.DisposeSafe ();
                _onFinishQuestAction.CallSafe ();
                return true;
            }

            IterationQuest ();
            return QuestState == QuestState.WaitForComplete;
        }


        /// <summary>
        /// 퀘스트가 반복적으로 수행될 경우 초과된 값만큼 반복 완료 한다.
        /// </summary>
        /// <returns></returns>
        public int CompleteIterally ()
        {
            if (!_iterateQuest)
                return 0;
            
            var count =  (int)(_nowProgressValue / TotalReqProgressValue);
            var remainValue = _nowProgressValue % TotalReqProgressValue;
            
            QuestState = QuestState.Accept;
            QuestProgresses.ForEach (q => { q.ResetProgress (); });

            _progressIndex = 0;
            _nowProgressValue = 0;
            NowProgressModel.ReachProgress ();
            AddProgressValue (remainValue);
            _onIteratedQuestAction.CallSafe (_nowProgressValue);
            _subscribeQuestModule.OnNext (this);

            return count;
        }


        /// <summary>
        /// 퀘스트 포기.
        /// </summary>
        public void DeclineQuest ()
        {
            if (!Utility.Or (QuestState, QuestState.Accept, QuestState.WaitForComplete))
                return;

            QuestState = QuestState.Decline;
            _onFinishQuestAction.CallSafe ();
        }
        
        
        /// <summary>
        /// 퀘스트 실패.
        /// </summary>
        public void FailureQuest ()
        {
            if (!Utility.Or (QuestState, QuestState.Accept, QuestState.WaitForComplete))
                return;

            QuestState = QuestState.Failure;
            _onFinishQuestAction.CallSafe ();
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
                _nowProgressValue += value;
                return;
            }

            _nowProgressValue += value;
            do
            {
                var completeProgress = NowProgressModel.SetProgress (_nowProgressValue);
                if (completeProgress)
                    CheckNextProgress ();
                else
                {
                    _subscribeQuestModule.OnNext (this);
                    break;
                }
            } while (true);
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
            if (!isCompleteAllProgress)
            {
                _subscribeQuestModule.OnNext (this);
                return false;
            }
            NowProgressModel.CompleteProgress ();
            QuestState = QuestState.WaitForComplete;
            _subscribeQuestModule.OnNext (this);
            return true;
        }


        /// <summary>
        /// 다음 단계 세팅.
        /// </summary>
        private void SetNextProgress ()
        {
            var nextValue = _resetValueOnNextProgress ? 0 : _nowProgressValue - NowProgressReqValue;
            _progressIndex = Mathf.Max (_progressIndex + 1, ProgressLength - 1);

            NowProgressModel.ReachProgress (nextValue);
            _nowProgressValue = nextValue;
            _subscribeQuestModule.OnNext (this);
        }


        /// <summary>
        /// 퀘스트 반복 처리.
        /// </summary>
        private void IterationQuest ()
        {
            QuestState = QuestState.Accept;
            QuestProgresses.ForEach (q => { q.ResetProgress (); });

            var remainValue = _resetValueOnInterateQuest ? 0 : _nowProgressValue - NowProgressReqValue;
            _progressIndex = 0;
            _nowProgressValue = 0;
            NowProgressModel.ReachProgress ();
            AddProgressValue (remainValue);
            _onIteratedQuestAction.CallSafe (_nowProgressValue);
            _subscribeQuestModule.OnNext (this);
        }


        public void Subscribe (Action<QuestModelModule> onNext)
        {
            _subscribeQuestModule.Subscribe (onNext);
        }

        #endregion
    }
}