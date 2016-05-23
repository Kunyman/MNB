using UnityEngine;
using System.Collections;

public class GameStartLayer : MonoBehaviour {


	public void OnClickedGameStart()
	{
		PlaySceneManager.Instance.SetLayerType (PlaySceneManager.LAYER_TYPE.STAGE_LAYER);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
