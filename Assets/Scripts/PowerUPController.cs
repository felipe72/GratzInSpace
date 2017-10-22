using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUPController : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 nextPosition = new Vector2(this.transform.position.x - 0.05f, this.transform.position.y);
		this.transform.position = nextPosition;
		if (this.transform.position.x <= -30f) {
			Destroy (this);
		}
	}
}
