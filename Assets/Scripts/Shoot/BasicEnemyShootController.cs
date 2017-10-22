using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyShootController : MonoBehaviour {
	
	public float slope = 0f;
	PlayerController player=null;

	float origLife;
	public float timeLife;
	public float speed;

	public bool vertical;
	public bool diagonal;

	private Vector3 dif;

	void Start(){

		origLife = timeLife;
		StartCoroutine (destroyAfterSeconds ());
	}

	void Update () {
		if (diagonal) {
			Vector2 nextPosition = new Vector2 (this.transform.position.x + speed * dif.x, this.transform.position.y + speed * dif.y);
			this.transform.position = nextPosition;
		} else {
			if (!vertical) {
				Vector2 nextPosition = new Vector2 (this.transform.position.x - speed, this.transform.position.y - slope);
				this.transform.position = nextPosition;
			} else {
				Vector2 nextPosition = new Vector2 (this.transform.position.x, this.transform.position.y + speed);
				this.transform.position = nextPosition;

			}
		}
	}

	IEnumerator destroyAfterSeconds(){
		yield return new WaitForSeconds (timeLife);

		timeLife = origLife;
		gameObject.SetActive (false);
	}

	public void Load(Vector3 v){
		dif = v.normalized;
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
