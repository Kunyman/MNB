using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {

	public delegate void Callback ();

	public virtual void onClickedClose()
	{
		PopupManager.Instance.hide ();
	}

	public virtual void OnGetChildObject()
	{
	}
}
