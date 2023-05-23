using UnityEngine;
using TMPro;
using ScriptableObjects.Variables;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private IntVariable score;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        if (GameManager.Instance is not null)
            GameManager.Instance.OnScoreUpdate?.AddListener(UpdateDisplay);
        
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        text.text = $"Score : {score.value}";
    }
}
