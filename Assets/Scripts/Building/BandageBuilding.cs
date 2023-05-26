using UnityEngine;

public class BandageBuilding : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.OnBuildingDestroyed(gameObject);
        Destroy(gameObject);
    }
}
