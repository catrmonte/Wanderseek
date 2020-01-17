using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;

    public Transform playerTrans;

    public float forwardForce = 2000f;

    public float sidewaysForce = 500f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        playerTrans.position += playerTrans.forward * Time.deltaTime * forwardForce;

        if (Input.GetKey("d") ) {
            // rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            playerTrans.Rotate(0f, .5f, 0f);
        }

        if (Input.GetKey("a") )
        {
            // rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            playerTrans.Rotate(0f, -.5f, 0f);
        }

        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
