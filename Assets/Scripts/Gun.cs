using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // The bullet that this gun shoots
    public float fireRate; // The rate at which this gun can fire at
    public float orbitSpeed; // The speed at which the gun rotates towards the mouse

    public bool rotatesAroundPlayer = false; // Whether the gun rotates around the player or not

    private float nextFireTime = 0; // The next time the player can fire
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // If it's time to fire again and the player is pressing the fire button
        if (Time.time > nextFireTime && Input.GetButton("Fire1"))
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
        if (rotatesAroundPlayer)
        {
            RotateAroundPlayerTowardMouse();
        }
    }

    void RotateAroundPlayerTowardMouse()
    {
        // Get mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate the direction from the player to the mouse
        Vector3 directionToMouse = mousePosition - player.transform.position;

        // Calculate the angle from the player to the mouse when inital angle is -90
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg - 90f;

        // Calculate the shortest direction to rotate
        float shortestAngle = Mathf.DeltaAngle(transform.eulerAngles.z, angle);
        float direction = shortestAngle / Mathf.Abs(shortestAngle);

        // Rotate the gun around the player to face the mouse
        transform.RotateAround(player.transform.position, direction * Vector3.forward, Mathf.Abs(shortestAngle) * Time.deltaTime * orbitSpeed);
    }

    void Shoot()
    {
        // Instantiate a new bullet from the bulletPrefab
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // Change Bullet Scale
        bullet.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        // Add a Rigidbody2D to the bullet
        Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
        // Configure the Rigidbody2D properties as needed
        rb.gravityScale = 0;

        // Add a CircleCollider2D to the bullet
        CircleCollider2D cc = bullet.AddComponent<CircleCollider2D>();
        // Configure the CircleCollider2D properties as needed
        cc.radius = 0.2f;

        // Add a custom script to the bullet
        BulletScript bs = bullet.AddComponent<BulletScript>();
        // Configure the BulletScript properties as needed
        // bs.property = value;
    }
}