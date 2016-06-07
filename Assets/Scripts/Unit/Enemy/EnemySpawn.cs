using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour {
    // 몬스터 생성 가능 여부
    private bool isSpawn = false;

    // 몬스터 생성 위치
    private GameObject spawn;
    public GameObject pfSpawn;

    // 몬스터 프리팹
    public GameObject[] pfEnemy;

    // 생성되는 주기
    public float spawnTime = 1f;

    // 소환 될 몬스터 수 
    private int enemyCount;
    public int EnemeyCount { get{ return enemyCount; } }
    private int basicEnemy = 0;
    private int attackEnemy = 0;
    private int defenceEnemy = 0;

    // 소환 몬스터 생성 순서
    private EnemyType[] arrApear;

    // 현재 소환 된 수
    private int currentEnemy = 0;

    // 스테이지 정보
    private StageInfo stage;

    // 몬스터 종류
    public enum EnemyType
    {
        BASIC   = 0,
        STRONG  = 1,
        INSANE = 2,
        BOSS    = 4
    }

	// Use this for initialization
	void Start () {
        //enemyCount = stage.strongEnemyCount + stage.basicEnemyCount + stage.insaneEnemyCount;
        //arrApear = new EnemyType[enemyCount];
        //CreateEnemyList();
        //EnemyListSuffle();

        //// 스폰 코루틴 실행
        //StartCoroutine("Spawn");
    }
	
	// Update is called once per frame
	void Update ()
    {
        // 몬스터 스폰 끝내기
        if( !isSpawn )
            StopCoroutine("Spawn");
    }

    /// spawnTime 마다 몬스터 뽑
    IEnumerator Spawn()
    {
        // enemyCount 만큼 몬스터를 뽑았으면 정지
        while (currentEnemy < enemyCount)
        {
            yield return new WaitForSeconds(spawnTime);
            int stageNum = (int)(stage.stageNumber * 0.1f);

            GameObject temp = Instantiate(pfEnemy[stageNum], spawn.transform.position, Quaternion.identity) as GameObject;
            Enemy tempEnemy = (Enemy)temp.GetComponent<EnemyController>().Enemy;
            SetEnemyName(tempEnemy, stageNum);
            tempEnemy.LoadData();

            SpriteRenderer tempSprite = temp.GetComponent<SpriteRenderer>();

            EnemyType type = arrApear[currentEnemy];
            if (EnemyType.BASIC == type)
            {
                temp.transform.localScale = new Vector2(0.8f, 0.8f);
                tempEnemy.type = EnemyType.BASIC;
                tempSprite.sprite = Resources.Load<Sprite>("Textures/" + temp.GetComponent<EnemyController>().Enemy.unitName);
            }
            else if (EnemyType.STRONG == type)
            {
                temp.transform.localScale = new Vector2(0.5f, 0.5f);
                tempEnemy.type = EnemyType.STRONG;
                tempSprite.sprite = Resources.Load<Sprite>("Textures/" + temp.GetComponent<EnemyController>().Enemy.unitName + "1");
            }
            else if (EnemyType.INSANE == type)
            {
                tempEnemy.type = EnemyType.INSANE;
                tempSprite.sprite = Resources.Load<Sprite>("Textures/" + temp.GetComponent<EnemyController>().Enemy.unitName + "2");
            }
            // 타입별 능력치 세팅
            tempEnemy.AttributeSet(tempEnemy.type);
            // 생성된 오브젝트를 spawn 하위 오브젝트로 넣어줌
            temp.transform.parent = spawn.transform;
            ++currentEnemy;
        }
        isSpawn = false;
    }

    // 출현 몬스터 리스트 
    void CreateEnemyList()
    {
        while( currentEnemy < enemyCount )
        {
            if (basicEnemy < stage.basicEnemyCount)
            {
                arrApear[currentEnemy] = EnemyType.BASIC;
                ++basicEnemy;
            }

            else if (attackEnemy < stage.strongEnemyCount)
            {
                arrApear[currentEnemy] = EnemyType.STRONG;
                ++attackEnemy;
            }
            else if (defenceEnemy < stage.insaneEnemyCount)
            {
                arrApear[currentEnemy] = EnemyType.INSANE;
                ++defenceEnemy;
            }
            ++currentEnemy;
        }
        currentEnemy = 0;
    }

    // 리스트 섞기
    void EnemyListSuffle()
    {
        while (currentEnemy < enemyCount)
        {
            int i = Random.Range(currentEnemy, enemyCount - 1);
            EnemyType temp = arrApear[currentEnemy];
            arrApear[currentEnemy] = arrApear[i];
            arrApear[i] = temp;
            ++currentEnemy;
        }
        currentEnemy = 0;
    }

    /// 몬스터 이름 설정
    void SetEnemyName(Enemy _enemy, int _stage)
    {
        switch (_stage)
        {
            case 0:
                _enemy.unitName = "goblin";
                break;
            case 1:
                _enemy.unitName = "alligator";
                break;
            case 2:
                _enemy.unitName = "snowman";
                break;
            case 3:
                _enemy.unitName = "alligator";
                break;
            case 4:
                _enemy.unitName = "alligator";
                break;
            case 5:
                _enemy.unitName = "alligator";
                break;
            case 6:
                _enemy.unitName = "alligator";
                break;
        }
    }

    ///
    void InitEnemy()
    {
        enemyCount = stage.strongEnemyCount + stage.basicEnemyCount + stage.insaneEnemyCount;
        arrApear = new EnemyType[enemyCount];
        CreateEnemyList();
        EnemyListSuffle();

        // 스폰 코루틴 실행
        StartCoroutine("Spawn");
    }

    /// 스크립트 활성화 될 때 실행
    void OnEnable()
    {
        stage = StageManager.Instance.stage[StageManager.Instance.currentStage];
        float backgroundSizeX = GameObject.Find("StageManager").GetComponent<StageCreate>().fLocalSpriteSizeX;

        // 생성 위치 만들고 배경 크기에 맞게 위치 조정
        spawn = Instantiate(pfSpawn);
        spawn.transform.Translate(new Vector3((stage.bgCount - 1) * backgroundSizeX + 5f, 0, 0));

        isSpawn = true;
    }

    /// 스크립트 비활성화 될 때 소환된 몬스터 파괴 ( 메뉴로 돌아가거나 할 때 비활성화)
    void OnDisable()
    {
        currentEnemy = 0;
        basicEnemy = 0;
        attackEnemy = 0;
        defenceEnemy = 0;
        Destroy(spawn);
    }

    /// 오브젝트 활성화 시키기
    public void StartSpawn()
    {
        gameObject.SetActive(true);
        InitEnemy();
    }

    /// 비활성화
    public void EndSpawn()
    {
        gameObject.SetActive(false);
    }
}
