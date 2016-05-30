using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    // 플레이어를 받아옴
    public Transform player;

    // 카메라 움직이는 속도
    public float smoothMoveSpeed = 5f;

    // 배경 사이즈
    public float bgWidth;

    // 매니저 인스턴스
    private StageManager stageManager;
    
	void Start ()
    {
        Screen.SetResolution(Screen.height * 128 / 80, Screen.height, true);
        player = GameObject.Find("player").transform;
        stageManager = StageManager.Instance;

        StageCreate background = GameObject.Find("StageManager").GetComponent<StageCreate>();
        // 스테이지에 따라 배경길이가 달라지므로
        bgWidth = (stageManager.stage[stageManager.currentStage].bgCount-1) * background.fLocalSpriteSizeX;

    }
	
	void LateUpdate () {
        // 카메라 좌표 캐릭터 따라 부드럽게 움직이게 함.
        transform.position = Vector3.Lerp(transform.position, player.transform.position, smoothMoveSpeed);
        MoveLimit();
	}

    void MoveLimit()
    {
        Vector3 tempPos = transform.position;
        tempPos.x = Mathf.Clamp(transform.position.x, 0, bgWidth);
        tempPos.y = 0f;
        tempPos.z = -10f;

        transform.position = tempPos;
    }
}
