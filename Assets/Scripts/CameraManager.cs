using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject _freeLookCam;
    [SerializeField] private GameObject _faceCam;

    private void OnEnable()
    {
        TimerSystem.TimerFinished += OnTimerFinished;
    }

    private void OnDisable()
    {
        TimerSystem.TimerFinished -= OnTimerFinished;
    }

    private void OnTimerFinished()
    {
        _freeLookCam.SetActive(false);
        _faceCam.SetActive(true);
    }
}
