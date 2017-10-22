using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public float laserCooldown;
	public float laserUptime;

	Animator anim;
	Animator childAnim;
	bool dead;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
		childAnim = GetComponentsInChildren<Animator> ()[1];
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

	public void Die(){
		anim.SetTrigger ("die");
		dead = true;
	}
}
