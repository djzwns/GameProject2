using UnityEngine;
using System.Collections;

// 고드름이 플레이어 머리 위를 따라다니다가 떨어짐
public class InducementIcicle : FallingIce {
    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        FollowPlayer();
	}

    void FollowPlayer()
    {
        if (!isFalling)
        {
            ice.transform.position = new Vector3(player.transform.position.x, 0) + ice.transform.parent.position;
        }
    }
}
