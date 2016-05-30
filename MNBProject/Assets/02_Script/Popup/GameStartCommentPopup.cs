using UnityEngine;
using System.Collections;

public class GameStartCommentPopup : Popup {

	public Callback m_selector = null;

	public static void create(Callback selector)
	{
		GameObject popup = PopupManager.Instance.show ("gameStartCommentPopup");
		GameStartCommentPopup gameStartCommentPopup = popup.GetComponent< GameStartCommentPopup > ();
		gameStartCommentPopup.SetData (selector);
	}

	private void SetData(Callback selector)
	{
		m_selector = selector;
	}

	public void OnClickedStart()
	{
		m_selector ();
		PopupManager.Instance.hide ();
	}

}
