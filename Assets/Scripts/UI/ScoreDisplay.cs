using UnityEngine;
using TMPro;
using ScriptableObjects.Variables;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private IntVariable score;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        GameManager.Instance.OnScoreUpdate.AddListener(UpdateDisplay);
    }

    private void UpdateDisplay()
    {
        text.text = $"Score : {score.value}";
    }
}
