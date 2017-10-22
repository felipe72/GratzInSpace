using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Boss : MonoBehaviour {

	public float laserCooldown;
	public float laserUptime;
	public Vector2 eyePos;

	Animator anim;
	Animator childAnim;
	bool dead;

	public int life;
	EnemySpawnner spawner;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
		childAnim = GetComponentsInChildren<Animator> ()[1];
		spawner = FindObjectOfType<EnemySpawnner> ();
		spawner.canSpawn = false;
	}

	public void Initialize(){
		anim.SetTrigger ("start");
		childAnim.SetTrigger ("open");

		StartCoroutine (cooldown ());
	}

	IEnumerator cooldown(){
		while (!dead) {
			yield return new WaitForSeconds (laserCooldown);

			anim.SetTrigger ("laser");

			yield return new WaitForSeconds (laserUptime);

			anim.SetTrigger ("endLaser");

			yield return new WaitForSeconds (2f);

			childAnim.SetTrigger ("attack");

			yield return new WaitForSeconds (4f);
		
			childAnim.SetTrigger ("endAttack");
		}
	}

	bool canTake = true;

	public void Spawn(){
		StartCoroutine (spawn ());
	}

	float lasttime = 0;

	IEnumerator spawn(){
		float f = 4;

		while (f >= 0) {
			f -= .1f;

			yield return new WaitForSeconds (.1f);
			spawner.spawn (ref lasttime, eyePos + (Vector2) transform.position);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			other.GetComponent<PlayerController> ().Die ();
		}
	}

	public void ReceiveDamage(int damage){
		if (canTake) {
			life -= damage;

			if (life <= 0) {
				Die ();
			}

			canTake = false;
			StartCoroutine (wait ());
		}
	}

	IEnumerator wait(){
		yield return new WaitForSeconds (.1f);

		canTake = true;

	}

	public void Die(){
		if (!dead) {
			transform.DOShakePosition (10, 5f);
			anim.SetTrigger ("die");
			dead = true;
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.DrawSphere ((Vector2)transform.position + eyePos, .1f);
	}

	public void KillObj(){
		FindObjectOfType<ScoreManager> ().AddScore (1000);
		spawner.canSpawn = true;
		FindObjectOfType<Paralax> ().Continue ();

		Destroy (gameObject);

	}
}
