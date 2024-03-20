using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class GunData : ScriptableObject
{
    public string gunName;
    public float damage;
    public float headshotMultiplier;
    public float fireRate;
    public float range;
    public AnimationCurve damageDropOff;
    public float mobility;
    public float accuracy;
    // Shotgun specific variables
    public int bulletCount; // Number of bullets fired in one shot
    public float spreadAngle; // Angle of bullet spread
}

public class Gun : MonoBehaviour
{
    public float bulletSpeed = 20.0f;
    public GunData gunData;
    public Transform firePoint;
    public GameObject bulletPrefab;
    private float nextTimeToFire = 0f;

    public void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / gunData.fireRate;
            for (int i = 0; i < gunData.bulletCount; i++)
            {
                // Calculate spread
                float spread = Random.Range(-gunData.spreadAngle / 2, gunData.spreadAngle / 2);
                Quaternion bulletRotation = Quaternion.Euler(0, 0, spread) * firePoint.rotation;

                // Instantiate the bullet with spread
                Instantiate(bulletPrefab, firePoint.position, bulletRotation);
            }
        }
    }
}

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float damage;
    public float range;
    public AnimationCurve damageDropOff;
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
        // Give the bullet a forward force
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        float distanceTravelled = Vector2.Distance(startPosition, transform.position);
        if (distanceTravelled > range)
        {
            Destroy(gameObject); // Destroy the bullet if it exceeds the range
        }
        else
        {
            // Adjust damage based on the distance travelled
            damage *= damageDropOff.Evaluate(distanceTravelled / range);
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Add logic for what happens when the bullet hits something
        // Example: if(hitInfo.CompareTag("Enemy")) { hitInfo.GetComponent<Enemy>().TakeDamage(damage); }
        Destroy(gameObject); // Destroy the bullet
    }
}