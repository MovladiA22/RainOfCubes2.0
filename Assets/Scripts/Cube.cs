using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour, ISpawned
{
    private bool _isCollided = false;
    private float _lifeTime;
    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public event Action<Cube> TimeIsOver;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided == false && collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            _isCollided = true;
            ProcessCollision();
            Invoke(nameof(InvokeEvent), _lifeTime);
        }
    }

    public void ReturnSettings()
    {
        _rigidbody.velocity = Vector3.zero;
        _renderer.material.color = Color.clear;
        _isCollided = false;
    }

    private void InvokeEvent() =>
        TimeIsOver?.Invoke(this);

    private void ProcessCollision()
    {
        float minValue = 2;
        float maxValue = 5;

        _lifeTime = Random.Range(minValue, maxValue + 1);
        _renderer.material.color = Random.ColorHSV();
    }
}
