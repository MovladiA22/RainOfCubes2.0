using UnityEngine;

public class CubeToBombSwapper : MonoBehaviour
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.Spawned += SubscribeToEvent;
    }

    private void OnDisable()
    {
        _cubeSpawner.Spawned -= SubscribeToEvent;
    }

    private void SubscribeToEvent(Cube cube) =>
        cube.TimeIsOver += Swap;

    private void Swap(Cube cube)
    {
        _bombSpawner.GetBomb(cube.transform.position);

        cube.TimeIsOver -= Swap;
    }
}
