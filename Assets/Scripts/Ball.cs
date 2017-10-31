using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

   private float ballInitialVelocity =450f;
	public AudioSource ballSound;

    private Rigidbody rb;
    private bool ballInPlay;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


	void OnCollisionEnter(Collision other){
		if (MenuControl.muteSound) {
			ballSound.GetComponent<AudioSource> ().Play ();
		}
	}


	void FixedUpdate(){
		
		if (Input.GetButtonDown("Fire1") && ballInPlay == false)
		{
			transform.parent = null;
			ballInPlay = true;
			rb.isKinematic = false;
			rb.AddForce(new Vector3(ballInitialVelocity,ballInitialVelocity, 0));

		}

		if (ballInPlay) {
			if (rb.velocity.x < 1 && rb.velocity.x > -1) {
				rb.AddForce (new Vector3 (200, 0, 0));
			}
			if (rb.velocity.y < 1 && rb.velocity.y > -1) {
				rb.AddForce (new Vector3 (0, 200, 0));
			}
		}
	}

    void Update()
    {


    }
}