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
    public KeySequenceController()
    {
        mKeyList = this.RandomSequence();
		mList = new List<KeyCode>();
    }

    public bool Check()
    {
        Debug.Log(mKeyList[0] + " " + mKeyList[1] + " " + mKeyList[2]);
        if (Time.time > timeLastButtonPressed + allowedTimeBetweenmKeyList)
        {
            if (mCurrentIndex < mKeyList.Count && mList.Count > mCurrentIndex)
            {
                if (mKeyList[mCurrentIndex] == mList[mCurrentIndex])
                {
                        timeLastButtonPressed = Time.time;
                        mCurrentIndex++;
                }else{
                    mList.Clear();
                    return false;
                }
                if (mCurrentIndex >= mKeyList.Count)
                {
                    mCurrentIndex = 0;
					//timeLastButtonPressed = 0;
                    mKeyList = this.RandomSequence();
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

    private List<KeyCode> RandomSequence(){
        Debug.Log("ENTROU AQUI");
        List<List<KeyCode>> randomSequence = new List<List<KeyCode>>();
        randomSequence.Add(new List<KeyCode>(){KeyCode.RightArrow, KeyCode.RightArrow, KeyCode.RightArrow});
        randomSequence.Add(new List<KeyCode>(){KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow});
        randomSequence.Add(new List<KeyCode>(){KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.LeftArrow});
        randomSequence.Add(new List<KeyCode>(){KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.DownArrow});
        randomSequence.Add(new List<KeyCode>(){KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.UpArrow});
        List<KeyCode> sequenceChosen = randomSequence[Random.Range(0, randomSequence.Count)];
        return sequenceChosen;
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
