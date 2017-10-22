using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawnner : MonoBehaviour {

	public GameObject[] enemy;

	float posY1;
	float posY2;

	Tweener tween1;
	Tweener tween2;

	private float lastTime = 0f;
	private float lastCommand = 0f;

	void Update () {
		int r = 0;
		if(Time.time - lastCommand > 2f){
			lastCommand = Time.time;
			r = Random.Range(1,5);
		}
		if(Time.time - lastTime > 2f){
			lastTime = Time.time;
			
			if (r == 1) {
				OneWave ();
			} else if (r == 2) {
				TwoWave ();
			} else if (r == 3) {
				FullRandom ();
			} else if (r == 4) {
				WallOfEnemies (3);
			}
		}
	}

	void TrySpawn(ref float lastTime, int i){
		if (i == 1) {
			if (Time.time - lastTime > 1f) {
				var pos = this.transform.position;
				pos.y = posY1;
				Instantiate (enemy[Random.Range(0, enemy.Length)], pos + new Vector3(10,0,0), this.transform.rotation);
				lastTime = Time.time;
			}
		} else {
			if (Time.time - lastTime > 1f) {
				var pos = this.transform.position;
				pos.y = posY2;
				Instantiate (enemy[Random.Range(0, enemy.Length)], pos + new Vector3(10,0,0), this.transform.rotation);
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

		tween1 = DOTween.To (x => posY1 = x, 0f, 3.5f, 2f).SetLoops (2, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate(() => {
			TrySpawn(ref lastTime1, 1);
		});
		tween2 = DOTween.To (x => posY2 = x, 0f, -2f, 2f).SetLoops (2, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate(() => {
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

		tween1 = DOTween.To (x => posY1 = x, -2f, 3.5f, 2f).SetLoops (2, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate( () => {
			TrySpawn(ref lastTime2, 1);
		});
	}

	void WallOfEnemies(int howLess){
		print ("oi");
		for (int i = -2; i < 4; i+=2) {
			if (howLess <= 0 || Random.Range (0f, 1f) > .5f) {
				Instantiate (enemy[Random.Range(0, enemy.Length)], new Vector3 (transform.position.x, i, 0) + new Vector3(10,0,0), Quaternion.identity);
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

		tween1 = DOTween.To (x => fodace = x, -2, 3, 2).SetLoops (-1, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate( () => {
			posY1 = Random.Range (-5, 5);
			TrySpawn(ref lastTime2, 1);
		});

	}
}
