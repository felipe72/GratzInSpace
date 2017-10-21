using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawnner : MonoBehaviour {

	public GameObject enemy;

	float posY1;
	float posY2;

	Tweener tween1;
	Tweener tween2;

	void Update () {
		if (Input.GetKeyDown (KeyCode.I)) {
			OneWave ();
		} else if (Input.GetKeyDown (KeyCode.O)) {
			TwoWave ();
		} else if (Input.GetKeyDown (KeyCode.P)) {
			FullRandom ();
		} else if (Input.GetKeyDown (KeyCode.U)) {
			WallOfEnemies (3);
		}

		/*if(Time.time - lastTime > 0.3f){
			var pos = this.transform.position;
			pos.y = posY;
			Instantiate(enemy, pos, this.transform.rotation);
			lastTime = Time.time;
		}*/
	}

	void TrySpawn(ref float lastTime, int i){
		if (i == 1) {
			if (Time.time - lastTime > 0.3f) {
				var pos = this.transform.position;
				pos.y = posY1;
				Instantiate (enemy, pos, this.transform.rotation);
				lastTime = Time.time;
			}
		} else {
			if (Time.time - lastTime > 0.3f) {
				var pos = this.transform.position;
				pos.y = posY2;
				Instantiate (enemy, pos, this.transform.rotation);
				lastTime = Time.time;
			}
		}
	}

	void TwoWave(){
		float lastTime1 = 0, lastTime2 = 0;

		if (tween1 != null) {
			tween1.Kill ();
			tween1 = null;
		}

		if (tween2 != null) {
			tween2.Kill ();
			tween2 = null;
		}

		tween1 = DOTween.To (x => posY1 = x, 0, 5, 2).SetLoops (2, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate(() => {
			TrySpawn(ref lastTime1, 1);
		});
		tween2 = DOTween.To (x => posY2 = x, 0, -5, 2).SetLoops (2, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate(() => {
			TrySpawn(ref lastTime2, 2);
		});;
	}

	void OneWave(){
		float lastTime2 = 0f;

		if (tween1 != null) {
			tween1.Kill ();
			tween1 = null;
		}

		if (tween2 != null) {
			tween2.Kill ();
			tween2 = null;
		}

		tween1 = DOTween.To (x => posY1 = x, -5, 5, 2).SetLoops (-1, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate( () => {
			TrySpawn(ref lastTime2, 1);
		});
	}

	void WallOfEnemies(int howLess){
		print ("oi");
		for (int i = -4; i < 5; i+=2) {
			if (howLess <= 0 || Random.Range (0f, 1f) > .5f) {
				Instantiate (enemy, new Vector3 (transform.position.x, i, 0), Quaternion.identity);
				howLess--;
			} 
		}
	}

	void FullRandom(){
		float fodace = 0f;
		float lastTime2 = 0f;

		if (tween1 != null) {
			tween1.Kill ();
			tween1 = null;
		}

		if (tween2 != null) {
			tween2.Kill ();
			tween2 = null;
		}

		tween1 = DOTween.To (x => fodace = x, -5, 5, 2).SetLoops (-1, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate( () => {
			posY1 = Random.Range (-5, 5);
			TrySpawn(ref lastTime2, 1);
		});

	}
}
