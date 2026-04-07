using UnityEngine;
using TMPro;

public class FlightExamManager : MonoBehaviour
{
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private TMP_Text missionText;
    [SerializeField] private AudioSource successAudioSource;

    private bool hasTakenOff = false;
    private bool hasEnteredDangerZone = false;
    private bool missileCountdownActive = false;
    private bool missileActive = false;
    private bool threatCleared = false;
    private bool missionComplete = false;
    private bool isRespawning = false;

    void Start()
    {
        if (statusText != null)
            statusText.text = "";

        if (missionText != null)
            missionText.text = "";
    }

    public void EnterDangerZone()
    {
        if (missionComplete || isRespawning)
            return;

        hasEnteredDangerZone = true;
        missileCountdownActive = true;

        if (statusText != null)
            statusText.text = "Entered a Dangerous Zone!";

        if (missionText != null)
            missionText.text = "Danger detected. Stay alert.";
    }

    public void ExitDangerZone()
    {
        if (missionComplete || isRespawning || !hasEnteredDangerZone)
            return;

        missileCountdownActive = false;
        missileActive = false;
        threatCleared = true;
        hasEnteredDangerZone = false;

        if (statusText != null)
            statusText.text = "";

        if (missionText != null)
            missionText.text = "Threat cleared. Landing is now allowed.";
    }

    public void SetMissileActive(bool value)
    {
        missileActive = value;
        missileCountdownActive = false;
    }

    public void MarkTakeoff()
    {
        if (missionComplete || isRespawning)
            return;

        hasTakenOff = true;

        if (missionText != null && !hasEnteredDangerZone)
            missionText.text = "Takeoff complete. Proceed to danger zone.";
    }

    public void TryCompleteMission()
    {
        if (missionComplete || isRespawning)
            return;

        if (!hasTakenOff)
            return;

        if (!threatCleared)
        {
            if (missionText != null)
                missionText.text = "Mission incomplete. Clear the threat first.";
            return;
        }

        missionComplete = true;

        if (statusText != null)
            statusText.text = "Mission Complete!";

        if (missionText != null)
            missionText.text = "Successful landing completed.";

        if (successAudioSource != null)
            successAudioSource.Play();
    }

    public void ShowGameOver()
    {
        if (statusText != null)
            statusText.text = "Game Over";

        if (missionText != null)
            missionText.text = "Missile hit the aircraft.Respawning in 5 seconds...";
    }

    public void SetRespawning(bool value)
    {
        isRespawning = value;
    }

    public bool IsRespawning()
    {
        return isRespawning;
    }

    public void ResetMissionState()
    {
        hasTakenOff = false;
        hasEnteredDangerZone = false;
        missileCountdownActive = false;
        missileActive = false;
        threatCleared = false;
        missionComplete = false;

        if (statusText != null)
            statusText.text = "";

        if (missionText != null)
            missionText.text = "";
    }

    public bool IsMissionComplete()
    {
        return missionComplete;
    }
}