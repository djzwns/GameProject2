using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    // 리지드바디 사용
    private Rigidbody2D rigid2D;
    
    // 수평 키 입력 시 사용
    private float moving = 0f;

    // 캐릭터 이동 속도
    public float speed = 4.0f;

    // 캐릭터 점프력
    private float jumpPower;

    // 스프라이트 렌더러
    private SpriteRenderer spriteRenderer;

    // 게임 모드에 따라 움직임이 달라짐.
    [SerializeField]
    private GAMEMODE GameMode;

	// Use this for initialization
	void Awake () {
        rigid2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        speed = Player.Instance.speed;
        jumpPower = Player.Instance.JumpPower;
        GameMode = GAMEMODE.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameMode.gamemode == GAMEMODE.Gamemode.ARCADE)
        {
            Move();
            Jump();
            rigid2D.velocity = new Vector2(moving * speed, rigid2D.velocity.y);

            // axis 가 0 이 아닐 때 즉, 움직이고 있을 때 방향 따라 스프라이트를 플립 해줌.
            // axis < 0 의 결과에 따라 true, false 를 반환 하게 됨.
            if (moving != 0)
                spriteRenderer.flipX = moving < 0;
        }
        else if (GameMode.gamemode == GAMEMODE.Gamemode.STORY)
        {
            if (EndFalling())
                rigid2D.velocity = new Vector2( speed, rigid2D.velocity.y);// (Vector2.right * speed * 0.1f) + rigid2D.velocity;
        }
	}

    // 터치하고 드래그 하면 움직임
    public void Move()
    {
        moving = Input.GetAxis("Horizontal");
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //    moving = Input.GetTouch(0).deltaPosition.x;
        //}
        //else
        //    moving = 0f;
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
