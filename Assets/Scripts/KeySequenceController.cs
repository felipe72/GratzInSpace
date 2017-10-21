using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySequenceController : MonoBehaviour
{
	public List<KeyCode> mList;
    public List<KeyCode> mKeyList;
    public int mCurrentIndex = 0;
    public float allowedTimeBetweenmKeyList = 0.2f; //tweak as needed
    private float timeLastButtonPressed;
    public KeySequenceController(List<KeyCode> buttons)
    {
        mKeyList = buttons;
        Debug.Log(mKeyList);
		mList = new List<KeyCode>();
    }

    public bool Check()
    {
		Debug.Log("TIME " + timeLastButtonPressed);
		Debug.Log("COUNT " + mList.Count);
        if (Time.time > timeLastButtonPressed + allowedTimeBetweenmKeyList)
        {
            if (mCurrentIndex < mKeyList.Count && mList.Count > mCurrentIndex)
            {
                if (mKeyList[mCurrentIndex] == mList[mCurrentIndex])
                {
                        timeLastButtonPressed = Time.time;
                        mCurrentIndex++;
                }
                if (mCurrentIndex >= mKeyList.Count)
                {
                    mCurrentIndex = 0;
					//timeLastButtonPressed = 0;
                    return true;
                }
                else{
					if(mList.Count >= 3)
						mList.Clear();
                    return false;
				}
            }
        }
        return false;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
}
