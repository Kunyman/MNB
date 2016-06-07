using UnityEngine;
using System.Collections;

public class PlaySceneManager : MonoBehaviour {

	public enum LAYER_TYPE 
	{
//		MAIN_LAYER,
		GAME_START_LAYER,
		STAGE_LAYER,
		HARD_MODE_LAYER,
		EASY_MODE_LAYER,
	}

	private string[] _layerPrefabNameArr = 
	{
		"gameStartLayer",
		"stageLayer",
		"hardModeLayer",
		"easyModeLayer"
	};

	private HardModeLayer m_hardModeLayer;

	private GameObject m_currentLayer = null;
	private GameObject m_prevLayer = null;
	private LAYER_TYPE m_layerType;

	public void SetLayerType( LAYER_TYPE layerType )
	{
		GameObject prevLayer = null;
		GameObject nextLayer = null;

		m_layerType = layerType;

		Object nextObj = Resources.Load( _layerPrefabNameArr [(int)m_layerType] );
		nextLayer = Instantiate( nextObj ) as GameObject;

		if (nextLayer) 
		{
			nextLayer.SetActive (true);
			nextLayer.transform.parent = transform;
			nextLayer.transform.localPosition = Vector3.zero;
			nextLayer.transform.localScale = Vector3.one;
		}

		prevLayer = m_currentLayer;

		m_currentLayer = nextLayer;


		switch (m_layerType) 
		{
		case LAYER_TYPE.HARD_MODE_LAYER:			
			m_hardModeLayer = m_currentLayer.GetComponent< HardModeLayer > ();
			break;

		default:
			break;

		}

		if (prevLayer) 
		{
			prevLayer.SetActive (false);
			Destroy (prevLayer);
			prevLayer = null;
		}

	}



	public void SetDualLayerType( LAYER_TYPE layerType, bool isOnPrevLayer = false, bool isDestroyPreLayer = false )
	{
		m_layerType = layerType;

		//현재 레이어를 끄고 새 레이어 생성, 팝업 off
		if (isOnPrevLayer == false) 
		{
			m_prevLayer = m_currentLayer;

			Object nextObj = Resources.Load (_layerPrefabNameArr [(int)m_layerType]);
			m_currentLayer = Instantiate (nextObj) as GameObject;

			if (m_currentLayer) {
				m_currentLayer.SetActive (true);
				m_currentLayer.transform.parent = transform;
				m_currentLayer.transform.localPosition = Vector3.zero;
				m_currentLayer.transform.localScale = Vector3.one;
			}
				
			if (m_prevLayer != null) 
			{
				m_prevLayer.SetActive (false);
			}

			//팝업이 있다면 모두 끈다
//			PopupManager.Instance.AllDisable ();

		} 
		else 	//비활성화 되어있는 레이어 on, 현재 레이어 제거, 팝업 on
		{
			GameObject currentLayer = m_currentLayer;
			m_currentLayer = m_prevLayer;
			m_currentLayer.SetActive (true);
			currentLayer.SetActive (false);
			Destroy (currentLayer);
			m_prevLayer = null;

			//팝업이 있다면 모두 켠다
//			PopupManager.Instance.AllEnable ();
		}
	}

	private static PlaySceneManager instance;
	public static PlaySceneManager Instance
	{
		get 
		{
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		instance = this;
		SetLayerType (LAYER_TYPE.GAME_START_LAYER);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
