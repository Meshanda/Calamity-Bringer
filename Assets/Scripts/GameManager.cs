using UnityEngine;
using ScriptableObjects.Variables;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }
    #endregion

    [SerializeField] private IntVariable score;
    [HideInInspector] public UnityEvent OnScoreUpdate;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        score.value = 0;
        OnScoreUpdate.Invoke();
    }

    public void GainPoints(int points)
    {
        score.value += points;
        OnScoreUpdate.Invoke();
    }
}
