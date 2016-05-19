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
    // 소환 된 수
    private int currentEnemy = 0;

    // 스테이지 정보
    private StageInfo stage;

    // 몬스터 종류
    private enum EnemyType
    {
        BASIC   = 0,
        ATTACK  = 1,
        DEFENCE = 2,
        BOSS    = 4
    }

	// Use this for initialization
	void Start () {
        enemyCount = stage.attackEnemyCount + stage.basicEnemyCount + stage.defenceEnemyCount;

        // 스폰 코루틴 실행
        StartCoroutine("Spawn");
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
            GameObject temp = Instantiate(pfEnemy[stage.stageNumber % 10], spawn.transform.position, Quaternion.identity) as GameObject;

            // 생성된 오브젝트를 spawn 하위 오브젝트로 넣어줌
            temp.transform.parent = spawn.transform;
            ++currentEnemy;
        }
        isSpawn = false;
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
        Destroy(spawn);
    }

    /// 오브젝트 활성화 시키기
    public void StartSpawn()
    {
        gameObject.SetActive(true);
    }

    /// 비활성화
    public void EndSpawn()
    {
        gameObject.SetActive(false);
    }
}
