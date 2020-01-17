using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicFlee : MonoBehaviour
{

    public Transform target;
    public Transform character;
    public float maxSpeed;

    public class KinematicSteeringOutput
    {
        public Vector3 velocity;
        public Vector3 angularVelocity;
    }

    void Update()
    {
        CheckBoundaries();

        KinematicSteeringOutput newSteering = getSteering();

        character.position += newSteering.velocity * Time.deltaTime;

        character.eulerAngles = newSteering.angularVelocity;
    }

    KinematicSteeringOutput getSteering()
    {
        KinematicSteeringOutput result = new KinematicSteeringOutput();
        float angle;

        // Getting the direction towards target by getting the difference between target & character
        result.velocity = character.position - target.position;

        // Normalize and set speed
        result.velocity.Normalize();
        result.velocity *= maxSpeed;

        angle = newOrientation(character.transform.eulerAngles.y, result.velocity);
        angle *= Mathf.Rad2Deg;

        result.angularVelocity = new Vector3(0, angle, 0);

        return result;
    }

    // returns new orientation towards target in radians
    float newOrientation(float currentOrientation, Vector3 velocity)
    {
        if (velocity != Vector3.zero)
        {
            return Mathf.Atan2(velocity.x, velocity.z);
        }

        return currentOrientation;
    }

    void CheckBoundaries()
    {
        // If character goes out of bound on the x axis of arena move to random area towards middle of arena
        if (character.position.x < -49 || character.position.x > 49)
        {
            if (this.gameObject.tag == "Obstacle")
            {
                Debug.Log("destroy");
                Destroy(this.gameObject);
            }
            character.position = new Vector3(Random.Range(-35f, 35f), 1, Random.Range(44f, 98f));
        }
        // If character goes out of bound on the z axis of arena move to random area towards middle of arena
        else if (character.position.z < 20 || character.position.z > 118)
        {
            if (this.gameObject.tag == "Obstacle")
            {
                Debug.Log("destroy");
                Destroy(this.gameObject);
            }
            character.position = new Vector3(Random.Range(30f, 108f), 1, Random.Range(44f, 98f));
        }

    }
}
