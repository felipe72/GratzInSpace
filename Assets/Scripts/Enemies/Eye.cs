using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour {

	Boss boss;

	// Use this for initialization
	void Start () {
		boss = GetComponentInParent<Boss> ();
	}

	public void Attack(){
		boss.Spawn ();
	}
}
