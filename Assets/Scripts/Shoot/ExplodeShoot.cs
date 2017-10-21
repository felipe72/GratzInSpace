using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplodeShoot : MonoBehaviour {
	PlayerController player;

	void Start(){
		player = FindObjectOfType<PlayerController> ();
	}

	void Update () {
		Vector2 nextPosition = new Vector2(this.transform.position.x + 0.1f, this.transform.position.y);
		this.transform.position = nextPosition;
		if(this.transform.position.x > 30f){
			Destroy(this.gameObject);
		}
	}
	public void Explode(){
		this.transform.DOScale(5f, 0.2f).SetEase(Ease.InQuad);
		player.exist = false;
		Destroy(this.gameObject, 0.2f);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if(collider.gameObject.tag == "Enemy" && !player.exist){
			Destroy(collider.gameObject);
		}
	}
}
