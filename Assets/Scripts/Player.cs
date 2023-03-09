using UnityEngine;

public class Player : Entity
{
    //public GameObject laserPrefab;
    //public GameObject explosionPrefab;

    public float speed = 1.5f;
    public float projectileSpeed = 3f;
    public float firingCooldown = 1f;
    public float maxshots = 2f;
    public float currLevel = 0;
    public float rotation = 180 / (currLevel + 1);
    private AudioSource audioSource;
    [SerializeField] private AudioClip laserAudio;
    [SerializeField] private float horizontalLimit = 2.5f;
    private float cooldownTimer;
    Rigidbody2D rb;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        cooldownTimer -= Time.deltaTime;
        //Player Movement
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, 0);

        if (transform.position.x > horizontalLimit)
        {
            rb.velocity = Vector2.zero;
            transform.position = new Vector3(horizontalLimit, transform.position.y, transform.position.z);
        }

        if (transform.position.x <= -horizontalLimit)
        {
            rb.velocity = Vector2.zero;
            transform.position = new Vector3(-horizontalLimit, transform.position.y, transform.position.z);
        }

        //Player Attack
        if (Input.GetMouseButtonDown(0))
        {
            if(cooldownTimer <= 0)
            {
                cooldownTimer = firingCooldown;
                GameObject laser = PlayerLaserPool.Instance.Get();
                laser.transform.position = transform.position;

                if (laser.TryGetComponent<Projectile>(out Projectile projectile))
                {
                    projectile.Init(rotation);
                    projectile.CancelInvoke();
                    projectile.Invoke("Release", projectile.lifetime);
                }
                audioSource.PlayOneShot(laserAudio);

            }
        }
    }

    protected override void OnDie()
    {
        base.OnDie();
        SceneController.Instance.ChangeScene("Menu");
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerTag.Contains(other.tag))
        {
            OnDie();
            gameObject.SetActive(false);
            if (other.TryGetComponent<Projectile>(out Projectile p))
                p.Release();
            if (other.tag == "PowerUp")
            {
                currLevel += 1;
            }
        }
    }
}
