using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyShootController : MonoBehaviour {
	
	public float slope = 0f;
	PlayerController player=null;

	public float timeLife;
	public float speed;

	void Start(){
		StartCoroutine (destroyAfterSeconds ());
	}

	void Update () {
		
		Vector2 nextPosition = new Vector2(this.transform.position.x - speed, this.transform.position.y - slope);
		this.transform.position = nextPosition;
	}

	IEnumerator destroyAfterSeconds(){
		yield return new WaitForSeconds (timeLife);

		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Player"){
			PlayerController player = collider.gameObject.GetComponent<PlayerController> ();
			player.Die ();
			Destroy(this.gameObject);
		}
	}
}
