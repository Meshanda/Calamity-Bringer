using ScriptableObjects.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private FloatVariable _timerVariable;
    [SerializeField] private GameObject[] _edges;
    [SerializeField] private TrailRenderer _renderer;

    [Header("Data")]
    [SerializeField] private float _completeFigureTime;
    [Space]
    [SerializeField] private float _maxTrailWidth;
    [SerializeField] private AnimationCurve _trailWidthGrowthCurve;

    public void Launch()
    {
        StartCoroutine(TravelTrailHead());
    }

    private IEnumerator TravelTrailHead()
    {
        float fullTime = _timerVariable.value;

        if(_completeFigureTime == 0f || _edges.Length == 0)
        {
            Debug.LogError("Check the Data");
            yield break;
        }

        float sideTime = _completeFigureTime / _edges.Length;

        float progress = 0;

        while (_timerVariable.value >= 0)
        {
            progress += Time.deltaTime;

            int currentCorner = Mathf.FloorToInt(progress / sideTime % _edges.Length);
            int nextCorner = (currentCorner + 1) % _edges.Length;

            _renderer.transform.position = Vector3.Lerp(
                _edges[currentCorner].transform.position, 
                _edges[nextCorner].transform.position, 
                progress % sideTime / sideTime
            );

            _renderer.time = progress / fullTime * (_edges.Length + 1);
            _renderer.endColor = Color.Lerp(Color.white, Color.red, progress / fullTime);
            _renderer.widthMultiplier = _trailWidthGrowthCurve.Evaluate(progress / fullTime) * _maxTrailWidth;

            yield return null;
        }
    }
}
