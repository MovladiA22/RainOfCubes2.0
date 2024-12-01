using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Exploder))]
[RequireComponent(typeof(Renderer))]
public class Bomb : SpawnableObject
{
    [SerializeField] private float _minDetonationTime;
    [SerializeField] private float _maxDetonationTime;

    private Exploder _exploder;
    private Renderer _renderer;
    private float _detonationTime;
    private Coroutine _coroutine;

    public event Action<Bomb> Exploded;

    public void ReturnSettings()
    {
        Color color = _renderer.material.color;
        color.a = 1f;
        _renderer.material.color = color;
    }

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _detonationTime = UnityEngine.Random.Range(_minDetonationTime, _maxDetonationTime + 1);
        _coroutine = StartCoroutine(ChangingTransparency());
        Invoke(nameof(Explode), _detonationTime);
    }

    private void Explode()
    {
        _exploder.Explode(this);
        Exploded?.Invoke(this);
    }

    private IEnumerator ChangingTransparency()
    {
        float counter = _detonationTime;
        var wait = new WaitForSeconds(Time.deltaTime);
        Color color = _renderer.material.color;

        while (counter > 0.0f)
        {
            counter -= Time.deltaTime;
            color.a = counter / _detonationTime;
            _renderer.material.color = color;
            yield return wait;
        }
    }
}
