using UnityEngine;
using System.Collections;

public class EnemyController : UnitController {

	// Use this for initialization
	void Awake () {
        enemy = ScriptableObject.CreateInstance<Enemy>() as Enemy;
        player = Player.Instance;
        damageDisplay = DamageDisplay.Instance;
    }

    void Update()
    {
        attackTime += Time.deltaTime;
        if (enemy.IsDead())
        {
            gameObject.SetActive(false);
            player.EnemyCounting();
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Player"))
        {
            Vector3 headPos;
            headPos = coll.gameObject.GetComponent<HPBar>().HeadPosition.transform.position;// + new Vector3(1f, 0.5f);
            //headPos = coll.transform.position + new Vector3(coll.gameObject.GetComponent<BoxCollider2D>().size.x*2f, coll.gameObject.GetComponent<BoxCollider2D>().size.y);
            // 다음 공격까지 약간의 텀을 만듬
            // 한번에 여러번 때리는 것 방지
            if (attackTime >= enemy.AttackSpeed)
            {
                attackTime = 0;
                damageDisplay.CreateDamageText(enemy.Attack(player), headPos);
                // 힘이 플레이어보다 세면 밀처냄
                if (enemy.power > player.power)
                {
                    KnockBack(coll);
                }
            }
        }
    }

    // 적 정보 보내줌
    public UnitEntity Enemy { get { return enemy; }}
}
