using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PopupManager : MonoBehaviour {

	private List< GameObject > m_objList = new List< GameObject >();

	private static PopupManager instance = null;
	public static PopupManager Instance
	{
		get
		{
			return instance;
		}
	}

	public GameObject show ( string name )
	{
		Debug.Log (name);

		Object obj = Resources.Load (name);
		GameObject popup = Instantiate (obj) as GameObject;
		popup.transform.parent = this.transform;
		popup.transform.localScale = Vector3.one;
		popup.transform.localPosition = Vector3.zero;

		Transform[] transforms = popup.GetComponentsInChildren< Transform > ();
		foreach (Transform child in transforms) 
		{
			child.gameObject.layer = LayerMask.NameToLayer ("CameraDepth20");
		}

		m_objList.Add (popup);

		return popup;
	}

	public void hide()
	{
		if (m_objList.Count >= 1) 
		{
			GameObject deletePopup = m_objList [m_objList.Count - 1];
			m_objList.Remove (deletePopup);

			Destroy (deletePopup);
		}
	}

	public void AllDisable()
	{
		if (m_objList.Count == 0)
			return;

		foreach (GameObject obj in m_objList) 
		{
			obj.SetActive (false);
		}
	}

	public void AllEnable()
	{
		if (m_objList.Count == 0)
			return;

		foreach (GameObject obj in m_objList) 
		{
			obj.SetActive (true);
		}
	}

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
