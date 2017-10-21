using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuController : MonoBehaviour {
	public Image[] curtains;

	void Update(){
		if (Input.GetKeyDown (KeyCode.K)) {
			curtains [0].rectTransform.DOMoveY (curtains [0].rectTransform.position.y + 2000, 1);
		}

		if (Input.GetKeyDown (KeyCode.F)) {
			curtains [1].rectTransform.DOMoveY (curtains [1].rectTransform.position.y - 2000, 1);
		}
	}
}
