using UnityEngine;

public class SwapperBombToCube : MonoBehaviour
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.Spawned += Swap;
    }

    private void OnDisable()
    {
        _cubeSpawner.Spawned -= Swap;
    }

    private void Swap(Vector3 cubePosition)
    {
        _bombSpawner.Spawn(cubePosition).ExplodeWithDelay();
    }
}
