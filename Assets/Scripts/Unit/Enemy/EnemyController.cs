using UnityEngine;
using System.Collections;

public class EnemyController : UnitController {

	// Use this for initialization
	void Awake () {
        enemy = ScriptableObject.CreateInstance<Enemy>() as Enemy;
        player = Player.Instance;
	}

    void Update()
    {
        attackTime += Time.deltaTime;
        if (enemy.IsDead())
            gameObject.SetActive(false);
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Player"))
        {
            // 힘이 플레이어보다 세면 밀처냄
            if (enemy.power > player.power)
            {
                KnockBack(coll);
            }

            // 다음 공격까지 약간의 텀을 만듬
            // 한번에 여러번 때리는 것 방지
            if (attackTime >= enemy.AttackSpeed)
            {
                attackTime = 0;
                enemy.Attack(player);
            }
        }
    }

    // 적 정보 보내줌
    public UnitEntity Enemy { get { return enemy; }}
}
