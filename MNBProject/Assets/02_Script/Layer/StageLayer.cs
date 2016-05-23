using UnityEngine;
using System.Collections;

public class StageLayer : MonoBehaviour {


	public void onClickedRecord()
	{
		
	}

	public void onClickedHelp()
	{
		HelpPopup.create ();
	}

	public void onClickedEasyMode()
	{
		
	}

	public void onClickedHardMode()
	{
		PlaySceneManager.Instance.SetLayerType (PlaySceneManager.LAYER_TYPE.HARD_MODE_LAYER);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
