using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour
{
	public AudioSource deathSound;

    void OnTriggerEnter(Collider col)
    {
		if (MenuControl.muteSound) {
			deathSound.GetComponent<AudioSource> ().Play ();
		}

        GM.instance.LoseLife();
    }
}