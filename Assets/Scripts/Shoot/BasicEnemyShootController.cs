using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyShootController : MonoBehaviour {
	
	public float slope = 0f;
	PlayerController player=null;

	float origLife;
	public float timeLife;
	public float speed;

	void Start(){

		origLife = timeLife;
		StartCoroutine (destroyAfterSeconds ());
	}

	void Update () {
		
		Vector2 nextPosition = new Vector2(this.transform.position.x - speed, this.transform.position.y - slope);
		this.transform.position = nextPosition;
	}

	IEnumerator destroyAfterSeconds(){
		yield return new WaitForSeconds (timeLife);

		timeLife = origLife;
		gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Player"){
			PlayerController player = collider.gameObject.GetComponent<PlayerController> ();
			player.Die ();
			timeLife = origLife;

			this.gameObject.SetActive(false);
		}
	}
}
