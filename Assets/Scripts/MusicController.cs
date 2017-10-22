using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {
	private static MusicController _instance;
	// Use this for initialization
	void Awake () {
		if (_instance) {
			_instance = this;
		} else {
			Destroy (this.gameObject);
		}
	}
}
