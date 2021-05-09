using System;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.GameSystem
{
    public partial class QuestModelModule
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods

        #region Modular
        
        public QuestModelModule SetOnComplete (Action onComplete)
        {
            _onCompleteQuestAction = onComplete;
            return this;
        } 
        
        
        public QuestModelModule SetOnIterate (Action<float> onIterate)
        {
            _onIteratedQuestAction = onIterate;
            return this;
        }
        
        
        public QuestModelModule SetOnFailure (Action onFailure)
        {
            _onFailureQuestAction = onFailure;
            return this;
        } 
        

        public QuestModelModule SetAddProgressOnComplete (bool addProcess)
        {
            _addProgressOnComplete = addProcess;
            return this;
        }

        
        public QuestModelModule SetIterate (bool iteration)
        {
            _iterateQuest = iteration;
            return this;
        }
        
        
        public QuestModelModule SetResetValueOnIterateQuest (bool reset)
        {
            _resetValueOnInterateQuest = reset;
            return this;
        }
        
        
        public QuestModelModule SetAutoCompleteProgress (bool autoComplete)
        {
            _autoCompleteProgress = autoComplete;
            return this;
        }
        
        
        public QuestModelModule SetResetValueOnNextProgress (bool reset)
        {
            _resetValueOnNextProgress = reset;
            return this;
        }

        #endregion
        
        #endregion


        #region EventMethods

        #endregion
    }
}