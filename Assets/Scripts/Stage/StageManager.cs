using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    // 스테이지 정보
    private int CurrentStage = 0;        // 유저가 선택한 스테이지
    public int currentStage { get { return CurrentStage; } set { CurrentStage = value; } }
    private List<StageInfo> Stage;
    public List<StageInfo> stage { get { return Stage; } }

    // 플레이어
    GameObject player;

    // 몬스터 스폰 매니저
    public EnemySpawn enemyManager;

    // 스테이지 생성 매니저
    private StageCreate StageCreator;

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
                instance.Stage = StageXml.StageLoad(Application.dataPath + "/Resources/Stage.xml");
            }
            return instance;
        }
    }

    void Awake()
    {
        StageCreator = GetComponent<StageCreate>();
        player = GameObject.Find("player");
    }

    void Start()
    {
        player.SetActive(false);
    }


    // 스테이지 선택 시 호출
    public void GameStart()
    {
        GAMEMODE.Instance.gamemode = stage[currentStage].gameMode;
        StageCreator.CreateStage();
        player.SetActive(true);
        enemyManager.StartSpawn();
    }

    // 스테이지 끝나면( 이기거나 지거나 )
    public void GameEnd()
    {
        player.SetActive(false);
        enemyManager.EndSpawn();
    }
}
