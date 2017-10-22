using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour {

	List<GameObject> pool;

	int poolIndex;
	int poolSize = 50;

	public GameObject shootObject;

	// Use this for initialization
	void Start () {
		pool = new List<GameObject> ();

		for (int i = 0; i < poolSize; i++) {
			pool.Add (Instantiate (shootObject, Vector3.one * 100, Quaternion.identity));
			pool [i].SetActive (false);
		}
	}

	public GameObject Instantiate(Vector3 pos){
		GameObject go = null;

		for (int i = 0; i < poolSize; i++) {
			if (!pool [i].activeSelf) {
				pool [i].SetActive (true);
				pool [i].transform.position = pos;
				go = pool [i];

				return go;
			}
		}

		pool.Add (Instantiate (shootObject, pos, Quaternion.identity));
		go = pool [pool.Count-1];

		return go;
	}
}
