using UnityEngine;
using System.Collections;

public class HelpPopup : Popup {

	private Callback m_selector = null;
	public static void create(Callback selector = null)
	{
		GameObject popup = PopupManager.Instance.show ("helpPopup");
		HelpPopup helpPopup = popup.GetComponent< HelpPopup > ();
		helpPopup.SetData (selector);
	}

	private void SetData(Callback selector)
	{
		m_selector = selector;
	}

	public void OnClickedOk()
	{
		if (m_selector != null)
			m_selector ();

		PopupManager.Instance.hide ();
	}

}
