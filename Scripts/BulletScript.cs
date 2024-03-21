using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f; // The speed at which the bullet moves
    public float lifetime = 3f; // The time after which the bullet is destroyed

    // Start is called before the first frame update
    void Start()
    {
        // Destroy the bullet after a certain amount of time
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the bullet upwards
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}