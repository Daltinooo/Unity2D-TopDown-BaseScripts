using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5.0f; // Public so can be changed later
    
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal"); // get input for horizontal movement
        float moveVertical = Input.GetAxisRaw("Vertical"); // get input for vertical movement

        Vector3 direction = new Vector3(moveHorizontal, moveVertical, 0).normalized; // create a direction vector from the input and normalize it

        transform.position += direction * speed * Time.deltaTime; // move the player in the direction of the input

        // diagonal movement
        if (direction!= Vector3.zero)
        {
            if (moveHorizontal != 0 && moveVertical != 0) // if the player is moving diagonally
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // calculate the angle of the direction
                angle = Mathf.Round(angle / 45) * 45; // round the angle to the nearest 45 degrees

                if (moveHorizontal < 0 && moveVertical < 0) // correct the angle for -x,-y movement
                {
                    angle = -angle - 90;
                }
                if (moveHorizontal < 0 && moveVertical > 0) // correct the angle for -x,+y movement
                {
                    angle = -angle + 90;
                }

                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // rotate the player to face the direction
            }
            else
            {
                transform.rotation = Quaternion.identity; // if the player is not moving diagonally, reset the rotation
            }
        }

        // Flip the player sprite to face the direction of movement
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // flip the player sprite to face right
        }
        if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // flip the player sprite to face left
        }
    }
}