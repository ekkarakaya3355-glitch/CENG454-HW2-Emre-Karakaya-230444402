using System.Collections;
using UnityEngine;

public class AircraftThreatHandler : MonoBehaviour
{
    [SerializeField] private AudioSource hitAudioSource;
    [SerializeField] private FlightExamManager examManager;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay = 2f;

    private Rigidbody rb;
    private bool isHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.CompareTag("Missile"))
            return;

        if (isHit)
            return;

        isHit = true;

        if (hitAudioSource != null)
            hitAudioSource.Play();

        if (examManager != null)
        {
            examManager.ShowGameOver();
            examManager.SetRespawning(true);
        }

        Destroy(collision.gameObject);

        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        yield return new WaitForSeconds(respawnDelay);

        if (respawnPoint != null)
        {
            transform.SetPositionAndRotation(respawnPoint.position, respawnPoint.rotation);
        }

        if (examManager != null)
        {
            examManager.ResetMissionState();
        }

        yield return new WaitForSeconds(0.2f);

        if (examManager != null)
        {
            examManager.SetRespawning(false);
        }

        isHit = false;
    }
}