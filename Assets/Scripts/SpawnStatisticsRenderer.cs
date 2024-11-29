using UnityEngine;
using TMPro;

public class SpawnStatisticsRenderer : MonoBehaviour
{
    private const string AboutCubes = "Кубы( Все/Акт.) - ";
    private const string AboutBombs = "Бомбы( Все/Акт.) - ";

    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private TextMeshProUGUI _cubeSpawnerText;
    [SerializeField] private TextMeshProUGUI _bombSpawnerText;

    private void Awake()
    {
        _cubeSpawnerText.text = AboutCubes + "0/0";
        _bombSpawnerText.text = AboutBombs + "0/0";
    }

    private void Update()
    {
        Render();
    }

    private void Render()
    {
        _cubeSpawnerText.text = AboutCubes + _cubeSpawner.NumberOfCubesCreated.ToString() + '/' + _cubeSpawner.NumberOfActiveCubes;
        _bombSpawnerText.text = AboutBombs + _bombSpawner.NumberOfBombsCreated.ToString() + '/' + _bombSpawner.NumberOfActiveBombs;
    }
}
