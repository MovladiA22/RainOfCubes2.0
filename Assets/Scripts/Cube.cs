using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _timeToTransform;

    public event Action<Cube> Collided;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            Invoke(nameof(InvokeEvent), _timeToTransform);
        }
    }

    private void InvokeEvent() =>
        Collided?.Invoke(this);
}
