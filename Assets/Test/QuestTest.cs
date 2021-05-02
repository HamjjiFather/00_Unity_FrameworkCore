using KKSFramework.GameSystem;
using UnityEngine;
using UnityEngine.UI;

public class QuestTest : MonoBehaviour
{
    private QuestModelModule _questModelModule;

    public Slider totalProgressGage;
    public Slider progressGage;

    public Text totalProgressText;
    public Text progressText;

    public Button add1Button;
    public Button add2Button;
    public Button add3Button;
    public Button completeProgress;
    public Button completeQuestManually;

    private void Awake ()
    {
        _questModelModule = new QuestModelModule (new[] {5f, 5, 10, 10, 15})
            .SetAutoCompleteProgress (true)
            .SetResetValueOnNextProgress (true)
            .SetIterate (true)
            .SetResetValueOnIterateQuest (true)
            .SetOnComplete (() => print ("Complete"));

        _questModelModule.AcceptQuest ();

        add1Button.onClick.AddListener (() => { _questModelModule.AddProgressValue (10f); });

        add2Button.onClick.AddListener (() => { _questModelModule.AddProgressValue (2f); });

        add3Button.onClick.AddListener (() => { _questModelModule.AddProgressValue (3f); });

        completeProgress.onClick.AddListener (() => { _questModelModule.CompleteProgressManually (); });

        completeQuestManually.onClick.AddListener (() => { _questModelModule.CompleteQuest (); });

        _questModelModule.Subscribe (v =>
        {
            progressText.text = $"{v}/{_questModelModule.NowProgressReqValue}";
            totalProgressText.text =
                $"{_questModelModule.ProgressIndexForView}/{_questModelModule.ProgressLength}\n{_questModelModule.TotalProgressValue}/{_questModelModule.TotalReqProgressValue}";
            totalProgressGage.value = _questModelModule.TotalProgressRatio;
            progressGage.value = _questModelModule.NowProgressRatio;
        });
    }

    void CompleteProgress (int action)
    {
        Debug.Log ($"Complete progress index: {action}");
    }

    void CompleteQuest ()
    {
        Debug.Log ($"Complete quest");
    }
}