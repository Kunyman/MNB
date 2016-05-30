using UnityEngine;
using System.Collections;

public class HintSlot : MonoBehaviour {

	private Color[] m_colors = new Color[4];
	private int m_colorIndex = 0;

	private UILabel m_SlotLabel;


	void OnGetChildObject()
	{
		m_SlotLabel = gameObject.GetComponent< UILabel > ();
	}

	public void OnClicked()
	{
		m_colorIndex++;

		if (m_colorIndex == 4)
			m_colorIndex = 0;

		m_SlotLabel.color = m_colors [m_colorIndex];
	}

	void Awake()
	{
		OnGetChildObject ();
		
		m_colors [0] = Color.white;
		m_colors [1] = Color.yellow;
		m_colors [2] = Color.blue;
		m_colors [3] = Color.red;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
