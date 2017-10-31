using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BricksMenu : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		transform.GetComponent<Renderer> ().material.color = Random.ColorHSV (0f, 1.1f, 1f, 1f, 0.5f, 1f);
	}
	

}
