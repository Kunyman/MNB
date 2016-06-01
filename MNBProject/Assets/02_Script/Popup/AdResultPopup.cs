using UnityEngine;
using System.Collections;

public class AdResultPopup : Popup {

	private SwitchComp m_AdSwitch;
	private string m_CaseString;
	private Callback m_selector = null;

	public static void create(string caseString, Callback selector = null)
	{
		GameObject popup = PopupManager.Instance.show ("adResultPopup");
		AdResultPopup adResultPopup = popup.GetComponent< AdResultPopup > ();
		adResultPopup.SetCaseString (caseString, selector);
	}
		
	private void SetCaseString(string caseString, Callback selector = null)
	{
		m_CaseString = caseString;
		m_selector = selector;
	}

	private void SetLabelData()
	{
		
	}

	private void OnGetChildObject()
	{
		Transform[] transforms = gameObject.GetComponentsInChildren< Transform > ();

		foreach (Transform child in transforms) 
		{
			switch (child.name) 
			{
			case "Switch":
				m_AdSwitch = child.GetComponent< SwitchComp > ();
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
		m_AdSwitch.SetCase (m_CaseString);
		Invoke ("onClickedClose", 2.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
}