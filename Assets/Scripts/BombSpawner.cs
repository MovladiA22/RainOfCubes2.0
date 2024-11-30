using UnityEngine;

public class BombSpawner : Spawner
{
    [SerializeField] private Bomb _bombPrefab;

    private Vector3 _spawnPosition;

    public int NumberOfBombsCreated { get; private set; }
    public int NumberOfActiveBombs { get; private set; }

    public void GetBomb(Vector3 position)
    {
        _spawnPosition = position;
        GetObj();
    }

    protected override ISpawned Spawn(ISpawned spawned)
    {
        var bombCopy = Instantiate(_bombPrefab, _spawnPosition, Quaternion.identity);
        spawned = bombCopy;
        bombCopy.Exploded += ReleaseObj;

        return base.Spawn(spawned);
    }

    protected override void ActionOnGet(ISpawned obj)
    {
        if (obj is Bomb bomb)
        {
            bomb.ReturnSettings();
            bomb.gameObject.SetActive(true);

            bomb.Exploded += ReleaseObj;
        }
    }

    protected override void ActionOnRelease(ISpawned obj)
    {
        if (obj is Bomb bomb)
        {
            bomb.gameObject.SetActive(false);

            bomb.Exploded -= ReleaseObj;
        }
    }

    protected override void DestroyObj(ISpawned obj)
    {
        if (obj is Bomb bomb)
            Destroy(bomb.gameObject);
    }
}
