using UnityEngine;

public class BombSpawner : MonoBehaviour
{
    [SerializeField] private Bomb _bombPrefab;

    public int NumberOfBombsCreated { get; private set; }
    public int NumberOfActiveBombs { get; private set; }

    public Bomb Spawn(Vector3 cubePosition)
    {
        var bombCopy = Instantiate(_bombPrefab, cubePosition, Quaternion.identity);
        bombCopy.Exploded += DestroyBomb;
        NumberOfBombsCreated++;
        NumberOfActiveBombs++;

        return bombCopy;
    }

    private void DestroyBomb(Bomb bomb)
    {
        bomb.Exploded -= DestroyBomb;
        NumberOfActiveBombs--;
        Destroy(bomb.gameObject);
    }
}
