using UnityEngine;
using System.Collections;

public class NumberCheckSlotEasy : MonoBehaviour {

	private UILabel m_NumberLabel;
	private UILabel m_StrikeLabel;
	private UILabel m_BallLabel;
	private UILabel m_OutLabel;

	public void SetInfo(int number, int strikeCnt, int ballCnt, int outCnt)
	{
		m_NumberLabel.text = number.ToString("D3");
		m_StrikeLabel.text = strikeCnt.ToString();
		m_BallLabel.text = ballCnt.ToString();
		m_OutLabel.text = outCnt.ToString();
	}

	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform> ();

		foreach (Transform child in transforms) 
		{
			switch (child.name) 
			{
			case "numberLabel":
				m_NumberLabel = child.GetComponent< UILabel > ();
				break;
			case "strikeLabel":
				m_StrikeLabel = child.GetComponent< UILabel > ();
				break;
			case "ballLabel":
				m_BallLabel = child.GetComponent< UILabel > ();
				break;
			case "outLabel":
				m_OutLabel = child.GetComponent< UILabel > ();
				break;
			}
		}
	}

	void Awake()
	{
		OnGetChildObject ();		
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
