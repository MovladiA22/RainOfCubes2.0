using System;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _repeatRate;

    private ObjectPool<Cube> _pool;
    private int _poolCapacity = 10;
    private int _maxSize = 10;

    public event Action<Vector3> Spawned;

    public int NumberOfCubesCreated { get; private set; }
    public int NumberOfActiveCubes { get; private set; }

    private void Awake()
    {
        _pool = new ObjectPool<Cube>
            (createFunc: () => Spawn(),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _maxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    public void DestroyCube(Cube cube)
    {
        cube.Collided -= DestroyCube;
        var cubePosition = cube.transform.position;

        Destroy(cube.gameObject);
        NumberOfActiveCubes--;
        Spawned?.Invoke(cubePosition);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private Cube Spawn()
    {
        var copyPosition = GetRandomPosition();
        Cube cubeCopy = Instantiate(_cubePrefab, copyPosition, Quaternion.identity);
        cubeCopy.Collided += DestroyCube;
        NumberOfCubesCreated++;
        NumberOfActiveCubes++;

        return cubeCopy;
    }

    private Vector3 GetRandomPosition()
    {
        float minValueX = -6f;
        float maxValueX = 6f;
        float minValueZ = -6f;
        float maxValueZ = 5f;
        float positionY = 20f;
        float randomPositionX = UnityEngine.Random.Range(minValueX, maxValueX + 1f);
        float randomPositionZ = UnityEngine.Random.Range(minValueZ, maxValueZ + 1f);

        return new Vector3(randomPositionX, positionY, randomPositionZ);
    }
}
