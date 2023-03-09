using UnityEngine;

public class ExplosionPool : ObjectPool
{
    public static ExplosionPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
