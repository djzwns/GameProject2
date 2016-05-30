using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour {
    [SerializeField]
    private int MAXSTAGE = 7;
    private bool isSelect;
	// Use this for initialization
	void Start () {
        isSelect = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnGUI()
    {
        if (!isSelect)
        {
            for (int i = 0; i < MAXSTAGE; ++i)
            {
                if (GUI.Button(new Rect(Screen.width/4*i, Screen.height * 0.5f - (Screen.height / 4), Screen.width / 4, Screen.height * 0.5f),(Texture)Resources.Load("Textures/player")))
                {
                    isSelect = true;
                    StageManager.Instance.currentStage = i*10;
                    StageManager.Instance.GameStart();
                    GameObject.Find("Main Camera").GetComponent<CameraFollow>().CamReset();
                }
            }
        }
    }
}
