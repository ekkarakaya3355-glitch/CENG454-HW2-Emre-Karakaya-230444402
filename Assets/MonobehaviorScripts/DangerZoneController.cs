using System.Collections;
using UnityEngine;

public class DangerZoneController : MonoBehaviour
{
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private float missileDelay = 5f;

    private Coroutine activeCountdown;

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        examManager.EnterDangerZone();

        if (activeCountdown != null)
            StopCoroutine(activeCountdown);

        activeCountdown = StartCoroutine(StartMissileCountdown());
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

        examManager.ExitDangerZone();
    }

    private IEnumerator StartMissileCountdown()
    {
        yield return new WaitForSeconds(missileDelay);

        examManager.SetMissileActive(true);
        Debug.Log("Missile should launch now.");

        activeCountdown = null;
    }
}