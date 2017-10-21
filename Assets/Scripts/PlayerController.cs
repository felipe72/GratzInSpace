using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    public float speed = 8f;
    public GameObject shoot;
    private KeySequenceController combo1;
	private List<KeyCode> keys;
    public Slider specialBarSlider;
    public Text sequenceKeys;

    public 
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        combo1 = new KeySequenceController();
		keys = new List<KeyCode>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        rigidbody.velocity = new Vector2(speed * x, speed * y);
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button0) )
        {
            if(specialBarSlider.value < 100)
                specialBarSlider.value += 10;
            Instantiate(shoot, this.transform.position, this.transform.rotation);
        }

		if(Input.GetKey(KeyCode.Z) && specialBarSlider.value == 100){
            sequenceKeys.text = combo1.mKeyList[0] + " " + combo1.mKeyList[1] + " " + combo1.mKeyList[2];
			if(Input.GetKeyDown(KeyCode.DownArrow))
				combo1.mList.Add(KeyCode.DownArrow);
            
			if(Input.GetKeyDown(KeyCode.RightArrow))
				combo1.mList.Add(KeyCode.RightArrow);
                
            if(Input.GetKeyDown(KeyCode.UpArrow))
				combo1.mList.Add(KeyCode.UpArrow);
            
			if(Input.GetKeyDown(KeyCode.LeftArrow))
				combo1.mList.Add(KeyCode.LeftArrow);
		}
		if(combo1.mList.Count > 0){
			if (combo1.Check()){
            	Debug.Log("COMBOU!");
				combo1.mList.Clear();
                specialBarSlider.value = 0;
        	}else if(combo1.mList.Count == 3 && !combo1.Check()){
                specialBarSlider.value = 0;
            }
		}

    }
}
