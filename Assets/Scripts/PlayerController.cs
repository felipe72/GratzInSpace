using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
	public float speed = 8f;
	public GameObject shoot;
	public float fireRate = 0.2f;
	private float lastShoot = 0f;

	public GameObject[] shoots;

	public bool laser = false;
	public bool explode = false;
	public bool exist = false;
	GameObject shootObject;

	public bool player1;
    private KeySequenceController combo1;
	private KeySequenceController combo2;
	private List<KeyCode> keys;
    public Slider specialBarSlider;
    public Text sequenceKeysP1;
	public Text sequenceKeysP2;

    public 
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        combo1 = new KeySequenceController();
		combo2 = new KeySequenceController();
		keys = new List<KeyCode>();
    }

    void Update()
    {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			shoot = shoots [0];
			laser = false;
			explode = false;
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			shoot = shoots [1];
			laser = false;
			explode = false;
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			shoot = shoots [2];
			laser = true;
			explode = false;
		}
		else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			shoot = shoots [3];
			laser = false;
			explode = true;
		}
        float x = 0;
		float y = 0;
		if (player1) {
			x = Input.GetAxis ("Horizontal");
			y = Input.GetAxis ("Vertical");
		} else {
			x = Input.GetAxis ("Horizontal2");
			y = Input.GetAxis ("Vertical2");
		}
		rigidbody.velocity = new Vector2(speed * x, speed * y);

		if (player1) {
			Shoot (KeyCode.K);
			if(Input.GetKey(KeyCode.Z) && specialBarSlider.value == 100){
            sequenceKeysP1.text = combo1.mKeyList[0] + " " + combo1.mKeyList[1] + " " + combo1.mKeyList[2];
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
		} else {
			Shoot (KeyCode.F);
			if(Input.GetKey(KeyCode.X) && specialBarSlider.value == 100){
            sequenceKeysP2.text = combo2.mKeyList[0] + " " + combo2.mKeyList[1] + " " + combo2.mKeyList[2];
			if(Input.GetKeyDown(KeyCode.DownArrow))
				combo2.mList.Add(KeyCode.DownArrow);
            
			if(Input.GetKeyDown(KeyCode.RightArrow))
				combo2.mList.Add(KeyCode.RightArrow);
                
            if(Input.GetKeyDown(KeyCode.UpArrow))
				combo2.mList.Add(KeyCode.UpArrow);
            
			if(Input.GetKeyDown(KeyCode.LeftArrow))
				combo2.mList.Add(KeyCode.LeftArrow);
		}
		if(combo2.mList.Count > 0){
			if (combo2.Check()){
            	Debug.Log("COMBOU!");
				combo2.mList.Clear();
                specialBarSlider.value = 0;
        	}else if(combo2.mList.Count == 3 && !combo2.Check()){
                specialBarSlider.value = 0;
            }
		}
		}

		
	}
	void Shoot(KeyCode key){
		if (laser) {
			if (Input.GetKeyDown (key)) {
				if(specialBarSlider.value < 100)
					specialBarSlider.value += 1;
				shootObject = Instantiate (shoot, this.transform.position, this.transform.rotation);
				shootObject.GetComponent<Laser> ().Load (this);
				lastShoot = Time.time;
			} else if(Input.GetKeyUp(key)){
				Destroy(shootObject);
			}
		} 
		else if(explode){
			if (Input.GetKeyDown (key) && !exist) {
				if(specialBarSlider.value < 100)
					specialBarSlider.value += 20;
				shootObject = Instantiate (shoot, this.transform.position, this.transform.rotation);
				shootObject.GetComponent<ExplodeShoot> ().Load (this);
				lastShoot = Time.time;
				exist = true;
			}
			else if(Input.GetKeyDown (key) && shootObject){
				shootObject.GetComponent<ExplodeShoot>().Explode();
			}
		}
		else {
			if (Input.GetKey (key) && Time.time - lastShoot > fireRate) {
				if(specialBarSlider.value < 100)
					specialBarSlider.value += 10;
				Instantiate (shoot, this.transform.position, this.transform.rotation);
				lastShoot = Time.time;
			}
		}
	}
}