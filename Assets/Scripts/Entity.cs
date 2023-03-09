using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public GameObject explosionPrefab;
    public List<string> triggerTag;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerTag.Contains(other.tag))
        {
            OnDie();
            gameObject.SetActive(false);
            if (other.TryGetComponent<Projectile>(out Projectile p))
                p.Release();
        }
    }

    protected virtual void OnDie()
    {
        GameObject explosion = ExplosionPool.Instance.Get();
        explosion.transform.position = transform.position;
    }
}
