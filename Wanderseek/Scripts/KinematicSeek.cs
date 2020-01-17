using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : MonoBehaviour {

    public Transform target;
    public Transform character;
    public float maxSpeed;
    public float searchRadius = 50f; // How far away it will continue to seek
    public float satisfactionRadius = 3f;
    float timeToTarget = .25f;

    public class KinematicSteeringOutput {
        public Vector3 velocity;
        public Vector3 angularVelocity; 
    }

    void Update()
    {
        KinematicSteeringOutput newSteering = getSteering();

        if(newSteering != null)
        {
            character.position += newSteering.velocity * Time.deltaTime;

            character.eulerAngles = newSteering.angularVelocity;
        }
    }    

    KinematicSteeringOutput getSteering()
    {
        KinematicSteeringOutput result = new KinematicSteeringOutput();
        float angle;

        // Getting the direction towards target by getting the difference between target & character
        result.velocity = target.position - character.position;

        if ( result.velocity.magnitude > searchRadius )
        {
            // Too far to seek
            return null;
        }
         
        if (result.velocity.magnitude <= satisfactionRadius)
        {
            // Close enough
            return null;
        }

        result.velocity /= timeToTarget;

        if (result.velocity.magnitude > maxSpeed)
        {
            result.velocity.Normalize();
            result.velocity *= maxSpeed;
        }

        // Normalize and set speed
        result.velocity.Normalize();
        result.velocity *= maxSpeed;

        angle = newOrientation( character.transform.eulerAngles.y , result.velocity);
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
}
