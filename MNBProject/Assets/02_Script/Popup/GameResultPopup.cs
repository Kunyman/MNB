using UnityEngine;
using System.Collections;

public class GameResultPopup : Popup {

	private UILabel m_timeLabel;
	private UILabel m_ballCntLabel;

	private SwitchComp m_resultSwitch;
	private string m_caseString;
	private float m_remainTime;
	private int m_remainBallCnt;
	private PlaySceneManager.LAYER_TYPE m_currMode;

	public Callback m_selector = null;

	public static void create(string caseString, float remainTime, int remainBallCnt, PlaySceneManager.LAYER_TYPE currMode, Callback selector = null)
	{
		GameObject popup = PopupManager.Instance.show ("gameResultPopup");
		GameResultPopup gameResultPopup = popup.GetComponent< GameResultPopup > ();
		gameResultPopup.SetInfo (caseString, remainTime, remainBallCnt, currMode, selector);
		gameResultPopup.SetLabelData ();
	}

	private void SetInfo(string caseString, float remainTime, int remainBallCnt, PlaySceneManager.LAYER_TYPE currMode, Callback selector = null)
	{
		m_remainTime = remainTime;
		m_remainBallCnt = remainBallCnt;
		m_caseString = caseString;
		m_currMode = currMode;
		m_selector = selector;
	}

	private void SetLabelData()
	{
		m_timeLabel.text = m_remainTime.ToString ("F0");
		m_ballCntLabel.text = m_remainBallCnt.ToString ();
	}

	public void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren< Transform > ();

		foreach (Transform child in transforms) 
		{
			switch (child.name) 
			{
			case "Switch_GameResult":
				m_resultSwitch = child.GetComponent< SwitchComp > ();
				break;

			case "Score":
				m_timeLabel = child.FindChild ("time").FindChild ("Sprite_text").FindChild ("Label_count").GetComponent<UILabel> ();
				m_ballCntLabel = child.FindChild ("count").FindChild ("Sprite_text").FindChild ("Label_count").GetComponent<UILabel> ();
				break;
			}
		}
	}

	public void OnClickedHome()
	{
		PopupManager.Instance.hide ();
		PlaySceneManager.Instance.SetLayerType (PlaySceneManager.LAYER_TYPE.STAGE_LAYER);
	}

	public void OnClickedRestart()
	{
		PopupManager.Instance.hide ();
		PlaySceneManager.Instance.SetLayerType (m_currMode);
	}

	public void OnClickedTimeUp()
	{
		AdResultPopup.create ("time");
	}

	public void OnClickedCountUp()
	{
		AdResultPopup.create ("count");
	}

	void Awake ()
	{
		OnGetChildObject ();
	}

	// Use this for initialization
	void Start ()
	{
		m_resultSwitch.SetCase (m_caseString);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
