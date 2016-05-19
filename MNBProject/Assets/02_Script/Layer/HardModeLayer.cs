using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HardModeLayer : MonoBehaviour {
	//UI Components
	private GameObject m_slot1;
	private GameObject m_slot2;
	private GameObject m_slot3;
	private GameObject m_slot4;

	private GameObject m_historyWindow;
	private UIScrollView m_historyScrollView;

	private UILabel m_timerLabel;
	private UILabel m_ballCntLabel;

	private UIButton m_GoBtn;

	//Variables
	private const int BALL_CNT = 4;

	private int m_ballCnt;
	private float m_timer;

	private int[] m_answer = new int[BALL_CNT];
	private int[] m_inputNumber = new int[BALL_CNT];

	private void OnGetChildObject() 
	{
		Transform[] transforms = gameObject.GetComponentsInChildren< Transform > ();

		foreach (Transform child in transforms) {
			switch (child.name) {
			case "Scroll_Numberbar":
				//슬롯 1
				Transform slot = child.FindChild ("Slotnumber_1").GetComponent< Transform > ();
				m_slot1 = slot.FindChild ("Grid").gameObject;
				m_slot1.GetComponent< UICenterOnChild > ().onCenter = GoBtnOnOffCheck;

				//슬롯 2
				slot = child.FindChild ("Slotnumber_2").GetComponent< Transform > ();
				m_slot2 = slot.FindChild ("Grid").gameObject;
				m_slot2.GetComponent< UICenterOnChild > ().onCenter = GoBtnOnOffCheck;

				//슬롯 3
				slot = child.FindChild ("Slotnumber_3").GetComponent< Transform > ();
				m_slot3 = slot.FindChild ("Grid").gameObject;
				m_slot3.GetComponent< UICenterOnChild > ().onCenter = GoBtnOnOffCheck;

				//슬롯 4
				slot = child.FindChild ("Slotnumber_4").GetComponent< Transform > ();
				m_slot4 = slot.FindChild ("Grid").gameObject;
				m_slot4.GetComponent< UICenterOnChild > ().onCenter = GoBtnOnOffCheck;
				break;

			case "Btn_bg":
				m_GoBtn = child.FindChild ("btn_go").GetComponent< UIButton > ();
				m_GoBtn.isEnabled = false;
				break;

			case "SlotNumber":
				m_historyScrollView = child.GetComponent< UIScrollView > ();
				m_historyWindow = child.FindChild ("Grid").gameObject;
				break;

			case "TimeSlot":
				m_timerLabel = child.FindChild ("TimerLabel").GetComponent< UILabel > ();
				break;

			case "BallCnt":
				m_ballCntLabel = child.FindChild ("CntLabel").GetComponent< UILabel > ();
				break;
			}
		}
	}

	private void SetRandomAnswer()
	{
		//random number generate
		int[] number = new int[10];

		for(int i = 0; i < 10; i++) 
		{
			number [i] = i;
		}

		for(int i = 0; i < 10; i++) 
		{
			int dest = UnityEngine.Random.Range (0, 10);

			int temp = number [i];
			number [i] = number [dest];
			number [dest] = temp;
		}

		for (int i = 0; i < BALL_CNT; i++) 
		{
			m_answer [i] = number [i];
		}
		Debug.Log (m_answer[0].ToString() + m_answer[1].ToString() + m_answer[2].ToString() + m_answer[3].ToString());

	}

	private void GetInputNumber()
	{
		int inputNumber;

		int input1, input2, input3, input4;

		string inputObjectName;

		if (m_slot1.GetComponent< UICenterOnChild > ().centeredObject == null ||
			m_slot2.GetComponent< UICenterOnChild > ().centeredObject == null ||
			m_slot3.GetComponent< UICenterOnChild > ().centeredObject == null ||
			m_slot4.GetComponent< UICenterOnChild > ().centeredObject == null)
			return;

		inputObjectName = m_slot1.GetComponent< UICenterOnChild > ().centeredObject.name;
		input1 = Convert.ToInt32 (inputObjectName);

		inputObjectName = m_slot2.GetComponent< UICenterOnChild > ().centeredObject.name;
		input2 = Convert.ToInt32 (inputObjectName);
	
		inputObjectName = m_slot3.GetComponent< UICenterOnChild > ().centeredObject.name;
		input3 = Convert.ToInt32 (inputObjectName);
	
		inputObjectName = m_slot4.GetComponent< UICenterOnChild > ().centeredObject.name;
		input4 = Convert.ToInt32 (inputObjectName);

		m_inputNumber [0] = input1;
		m_inputNumber [1] = input2;
		m_inputNumber [2] = input3;
		m_inputNumber [3] = input4;

		Debug.Log (input1.ToString () + input2.ToString () + input3.ToString () + input4.ToString ());
	}

	private bool DuplicationCheck()
	{
		GetInputNumber ();
		
		for (int i = 0; i < BALL_CNT-1; i++) 
		{
			for (int j = (i + 1); j < BALL_CNT; j++) 
			{
				if (m_inputNumber [i] == m_inputNumber [j])
					return false;
			}
		}

		return true;		
	}

	private void CheckAnswer()
	{
		int strikeCnt = 0, ballCnt = 0, outCnt = 0;

		for(int i = 0; i < BALL_CNT; i++)
		{
			for(int j = 0; j < BALL_CNT; j++)
			{
				if(m_inputNumber[i] == m_answer[j])
				{
					if( i == j)
						strikeCnt++;
					else
						ballCnt++;

					break;
				}
				if(j == BALL_CNT-1)
					outCnt++;
			}
		}

		if(outCnt == 4)
		{
			Debug.Log ("OUT");
		}
		else if ( strikeCnt == 4 )
		{
			Debug.Log ("HomeRun");
		}
		else
		{
			Debug.Log (strikeCnt.ToString () + "S " + ballCnt.ToString () + "B 입니다");
		}
	}

	public void GoBtnOnOffCheck(GameObject centeredObject)
	{
		if (DuplicationCheck () == true) 
		{
			m_GoBtn.isEnabled = true;
		}
		else 
		{
			m_GoBtn.isEnabled = false;
		}
	}

	public void OnClickedGo()
	{
		GetInputNumber ();
		CheckAnswer ();
	}

	void Awake() 
	{
		OnGetChildObject ();
		SetRandomAnswer ();

		m_timerLabel.text = "120";
		m_ballCntLabel.text = "6";
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
