using UnityEngine;
using System.Collections;

public class HelpPopup : Popup {

	public static void create()
	{
		GameObject popup = PopupManager.Instance.show ("helpPopup");
		HelpPopup helpPopup = popup.GetComponent< HelpPopup > ();
	}

}
