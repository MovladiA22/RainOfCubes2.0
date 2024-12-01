using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private Bomb _bombPrefab;

    private Vector3 _spawnPosition;

    public void GetBomb(Vector3 position)
    {
        _spawnPosition = position;
        GetObj();
    }

    protected override Bomb Create(Bomb bomb)
    {
        var bombCopy = Instantiate(_bombPrefab, _spawnPosition, Quaternion.identity);
        bombCopy.Exploded += ReleaseObj;

        return base.Create(bombCopy);
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        bomb.ReturnSettings();
        
        base.ActionOnGet(bomb);

        bomb.Exploded += ReleaseObj;
    }

    protected override void ActionOnRelease(Bomb bomb)
    {
        base.ActionOnRelease(bomb);

        bomb.Exploded -= ReleaseObj;
    }
}
