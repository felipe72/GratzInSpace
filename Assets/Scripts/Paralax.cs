using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paralax : MonoBehaviour {
	[System.Serializable]
	public class IParallax{
		public Image go;
		public float speed;
	}

	public IParallax[] parallaxes;

	RectTransform[] spaces;
	RectTransform[] ship;

	int lastSpace = 2;
	int lastShip = 2;

	void Start(){
		spaces = new RectTransform[3];
		ship = new RectTransform[3];

		for (int i=0; i<3; i++) {
			spaces [i] = parallaxes[i].go.rectTransform;
			ship [i] = parallaxes [i + 4].go.rectTransform;
		}
	}

	void FixedUpdate(){
		RectTransform rect = (RectTransform)parallaxes [0].go.GetComponentsInChildren<Image>()[0].gameObject.transform;

		foreach (var x in parallaxes) {
			Vector2 nextPosition = new Vector2(x.go.rectTransform.localPosition.x + x.speed, x.go.rectTransform.localPosition.y);
			x.go.rectTransform.localPosition = nextPosition;
		}
		int i = 0;
		foreach (var x in spaces) {
			if (x.localPosition.x < -1920) {
				var pos = x.localPosition;
				pos.x = 1920 + spaces[lastSpace++].localPosition.x;
				x.localPosition = pos;
			}
		}
		i = 0;
		foreach (var x in ship) {
			if (x.localPosition.x < -1920) {
				var pos = x.localPosition;
				pos.x = 1920 + ship[lastShip++].localPosition.x;
				x.localPosition = pos;
			}
		}

		lastSpace = lastSpace % 3;
		lastShip = lastShip % 3;
	}
}
