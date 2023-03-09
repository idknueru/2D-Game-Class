using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ObjectPool pool;
    public float speed;
    public float lifetime;
    Rigidbody2D rb;

    public void Init(float rotation)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        transform.eulerAngles = new Vector3(0f, 0f, 90f - (Mathf.Rad2Deg * Mathf.Atan2(speed, (speed * rotation))));
        rb.velocity = transform.up * speed;
    }

    public void Release()
    {
        pool.Release(gameObject);
    }
}
