using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Paralax : MonoBehaviour {
	[System.Serializable]
	public class IParallax{
		public Image go;
		public float speed;
	}

	public GameObject boss;

	public GameObject[] toDisable;
	public Image[] toFadeIn;

	public IParallax[] parallaxes;
	bool stopped = false;
	public Sprite sprite;

	int bossShip = -1;
	bool once = false;

	RectTransform[] spaces;
	RectTransform[] ship;

	int lastSpace = 2;
	int lastShip = 2;

	Boss bossGo = null;

	void Start(){
		spaces = new RectTransform[3];
		ship = new RectTransform[3];

		for (int i=0; i<3; i++) {
			spaces [i] = parallaxes[i].go.rectTransform;
			ship [i] = parallaxes [i + 4].go.rectTransform;
		}

		StartCoroutine (Spawn ());
	}

	IEnumerator Spawn(){
		yield return new WaitForSeconds (2);

		bossGo = Instantiate (boss, ship [lastShip].position, Quaternion.identity).GetComponent<Boss>();
		bossGo.transform.SetParent (ship [lastShip]);
		bossGo.transform.localPosition -= new Vector3 (136, 532);

		bossShip = lastShip;
	}

	void FixedUpdate(){
		RectTransform rect = (RectTransform)parallaxes [0].go.GetComponentsInChildren<Image>()[0].gameObject.transform;

		int j = 0;
		foreach (var x in parallaxes) {
			if (!stopped || j < 2) {
				Vector2 nextPosition = new Vector2 (x.go.rectTransform.localPosition.x + x.speed, x.go.rectTransform.localPosition.y);
				x.go.rectTransform.localPosition = nextPosition;

				if (bossGo && bossGo.transform.position.x < 2f && !once) {
					Stop ();
					once = true;
				}
			}
			j++;
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

	public void Stop(){
		stopped = true;
		bossGo.Initialize ();

		foreach (var enemy in FindObjectsOfType<EnemyController>()) {
			enemy.Die ();
		}

		foreach (var enemy in FindObjectsOfType<EnemyShooterController>()) {
			enemy.Die ();
		}
	}

	public void Continue(){
		for (int i = 4; i < parallaxes.Length; i++) {
			var para = parallaxes [i];
			para.go.DOFade (0, 5).OnComplete (() => {
				para.go.sprite = sprite;
				para.go.DOFade(1f, 5);
			});
		}

		foreach (var x in toDisable) {
			foreach (var y in x.GetComponentsInChildren<SpriteRenderer>()) {
				y.DOFade (0, 5f);
			}
		}

		foreach (var x in toFadeIn) {
			x.DOFade (1, 5f);
		}

		stopped = false;
		once = false;
		bossGo = null;
		bossShip = -1;
	}
}
