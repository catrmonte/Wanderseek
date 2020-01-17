using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public PlayerMovement movement;

    void OnCollisionEnter(Collision collisionInfo) // gets info on item colliding into
    {
        if (collisionInfo.collider.tag == "Obstacle") // check to see if it's Obstacle
        {
            GetComponent<PlayerMovement>().enabled = false;

            FindObjectOfType<GameManager>().EndGame();
        }
    }

}
