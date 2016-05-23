using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwitchComp : MonoBehaviour {
	private string m_strCase;
	private List<CaseComp> ListCaseComponent = new List<CaseComp>();

	public void SetCase(string strCase)
	{
		m_strCase = strCase;

		foreach(CaseComp caseComponent in ListCaseComponent)
		{
			if(m_strCase == caseComponent.Case)
			{
				caseComponent.gameObject.SetActive(true);
			}
			else
			{
				//DestroyImmediate(aceSwitchComponent.gameObject);
				caseComponent.gameObject.SetActive(false);
			}
		}
	}

	public string GetCase()
	{
		return m_strCase;
	}

	private void AddCase()
	{
		ListCaseComponent.Clear ();

		CaseComp[] aceCases = gameObject.GetComponentsInChildren<CaseComp> ();

		foreach(CaseComp caseComp in aceCases)
		{
			if(caseComp.transform.parent == transform)
			{
				ListCaseComponent.Add(caseComp);
			}
		}
	}

	void Awake()
	{
		AddCase ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
