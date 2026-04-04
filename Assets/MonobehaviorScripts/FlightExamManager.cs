using UnityEngine;
using TMPro;

public class FlightExamManager : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text missionText;

    private bool hasEnteredDangerZone = false;
    private bool missileCountdownActive = false;
    private bool missileActive = false;
    private bool threatCleared = false;

    public void EnterDangerZone()
    {
        hasEnteredDangerZone = true;
        missileCountdownActive = true;

        if (statusText != null)
            statusText.text = "Entered a Dangerous Zone!";

        if (missionText != null)
            missionText.text = "Danger detected. Stay alert.";
    }

    public void ExitDangerZone()
    {
        missileCountdownActive = false;
        missileActive = false;
        threatCleared = true;

        if (statusText != null)
            statusText.text = "";

        if (missionText != null)
            missionText.text = "Threat cleared. You may continue.";
    }

    public void SetMissileActive(bool value)
    {
        missileActive = value;
        missileCountdownActive = false;
    }

    public bool IsMissileCountdownActive()
    {
        return missileCountdownActive;
    }

    public bool IsMissileActive()
    {
        return missileActive;
    }

    public bool HasEnteredDangerZone()
    {
        return hasEnteredDangerZone;
    }

    public bool IsThreatCleared()
    {
        return threatCleared;
    }
}