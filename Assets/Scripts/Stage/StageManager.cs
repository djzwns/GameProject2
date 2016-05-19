using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{

    // 스테이지 정보
    public int currentStage = 0;        // 유저가 선택한 스테이지
    public List<StageInfo> stage;

    // 매니저 인스턴스
    private static StageManager instance;
    public static StageManager Instance
    {
        get
        {
            if (instance == null)
            {
                //StageXml.StageSave(new StageInfo(), Application.dataPath + "/StreamingAssets/Stage.xml");
                instance = (StageManager)GameObject.FindObjectOfType(typeof(StageManager));
                instance.stage = StageXml.StageLoad(Application.dataPath + "/StreamingAssets/Stage.xml");
            }
            return instance;
        }
    }

    void Awake()
    {
        GAMEMODE.Instance.gamemode = stage[currentStage].gameMode;
    }
}
