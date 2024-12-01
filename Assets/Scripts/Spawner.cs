using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<Object> : MonoBehaviour where Object : SpawnableObject
{
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    private ObjectPool<Object> _pool;

    public event Action ChangedCount;

    public int NumberOfObjectsSpawned { get; private set; }
    public int NumberOfObjectsCreated { get; private set; }
    public int NumberOfActiveObjects { get; private set; }

    private void Awake()
    {
        _pool = new ObjectPool<Object>
            (createFunc: () => Create(null),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => DestroyObj(obj),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _maxSize);
    }

    protected void ReleaseObj(Object obj)
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

    protected virtual Object Create(Object obj)
    {
        NumberOfObjectsCreated++;

        return obj;
    }

    protected virtual void ActionOnGet(Object obj) 
    {
        obj.gameObject.SetActive(true);
    }

    protected virtual void ActionOnRelease(Object obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void DestroyObj(Object obj)
    {
        Destroy(obj.gameObject);
    }
}
