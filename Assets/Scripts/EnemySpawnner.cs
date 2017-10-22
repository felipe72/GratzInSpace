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

	void Start(){
		OneWave ();
	}

	void Update(){

		if(Time.time - lastTime > 5f){
			lastTime = Time.time;
			int r = Random.Range(1,4);
			if (r == 1) {
				OneWave ();
			} else if (r == 2) {
				TwoWave ();
			} else if (r == 4) {
				//FullRandom ();
			} else if (r == 3) {
				WallOfEnemies (3);
			}
		}
	}

	public void spawn(ref float lastTime, Vector2 pos){
		if (Time.time - lastTime > .5f) {
			Instantiate (enemy [0], pos, Quaternion.identity);	
			lastTime = Time.time;
		}
	}

	public bool canSpawn = true;

	void TrySpawn(ref float lastTime, int i){
		if (canSpawn) {
			if (i == 1) {
				if (Time.time - lastTime > .5f) {
					var pos = this.transform.position;
					pos.y = posY1;
					Instantiate (enemy [Random.Range (0, enemy.Length)], pos + new Vector3 (10, 0, 0), this.transform.rotation);
					lastTime = Time.time;
				}
			} else {
				if (Time.time - lastTime > .5f) {
					var pos = this.transform.position;
					pos.y = posY2;
					Instantiate (enemy [Random.Range (0, enemy.Length)], pos + new Vector3 (10, 0, 0), this.transform.rotation);
					lastTime = Time.time;
				}
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

		tween1 = DOTween.To (x => posY1 = x, 0f, 3f, 2f).SetLoops (2, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate(() => {
			TrySpawn(ref lastTime1, 1);
		});
		tween2 = DOTween.To (x => posY2 = x, 0f, -3f, 2f).SetLoops (2, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate(() => {
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

		tween1 = DOTween.To (x => posY1 = x, -3f, 3f, 2f).SetLoops (-1, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate( () => {
			TrySpawn(ref lastTime2, 1);
		});
	}

	void WallOfEnemies(int howLess){
		for (int i = -4; i < 3; i+=2) {
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

		tween1 = DOTween.To (x => fodace = x, -3, 3, 2).SetLoops (-1, LoopType.Yoyo).SetEase (Ease.InOutQuad).OnUpdate( () => {
			posY1 = Random.Range (-3, 3);
			TrySpawn(ref lastTime2, 1);
		});

	}

	void OnDestroy(){
		tween1.Kill ();
		tween2.Kill ();
	}
}
