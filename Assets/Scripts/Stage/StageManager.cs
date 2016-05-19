using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    // 스테이지 정보
    private int CurrentStage = 0;        // 유저가 선택한 스테이지
    public int currentStage { get { return CurrentStage; } }
    private List<StageInfo> Stage;
    public List<StageInfo> stage { get { return Stage; } }

    // 몬스터 매니저
    public EnemySpawn enemyManager;

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
                instance.Stage = StageXml.StageLoad(Application.dataPath + "/StreamingAssets/Stage.xml");
            }
            return instance;
        }
    }

    void Awake()
    {
        GAMEMODE.Instance.gamemode = stage[currentStage].gameMode;
        enemyManager.StartSpawn();
    }
}
