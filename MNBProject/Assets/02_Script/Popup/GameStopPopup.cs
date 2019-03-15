using UnityEngine;
using System.Collections;

public class GameStopPopup : Popup {

	private UILabel m_timeLabel;
	private UILabel m_ballCntLabel;

	private int m_remainBallCnt;
	private float m_remainTime;
	private Callback m_selector;

	public static void create(float remainTime, int remainBallCnt, Callback selector = null)
	{
		GameObject popup = PopupManager.Instance.show ("gameStopPopup");
		GameStopPopup gameStopPopup = popup.GetComponent< GameStopPopup > ();

		gameStopPopup.SetData (remainTime, remainBallCnt, selector);
		gameStopPopup.SetLabelData ();
	}

	private void SetData(float remainTime, int remainBallCnt, Callback selector)
	{
		m_selector = selector;	
		m_remainTime = remainTime;
		m_remainBallCnt = remainBallCnt;
	}

	private void SetLabelData()
	{
		m_timeLabel.text = m_remainTime.ToString ("F0");
		m_ballCntLabel.text = m_remainBallCnt.ToString ();
	}

	public void OnClickedHome()
	{
		PopupManager.Instance.hide ();
		PlaySceneManager.Instance.SetLayerType (PlaySceneManager.LAYER_TYPE.STAGE_LAYER);
	}

	public void OnClickedTimeUp()
	{
		AdResultPopup.create ("time");
	}

	public void OnClickedCountUp()
	{
		AdResultPopup.create ("count");
	}

	public void OnClickedResume()
	{
		m_selector ();
		PopupManager.Instance.hide ();
	}
	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren<Transform> ();

		foreach (Transform child in transforms) 
		{
			switch (child.name) 
			{
			case "Score":
				m_timeLabel = child.Find ("time").Find ("Sprite_text").Find ("Label_count").GetComponent<UILabel> ();
				m_ballCntLabel = child.Find ("count").Find ("Sprite_text").Find ("Label_count").GetComponent<UILabel> ();
				break;
			}
		}
	}

	void Awake()
	{
		OnGetChildObject ();
	}




}
