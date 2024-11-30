using UnityEngine;
using TMPro;

public class SpawnStatisticsRenderer : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _textMeshPro.text = "0 / 0 / 0";
    }

    private void OnEnable()
    {
        _spawner.ChangedCount += Render;
    }

    private void OnDisable()
    {
        _spawner.ChangedCount -= Render;
    }

    private void Render()
    {
        _textMeshPro.text = $"{_spawner.NumberOfObjectsSpawned} / {_spawner.NumberOfObjectsCreated} / {_spawner.NumberOfActiveObjects}";
    }
}
