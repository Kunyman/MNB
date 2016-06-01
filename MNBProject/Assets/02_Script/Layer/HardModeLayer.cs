using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HardModeLayer : MonoBehaviour {
	//UI Components
	private GameObject m_Slot1;
	private GameObject m_Slot2;
	private GameObject m_Slot3;
	private GameObject m_Slot4;

	private UIGrid m_NumberSlotGrid;
	private UIScrollView m_HistoryScrollView;

	private UILabel m_TimerLabel;
	private UILabel m_BallCntLabel;

	private UIButton m_GoBtn;

	//Variables
	private const int BALL_CNT = 4;
	private const int MAX_SLOT_CNT = 7;
	private const float GAME_TIME = 180f;
	private float SLOT_HEIGHT = 78;
	private int m_CurrentSlotCnt;
	private Vector3 m_GridPos;
	private Vector3 m_ScrollViewPos = new Vector3 (0f, 241f, 0f);

	private int m_ballCnt;
	private float m_timer;
	private bool m_stopFlag = false;

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
				m_Slot1 = slot.FindChild ("Grid").gameObject;
				m_Slot1.GetComponent< UICenterOnChild > ().onCenter = GoBtnOnOffCheck;

				//슬롯 2
				slot = child.FindChild ("Slotnumber_2").GetComponent< Transform > ();
				m_Slot2 = slot.FindChild ("Grid").gameObject;
				m_Slot2.GetComponent< UICenterOnChild > ().onCenter = GoBtnOnOffCheck;

				//슬롯 3
				slot = child.FindChild ("Slotnumber_3").GetComponent< Transform > ();
				m_Slot3 = slot.FindChild ("Grid").gameObject;
				m_Slot3.GetComponent< UICenterOnChild > ().onCenter = GoBtnOnOffCheck;

				//슬롯 4
				slot = child.FindChild ("Slotnumber_4").GetComponent< Transform > ();
				m_Slot4 = slot.FindChild ("Grid").gameObject;
				m_Slot4.GetComponent< UICenterOnChild > ().onCenter = GoBtnOnOffCheck;
				break;

			case "Btn_bg":
				m_GoBtn = child.FindChild ("btn_go").GetComponent< UIButton > ();
				m_GoBtn.isEnabled = false;
				break;

			case "SlotNumberView":
				m_HistoryScrollView = child.GetComponent< UIScrollView > ();
				m_NumberSlotGrid = child.FindChild ("NumberSlotGrid").GetComponent< UIGrid > ();
				SLOT_HEIGHT = m_NumberSlotGrid.cellHeight;
				m_GridPos = m_NumberSlotGrid.transform.localPosition;
				break;

			case "TimeSlot":
				m_TimerLabel = child.FindChild ("TimerLabel").GetComponent< UILabel > ();
				break;

			case "BallCnt":
				m_BallCntLabel = child.FindChild ("CntLabel").GetComponent< UILabel > ();
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

		if (m_Slot1.GetComponent< UICenterOnChild > ().centeredObject == null ||
			m_Slot2.GetComponent< UICenterOnChild > ().centeredObject == null ||
			m_Slot3.GetComponent< UICenterOnChild > ().centeredObject == null ||
			m_Slot4.GetComponent< UICenterOnChild > ().centeredObject == null)
			return;

		inputObjectName = m_Slot1.GetComponent< UICenterOnChild > ().centeredObject.name;
		input1 = Convert.ToInt32 (inputObjectName);

		inputObjectName = m_Slot2.GetComponent< UICenterOnChild > ().centeredObject.name;
		input2 = Convert.ToInt32 (inputObjectName);
	
		inputObjectName = m_Slot3.GetComponent< UICenterOnChild > ().centeredObject.name;
		input3 = Convert.ToInt32 (inputObjectName);
	
		inputObjectName = m_Slot4.GetComponent< UICenterOnChild > ().centeredObject.name;
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

		if (strikeCnt == 4) 
		{
			GameClear ();
		}
		else 
		{
			SetWrongAnswer (strikeCnt, ballCnt, outCnt);
		}
	}

	private void SetWrongAnswer(int strikeCnt, int ballCnt, int outCnt)
	{
		int number = 0;
		for (int i = 0; i < BALL_CNT; i++) 
		{
			number += m_inputNumber [i] * (int)Math.Pow (10, (BALL_CNT - (i + 1)));
		}

		SetNumberSlot (number, strikeCnt, ballCnt, outCnt);

		m_BallCntLabel.text = m_ballCnt.ToString ();

		if (m_ballCnt == 0) 
		{
			GameOver ();
		}
	}

	private void SetNumberSlot(int number, int strikeCnt, int ballCnt, int outCnt)
	{
		UnityEngine.Object obj = Resources.Load ("numberCheckSlot");
		GameObject slot = Instantiate (obj) as GameObject;
		slot.transform.parent = m_NumberSlotGrid.transform;
		slot.transform.localScale = Vector3.one;
		slot.transform.localPosition = new Vector3 (0, m_CurrentSlotCnt * (m_NumberSlotGrid.cellHeight) * -1, 0);

		slot.GetComponent< NumberCheckSlot > ().SetInfo (number, strikeCnt, ballCnt, outCnt);
//		m_NumberSlotGrid.Reposition ();
		m_CurrentSlotCnt++;
		ViewPositionUp ();
	}

	private void ViewPositionUp()
	{
		
		TweenPosition tp = m_NumberSlotGrid.GetComponent< TweenPosition > ();

		tp.from = m_GridPos;
		m_GridPos.y += SLOT_HEIGHT;

		tp.to = m_GridPos;

		tp.duration = 0.5f;
		tp.ResetToBeginning ();
		tp.PlayForward ();

		m_HistoryScrollView.transform.localPosition = m_ScrollViewPos;
		m_HistoryScrollView.gameObject.GetComponent< UIPanel > ().clipOffset = Vector2.zero;

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

	private void GameClear()
	{
		m_stopFlag = true;
		GameResultPopup.create ("success", m_timer, m_ballCnt);
	}

	private void GameOver()
	{
		m_stopFlag = true;
		GameResultPopup.create ("fail", m_timer, m_ballCnt);		
	}

	IEnumerator CountTime(float sec)
	{
		yield return new WaitForSeconds (sec);

		if (!m_stopFlag) 
		{
			m_timer--;
			m_TimerLabel.text = m_timer.ToString ();
			if (m_timer > 0) 
			{
				StartCoroutine (CountTime (sec));
			} 
			else 
			{
				GameOver ();
			}
		}
	}

	public void StartGame()
	{
		StartCoroutine (CountTime (1f));
	}

	public void Resume()
	{
		m_stopFlag = false;
		StartCoroutine (CountTime (1f));
	}

	public void OnClickedGo()
	{
		m_ballCnt--;
		GetInputNumber ();
		CheckAnswer ();
	}

	public void OnClickedStop()
	{
		m_stopFlag = true;
		GameStopPopup.create (m_timer, m_ballCnt, this.Resume);
	}

	public void OnClickedHelp()
	{
		m_stopFlag = true;
		HelpPopup.create (this.Resume);
	}

	void Awake() 
	{
		OnGetChildObject ();
		SetRandomAnswer ();

		m_timer = GAME_TIME;
		m_ballCnt = 10;

		m_TimerLabel.text = m_timer.ToString();
		m_BallCntLabel.text = m_ballCnt.ToString();
		m_CurrentSlotCnt = 0;
	}

	// Use this for initialization
	void Start () {
//		StartCoroutine( CountTime(1f) );
		GameStartCommentPopup.create(this.StartGame);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
