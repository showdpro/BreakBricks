using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bricks : MonoBehaviour
{
    public GameObject lifeParticle;
    public GameObject pointParticle;

    void Awake(){
		SetColor();
		gameObject.tag = "Brick";

	}
		
    void OnCollisionEnter(Collision other)
    {
        int n = Random.Range(0, 100);

        if(n< 10)
        {
            Instantiate(lifeParticle, new Vector3(transform.position.x, transform.position.y, 5), transform.rotation);
            GM.instance.SetLifeBuff();
        }
        if(n >=10 && n < 20)
        {
            Instantiate(pointParticle, new Vector3(transform.position.x, transform.position.y, 5), transform.rotation);
            GM.instance.SetScoreBuff();
          
        }

            GM.instance.DestroyBrick();
            GM.instance.SetPoints();
            Destroy(gameObject);
	}


	void SetColor(){
		// Colors
		transform.GetComponent<Renderer> ().material.color = Random.ColorHSV (0f, 1.1f, 1f, 1f, 0.5f, 1f);
	}
}