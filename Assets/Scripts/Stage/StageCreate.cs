using UnityEngine;
using System.Collections;

public class StageCreate : MonoBehaviour
{
    // 배경 
    public GameObject RootBackgroundObject; // 부모 오브젝트로 만들어진 배경이 이곳에 모임
    public GameObject pfBackground;         // 배경 프리팹
    private SpriteRenderer background;
    private Sprite sprite;
    public float fLocalSpriteSizeX;

    // 스테이지 매니저
    private StageManager stageManager;

    // 스테이지 초기 설정
    void Awake()
    {
        stageManager = StageManager.Instance;
        // 스프라이트의 크기를 받아옴
        fLocalSpriteSizeX = pfBackground.GetComponent<SpriteRenderer>().bounds.max.x * 2;
    }

    public void CreateStage()
    {
        // 스프라이트 이미지 받아옴
        sprite = Resources.Load<Sprite>("Textures/background" + ((int)(stageManager.currentStage*0.1f) + 1));

        // 부모 오브젝트 찾기
        RootBackgroundObject = GameObject.Find("BackGround");

        int BGCount = stageManager.stage[stageManager.currentStage].bgCount;
        // 배경 수 만큼 프리팹 생성
        for (int i = 0; i < BGCount; ++i)
        {
            GameObject temp = Instantiate(pfBackground, new Vector3( i * fLocalSpriteSizeX, 0f, 0.1f), Quaternion.identity) as GameObject;
            background = temp.GetComponent<SpriteRenderer>();

            // 스프라이트를 스테이지에 맞게 변경해준다.
            background.sprite = sprite;
            // 생성된 프리팹 부모 오브젝트 밑으로
            temp.transform.parent = RootBackgroundObject.transform;
        }
        BoxCollider2D box2D = GameObject.Find("FloorBoxCollider").GetComponent<BoxCollider2D>();
        box2D.offset = new Vector2(fLocalSpriteSizeX * 0.5f * (BGCount - 1), box2D.offset.y);
        box2D.size = new Vector2(BGCount * fLocalSpriteSizeX, box2D.size.y);   
        //pfEnemy = new GameObject[enemyCount];
    }
}
