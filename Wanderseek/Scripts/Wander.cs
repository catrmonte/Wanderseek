using UnityEngine;

public class Wander : MonoBehaviour
{
    public Transform character; // This is the object wandering
    public Rigidbody rigBody;

    public float maxSpeed = 5f;
    public float angleVariance = 15f; // the plus or minues difference that will be randomized between for character's new direction

    public float rotateTimer = 0f; // Counter that keeps track of when to get a new direction
    public float changeFreq = 100f; // How often the object will get a new direction
    public float smoothingAngleIncrement; // percent of angle to rotate
    public int numIncrements = 5; // Number of increments to smooth between

    public int respawnZ;

    void Start()
    {
        // Initialize goal point to be 1 unit ahead in the z direction of the character
        /*goalPoint.x = character.position.x;
        goalPoint.z = character.position.z + 1;
        goalPoint.y = character.position.y;

        character.LookAt(goalPoint);*/

        // Initialize rigidbody
        rigBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBoundaries();

        // Gets a value between negative and postive angleVarience s
        float angleDifference = (Random.value * angleVariance - Random.value * angleVariance);

        if (rotateTimer == changeFreq)
        {
            // Angle is currently the direction the character is facing (in degrees)
            float angle = character.eulerAngles.y;

            // Add the angle difference to the angle. If it is over 360 degrees, wrap back around.
            angle += angleDifference;
            angle %= 360;

           smoothingAngleIncrement = angleDifference / numIncrements;

            // Changes characters rotation by smoothingAngleIncrement degrees
            character.Rotate(0f, smoothingAngleIncrement, 0f, Space.Self);

            angle *= Mathf.Deg2Rad; // Convert angle in degrees to radians

            // Move the character forward
            rigBody.velocity = transform.forward * maxSpeed;

            // Reset rotate counter
            rotateTimer = 0f;
        }
        else if (rotateTimer < numIncrements)
        {
            character.Rotate(0f, smoothingAngleIncrement, 0f, Space.Self);
            rotateTimer++;
        }
        else
        {
            rotateTimer++;
        }
    }

    void CheckBoundaries()
    {
        // If character goes out of bound on the x axis of arena move back to start
        if (character.position.x < -49 || character.position.x > 49 ) {
            character.position = new Vector3(0, 1, respawnZ);
        }
        // If character goes out of bound on the z axis of arena move back to start
        else if (character.position.z < 14 || character.position.z > 118)
        {
            character.position = new Vector3(0, 1, respawnZ);
        }
    }
}
