using UnityEngine;
using System.Collections;

public class FallingIce : MonoBehaviour {
    public GameObject ice;

    private SpriteRenderer iceSprite;
    private Rigidbody2D iceRigid2D;

    private StageManager stage;
    protected float spawnTime = 2f;

    protected bool isFalling = false;

    void OnEnable()
    {
        spawnTime = 9f / (stage.currentStage % 10) + Random.Range(0f, 3f);
        //IcicleReset();
        StartCoroutine(FadeIn());
    }

    void OnDisable()
    {
        IcicleReset();
        StopCoroutine(FadeIn());
    }

    // 이미지 점점 보이게 fixedUpdate 주기마다 알파 값 증가 시켜줌.
    IEnumerator FadeIn()
    {
        float i = 0;
        yield return new WaitForSeconds(spawnTime);
        isFalling = true;
        while (i < 1f)
        {
            yield return new WaitForSeconds(0.001f);
            Color ImageColor = new Color(1, 1, 1, i);
            iceSprite.color = ImageColor;
            i += 0.01f;
        }

        FallingIcicle();
    }

    // Use this for initialization
    void Awake () {
        iceSprite = GetComponent<SpriteRenderer>();
        iceRigid2D = GetComponent<Rigidbody2D>();
        iceRigid2D.gravityScale = 0;

        stage = StageManager.Instance;
	}

    // 얼음 떨어짐
    void FallingIcicle()
    {
        iceRigid2D.gravityScale = 1;
        StopCoroutine(FadeIn());
    }

    // 고드름 떨어져서 멈추면 리셋
    void IcicleReset()
    {
        //if (isFalling && iceRigid2D.velocity.y == 0)
        //{
        ice.transform.position = ice.transform.parent.position;
        isFalling = false;
        iceSprite.color = new Color(1, 1, 1, 0);
        iceRigid2D.gravityScale = 0;
        //}
    }

    // 충돌 처리
    void OnCollisionEnter2D(Collision2D coll)
    {
        // 부딪힌 오브젝트가 플레이어일 경우 데미지
        if (coll.gameObject.tag == "Player")
        {
            coll.gameObject.GetComponent<PlayerController>().SuddenlyDeath();
        }

        IcicleReset();
        StartCoroutine(FadeIn());
    }
}
