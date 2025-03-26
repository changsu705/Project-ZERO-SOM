using UnityEngine;
using UnityEngine.UI;

public class PatternLoopUI : MonoBehaviour
{
    public Paladin paladin;

    public Toggle slashToggle;
    public Toggle castSlashToggle;

    public float loopDelay = 2f;

    private string currentLoopPattern = null;

    private void Start()
    {
        slashToggle.onValueChanged.AddListener(OnSlashToggleChanged);
        castSlashToggle.onValueChanged.AddListener(OnCastSlashToggleChanged);
    }

    void OnSlashToggleChanged(bool isOn)
    {
        if (isOn)
        {
            castSlashToggle.isOn = false; // ´Ù¸¥ ÂÊ ²ô±â
            StartPatternLoop("Slash");
        }
        else if (!castSlashToggle.isOn)
        {
            StopPatternLoop();
        }
    }

    void OnCastSlashToggleChanged(bool isOn)
    {
        if (isOn)
        {
            slashToggle.isOn = false;
            StartPatternLoop("CastSlash");
        }
        else if (!slashToggle.isOn)
        {
            StopPatternLoop();
        }
    }

    void StartPatternLoop(string patternName)
    {
        currentLoopPattern = patternName;
        InvokeRepeating(nameof(LoopPattern), 0f, loopDelay);
    }

    void StopPatternLoop()
    {
        CancelInvoke(nameof(LoopPattern));
        currentLoopPattern = null;
    }

    void LoopPattern()
    {
        if (!string.IsNullOrEmpty(currentLoopPattern))
        {
            paladin.ChangePattern(currentLoopPattern);
        }
    }
}
