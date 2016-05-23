using UnityEngine;
using System.Collections;

public class GameResultPopup : Popup {

	private SwitchComp m_ResultSwitch;
	private string m_CaseString;

	public Callback Selector = null;

	public static void create(string caseString, Callback selector = null)
	{
		GameObject popup = PopupManager.Instance.show ("gameResultPopup");
		GameResultPopup gameResultPopup = popup.GetComponent< GameResultPopup > ();
		gameResultPopup.SetCaseString (caseString, selector);
	}

	private void SetCaseString(string caseString, Callback selector = null)
	{
		m_CaseString = caseString;
		Selector = selector;
	}

	public void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren< Transform > ();

		foreach (Transform child in transforms) 
		{
			switch (child.name) 
			{
			case "Switch_GameResult":
				m_ResultSwitch = child.GetComponent< SwitchComp > ();
				break;
			}
		}
	}

	void Awake ()
	{
		OnGetChildObject ();
	}

	// Use this for initialization
	void Start ()
	{
		m_ResultSwitch.SetCase (m_CaseString);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
