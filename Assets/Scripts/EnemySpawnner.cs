using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour {

	public GameObject enemy;
	private float lastTime = 0f;

	void Update () {
		if(Time.time - lastTime > 0.3f){
			Instantiate(enemy, this.transform.position, this.transform.rotation);
			lastTime = Time.time;
		}
	}
}
