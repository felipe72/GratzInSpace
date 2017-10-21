using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplodeShoot : MonoBehaviour {
	void Update () {
		Vector2 nextPosition = new Vector2(this.transform.position.x + 0.1f, this.transform.position.y);
		this.transform.position = nextPosition;
		if(this.transform.position.x > 30f){
			Destroy(this.gameObject);
		}
	}
	public void Explode(){
		this.transform.DOScale(5f, 0.2f).SetEase(Ease.InQuad);
		FindObjectOfType<PlayerController> ().exist = false;
		Destroy(this.gameObject, 0.2f);
	}
}
