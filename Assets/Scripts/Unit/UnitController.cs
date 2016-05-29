using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {
    // player 의 정보를 받음
    [SerializeField]
    protected UnitEntity player;

    // 몬스터 정보를 받음
    [SerializeField]
    protected UnitEntity enemy;
    

    // 공격 가능한 시간
    protected float attackTime = 0f;

    public float knockbackPower = 4f;

    // unit 을 뒤로 밀쳐냄
    public void KnockBack( Collision2D unit )
    {
        Rigidbody2D rigid2D = unit.gameObject.GetComponent<Rigidbody2D>();
        if (rigid2D.velocity.y == 0)
        {
            Vector2 vec = (rigid2D.velocity.normalized * -1f) + Vector2.up;
            rigid2D.velocity = vec * knockbackPower;// new Vector2( Vector2.right.x * vec.x, 5f );
        }
    }
}
