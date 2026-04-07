using UnityEngine;

public class MissionTriggerZone : MonoBehaviour
{
    public enum ZoneType
    {
        Takeoff,
        Landing
    }

    [SerializeField] private ZoneType zoneType;
    [SerializeField] private FlightExamManager examManager;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger touched by: " + other.name + " | tag: " + other.tag + " | zone: " + zoneType);

        if (!other.CompareTag("Player"))
            return;

        if (examManager == null)
        {
            Debug.Log("ExamManager is NULL");
            return;
        }

        if (zoneType == ZoneType.Takeoff)
        {
            Debug.Log("Calling MarkTakeoff()");
            examManager.MarkTakeoff();
        }
        else if (zoneType == ZoneType.Landing)
        {
            Debug.Log("Calling TryCompleteMission()");
            examManager.TryCompleteMission();
        }
    }
}