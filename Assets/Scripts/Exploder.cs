using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public void Explode(Bomb bomb)
    {
        float divider = bomb.transform.localScale.x;

        foreach (var explodableObject in GetExplodableObjects(bomb))
            explodableObject.AddExplosionForce(_explosionForce / divider, transform.position, _explosionRadius / divider);
    }

    private Stack<Rigidbody> GetExplodableObjects(Bomb bomb)
    {
        Stack<Rigidbody> explodableObjects = new Stack<Rigidbody>();
        Collider[] hits = Physics.OverlapSphere(bomb.transform.position, _explosionRadius);

        foreach (var hit in hits)
        {
            if (hit.attachedRigidbody != null)
                explodableObjects.Push(hit.attachedRigidbody);
        }

        return explodableObjects;
    }
}
