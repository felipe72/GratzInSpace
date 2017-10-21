using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySequenceController : MonoBehaviour
{
	public List<KeyCode> mList;
    public List<KeyCode> mKeyListP1;
    public List<KeyCode> mKeyListP2;
    public int mCurrentIndex = 0;
    public float allowedTimeBetweenmKeyList = 0.2f; //tweak as needed
    private float timeLastButtonPressed;
    public KeySequenceController()
    {
        mKeyListP1 = this.RandomSequenceP1();
        mKeyListP2 = this.RandomSequenceP2();
		mList = new List<KeyCode>();
    }

    public bool CheckP1()
    {
        Debug.Log(mKeyListP1[0] + " " + mKeyListP1[1] + " " + mKeyListP1[2]);
        Debug.Log("COUNT " + mList.Count);
        if (Time.time > timeLastButtonPressed + allowedTimeBetweenmKeyList)
        {
            if (mCurrentIndex < mKeyListP1.Count && mList.Count > mCurrentIndex)
            {
                if (mKeyListP1[mCurrentIndex] == mList[mCurrentIndex])
                {
                        timeLastButtonPressed = Time.time;
                        mCurrentIndex++;
                }else{
                    Debug.Log("ENTROU NESSE ELSE?");
                    //mList.Clear();
                    return false;
                }
                if (mCurrentIndex >= mKeyListP1.Count)
                {
                    mCurrentIndex = 0;
					//timeLastButtonPressed = 0;
                    mKeyListP1 = this.RandomSequenceP1();
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

    public bool CheckP2()
    {
        Debug.Log(mKeyListP2[0] + " " + mKeyListP2[1] + " " + mKeyListP2[2]);
        Debug.Log("COUNT " + mList.Count);
        if (Time.time > timeLastButtonPressed + allowedTimeBetweenmKeyList)
        {
            if (mCurrentIndex < mKeyListP2.Count && mList.Count > mCurrentIndex)
            {
                if (mKeyListP2[mCurrentIndex] == mList[mCurrentIndex])
                {
                        timeLastButtonPressed = Time.time;
                        mCurrentIndex++;
                }else{
                    Debug.Log("ENTROU NESSE ELSE?");
                    //mList.Clear();
                    return false;
                }
                if (mCurrentIndex >= mKeyListP2.Count)
                {
                    mCurrentIndex = 0;
					//timeLastButtonPressed = 0;
                    mKeyListP2 = this.RandomSequenceP2();
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

    private List<KeyCode> RandomSequenceP1(){
        Debug.Log("ENTROU AQUI");
        List<List<KeyCode>> randomSequence = new List<List<KeyCode>>();
        randomSequence.Add(new List<KeyCode>(){KeyCode.D, KeyCode.D, KeyCode.D});
        randomSequence.Add(new List<KeyCode>(){KeyCode.W, KeyCode.D, KeyCode.S});
        randomSequence.Add(new List<KeyCode>(){KeyCode.S, KeyCode.A, KeyCode.A});
        randomSequence.Add(new List<KeyCode>(){KeyCode.W, KeyCode.S, KeyCode.S});
        randomSequence.Add(new List<KeyCode>(){KeyCode.W, KeyCode.S, KeyCode.W});
        List<KeyCode> sequenceChosen = randomSequence[Random.Range(0, randomSequence.Count)];
        return sequenceChosen;
    }
    private List<KeyCode> RandomSequenceP2(){
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
}
