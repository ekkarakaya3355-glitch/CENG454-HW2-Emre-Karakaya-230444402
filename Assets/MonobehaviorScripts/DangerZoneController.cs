using System.Collections;
using UnityEngine;

public class DangerZoneController : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private MissileLauncher missileLauncher;
    [SerializeField] private float missileDelay = 5f;
    [SerializeField] private AudioSource warningAudioSource;

    private Coroutine activeCountdown;

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (examManager != null && (examManager.IsMissionComplete() || examManager.IsRespawning()))
            return;

        if (warningAudioSource != null)
            warningAudioSource.Play();

        if (examManager != null)
            examManager.EnterDangerZone();

        if (activeCountdown != null)
            StopCoroutine(activeCountdown);

        activeCountdown = StartCoroutine(StartMissileCountdown(collision.transform));
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        if (activeCountdown != null)
        {
            StopCoroutine(activeCountdown);
            activeCountdown = null;
        }

        if (missileLauncher != null)
            missileLauncher.DestroyActiveMissile();

        if (examManager != null)
            examManager.ExitDangerZone();
    }

    private IEnumerator StartMissileCountdown(Transform player)
    {
        yield return new WaitForSeconds(missileDelay);

        if (examManager != null && examManager.IsRespawning())
        {
            activeCountdown = null;
            yield break;
        }

        if (missileLauncher != null)
        {
            missileLauncher.Launch(player);

            if (examManager != null)
                examManager.SetMissileActive(true);
        }

        activeCountdown = null;
    }
}