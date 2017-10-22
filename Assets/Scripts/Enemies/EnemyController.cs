using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	bool isDead = false;

	public float speed = 0.07f;

	public bool Launcher;

	public GameObject bullet;

	bool once = false;

	public bool turret;

	SpriteRenderer child;

	bool goingBack;

	Vector3 direction;

	Vector3[] positions;
	Transform target;
	void Start(){
		child = GetComponentInChildren<Animator> ().GetComponent<SpriteRenderer>();
		target = this.transform.Find("target");
	}

	// Update is called once per frame
	void Update () {
		if (!isDead) {
			if (Launcher) {
				RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.up, 10000, 1 << LayerMask.NameToLayer("Player1") | 1 << LayerMask.NameToLayer("Player2"));

				if (hit && !once) {
					once = true;
					Instantiate (bullet, transform);
					StartCoroutine (cooldown ());
				}
			} else if(turret){
				if (!goingBack) {
					child.transform.RotateAround (child.bounds.center, Vector3.forward, Time.deltaTime * 10);
					target.transform.RotateAround (child.bounds.center, Vector3.forward, Time.deltaTime * 10);
				} else {
					child.transform.RotateAround (child.bounds.center, Vector3.forward, Time.deltaTime * -10);
					target.transform.RotateAround (child.bounds.center, Vector3.forward, Time.deltaTime * -10);
				}
				if (!once) {
					once = true;
					direction = target.position - child.transform.position;
					BasicEnemyShootController a = Instantiate (bullet, transform.position + direction.normalized,  transform.rotation).GetComponent<BasicEnemyShootController>();
					a.Load (direction);

					StartCoroutine (cooldown ());
				}
				if (child.transform.rotation.z * Mathf.Rad2Deg >= 20) {
					goingBack = true;
				} else if (child.transform.rotation.z* Mathf.Rad2Deg <= -10) {
					goingBack = false;
				}
			} else {
				Vector2 nextPosition = new Vector2 (this.transform.position.x - speed, this.transform.position.y);
				this.transform.position = nextPosition;
				if (this.transform.position.x < -15f) {
					Destroy (this.gameObject);
				}
			}
		}
	}

	IEnumerator cooldown(){
		yield return new WaitForSeconds (1f);

		once = false;
	}

	public void Die(){
		isDead = true;
		Destroy (gameObject, .2f);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Player"){
			PlayerController player = collider.gameObject.GetComponent<PlayerController> ();
			player.Die ();
		}
	}
}
