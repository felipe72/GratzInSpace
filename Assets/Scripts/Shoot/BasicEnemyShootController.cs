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

	void Start(){

		origLife = timeLife;
		StartCoroutine (destroyAfterSeconds ());
	}

	void Update () {
		if (diagonal) {
			this.transform.position += dif * Time.deltaTime;
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

	Vector3 dif;

	public void Load(Vector3 v){
		dif = v - transform.position;
		dif.Normalize ();
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
