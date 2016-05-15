using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    // 플레이어를 받아옴
    public Transform player;

    // 카메라 움직이는 속도
    public float smoothMoveSpeed = 5f;

    // 배경 사이즈
    public float bgWidth;
    
	void Start ()
    {
        player = GameObject.Find("player").transform;

        StageCreate background = GameObject.Find("BackGround").GetComponent<StageCreate>();
        bgWidth = (background.bgCount-1) * background.fLocalSpriteSizeX;

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
