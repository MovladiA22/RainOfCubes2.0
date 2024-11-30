using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    private ObjectPool<ISpawned> _pool;

    public event Action ChangedCount;

    public int NumberOfObjectsSpawned { get; private set; }
    public int NumberOfObjectsCreated { get; private set; }
    public int NumberOfActiveObjects { get; private set; }

    private void Awake()
    {
        _pool = new ObjectPool<ISpawned>
            (createFunc: () => Spawn(null),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => DestroyObj(obj),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _maxSize);
    }

    protected void ReleaseObj(ISpawned obj)
    {
        _pool.Release(obj);
    }

    protected void GetObj()
    {
        _pool.Get();
        NumberOfObjectsSpawned++;
        NumberOfActiveObjects = _pool.CountActive;

        ChangedCount?.Invoke();
    }

    protected virtual ISpawned Spawn(ISpawned spawned)
    {
        NumberOfObjectsCreated++;

        return spawned;
    }

    protected virtual void ActionOnGet(ISpawned obj) { }

    protected virtual void ActionOnRelease(ISpawned obj) { }

    protected virtual void DestroyObj(ISpawned obj) { }
}
