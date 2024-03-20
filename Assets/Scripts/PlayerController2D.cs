using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        // Declair variables that use built in input for both axis
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Normalize the direction vector so that horizontal movement is not faster
        Vector3 direction = new Vector3(moveHorizontal, moveVertical, 0).normalized;

        //Move the player
        transform.position += direction * speed * Time.deltaTime;

        // Rotate the player so that it faces the direction of movement
        if (direction != Vector3.zero)
        {
            // Only rotate the player when moving diagonally
            if (moveHorizontal != 0 && moveVertical != 0)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle = Mathf.Round(angle / 45) * 45; // Lock the angle to the nearest 45 degrees

                // Adjust the angle based on the direction of horizontal movement

                // Adjust for -x, -y movement
                if (moveHorizontal < 0 && moveVertical < 0)
                {
                    angle = -angle - 90;
                }
                // Adjust for -x, y movement
                if (moveHorizontal < 0 && moveVertical > 0)
                {
                    angle = -angle + 90;
                }

                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                // Keep the player upright when moving vertically or horizontally
                transform.rotation = Quaternion.identity;
            }
        }

        //Reflect the sprite to face the direction of movement
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}