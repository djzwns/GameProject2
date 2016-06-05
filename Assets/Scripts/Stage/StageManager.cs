using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    // 스테이지 정보
    private int CurrentStage = 0;        // 유저가 선택한 스테이지
    public int currentStage { get { return CurrentStage; } set { CurrentStage = value; } }

    // 최고 달성 스테이지
    [SerializeField]
    private int achieveStage = 0;
    public int AchieveStage { get{ return achieveStage; } }

    private List<StageInfo> Stage;
    public List<StageInfo> stage { get { return Stage; } }

    // 플레이어
    GameObject player;
    Vector3 initPlayerPos;

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
        initPlayerPos = player.transform.position;
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
        PlayerInit();
        enemyManager.EndSpawn();
        StageCreator.RemoveStage();
    }

    private void PlayerInit()
    {
        player.transform.position = initPlayerPos;
        player.GetComponent<PlayerController>().PlayerReset();
        player.SetActive(false);
    }
}
