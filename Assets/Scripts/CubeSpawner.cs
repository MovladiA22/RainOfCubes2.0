using System;
using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _repeatRate;

    public event Action<Cube> Spawned;

    private void Start()
    {
        StartCoroutine(Raining());
    }

    protected override Cube Create(Cube cube)
    {
        var copyPosition = GetRandomPosition();
        Cube cubeCopy = Instantiate(_cubePrefab, copyPosition, Quaternion.identity);

        cubeCopy.TimeIsOver += ReleaseObj;

        return base.Create(cubeCopy);
    }

    protected override void ActionOnGet(Cube cube)
    {
        cube.transform.position = GetRandomPosition();
        cube.transform.rotation = Quaternion.identity;
        cube.ReturnSettings();
        
        base.ActionOnGet(cube);

        Spawned?.Invoke(cube);
        cube.TimeIsOver += ReleaseObj;
    }

    protected override void ActionOnRelease(Cube cube)
    {
        base.ActionOnRelease(cube);

        cube.TimeIsOver -= ReleaseObj;
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

    private IEnumerator Raining()
    {
        var wait = new WaitForSeconds(_repeatRate);
        bool isWork = true;

        while (isWork)
        {
            GetObj();

            yield return wait;
        }
    }
}
