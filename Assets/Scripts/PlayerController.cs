using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    public float speed = 8f;
    public GameObject shoot;
    private KeySequenceController combo1;
	private List<KeyCode> keys;
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        combo1 = new KeySequenceController(new List<KeyCode> { KeyCode.DownArrow, KeyCode.DownArrow, KeyCode.RightArrow });
		keys = new List<KeyCode>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        rigidbody.velocity = new Vector2(speed * x, speed * y);
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(shoot, this.transform.position, this.transform.rotation);
        }

		if(Input.GetKey(KeyCode.Z)){
			if(Input.GetKeyDown(KeyCode.DownArrow))
				combo1.mList.Add(KeyCode.DownArrow);
			if(Input.GetKeyDown(KeyCode.RightArrow))
				combo1.mList.Add(KeyCode.RightArrow);
		}
		if(combo1.mList.Count > 0){
			if (combo1.Check()){
            	Debug.Log("COMBOU!");
				combo1.mList.Clear();
        	}
		}

    }
}
