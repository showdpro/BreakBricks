using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    private float paddleSpeed = 2f;
	private float xPos;

    private Vector3 playerPos = new Vector3(0, -10.5f, 0);

	void Awake(){
		transform.position = playerPos;
	}

    void Update()
    {

#if UNITY_EDITOR
			 xPos = transform.position.x + (Input.GetAxis("Horizontal")) * paddleSpeed * Time.deltaTime*30;
			playerPos = new Vector3(Mathf.Clamp(xPos, -6.2f, 6.2f), -10.5f, 0f);
			transform.position =playerPos;
#endif

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			xPos = transform.position.x + touchDeltaPosition.x * paddleSpeed * Time.deltaTime;
				
			playerPos = new Vector3(Mathf.Clamp(xPos, -6.2f, 6.2f), -10.5f, 0f);
			// Move object across XY plane
			transform.position =playerPos;

		}
    }
}
