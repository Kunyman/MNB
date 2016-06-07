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

	public void OnClickedBack()
	{
		PlaySceneManager.Instance.SetLayerType (PlaySceneManager.LAYER_TYPE.GAME_START_LAYER);
	}

	public void onClickedEasyMode()
	{
		PlaySceneManager.Instance.SetLayerType (PlaySceneManager.LAYER_TYPE.EASY_MODE_LAYER);
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
