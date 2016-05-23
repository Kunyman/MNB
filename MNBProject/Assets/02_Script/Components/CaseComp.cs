using UnityEngine;
using System.Collections;

[System.Serializable]
public class CaseComp : MonoBehaviour 
{
	public string Case;

	private void CheckValidation()
	{
		SwitchComp aceSwitch = GetComponentInParent<SwitchComp>();

		if(aceSwitch == null)
		{
			Debug.LogError("Case must be a child of Switch!!!");
		}
	}

	void Awake()
	{
		CheckValidation ();
	}


}