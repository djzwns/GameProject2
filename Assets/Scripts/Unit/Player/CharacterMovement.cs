using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    // 리지드바디 사용
    private Rigidbody2D rigid2D;
    
    // 수평 키 입력 시 사용
    private int istouch = 1;

    // 캐릭터 이동 속도
    private float speed = 3.0f;

    // 캐릭터 점프력
    private float jumpPower;

    // 스프라이트 렌더러
    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake () {
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = Player.Instance.speed;
        jumpPower = Player.Instance.JumpPower;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //else if (GameMode.gamemode == GAMEMODE.Gamemode.STORY)
        //{
        if (EndFalling())
            rigid2D.velocity = new Vector2(speed * istouch, rigid2D.velocity.y);// (Vector2.right * speed * 0.1f) + rigid2D.velocity;
        //}
        if (GAMEMODE.Instance.gamemode == GAMEMODE.Gamemode.ARCADE)
        {
            Move();
            //Jump();
            //rigid2D.velocity = new Vector2(moving * speed, rigid2D.velocity.y);
        }
        else
            istouch = 1;

        // 터치 상태면 왼쪽을 바라봄
        spriteRenderer.flipX = istouch < 0;
    }

    // 터치하고 드래그 하면 움직임
    public void Move()
    {
        //moving = Input.GetAxis("Horizontal");

        // 누르고 있으면 왼쪽으로 아니면 오른쪽으로 이동
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
                istouch = -1;
            //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            else
                istouch = 1;
        }
        else
        {
            if (Input.GetMouseButton(0))
                istouch = -1;
            else
                istouch = 1;
        }
    }

    // 두번 터치 했을 때 점프를 하게됨
    public void Jump()
    {
        if (Input.GetAxis("Vertical") > 0 && EndFalling())
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, jumpPower);
        }
        //if (Input.GetTouch(0).tapCount > 1 && Input.GetTouch(0).phase == TouchPhase.Ended && !isJumping)
        //{
        //    isJumping = true;
        //    rigid2D.velocity = new Vector2(rigid2D.velocity.x, speed);
        //}
    }

    // 낙하 종료 시 true 반환
    bool EndFalling()
    {
        // y축 속도가 0이면 착지 상태 이므로 점프 종료
        if (rigid2D.velocity.y == 0)
        {
            return true;
        }
        return false;
    }
}
