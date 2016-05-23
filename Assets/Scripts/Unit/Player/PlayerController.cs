using UnityEngine;
using System.Collections;

public class PlayerController : UnitController
{

	// Use this for initialization
	void Awake () {
        player = Player.Instance;
	}
	
	// Update is called once per frame
	void Update () {

        attackTime += Time.deltaTime;
        if (player.IsDead())
            gameObject.SetActive(false);
    }

    // 충돌해 있을 때 공격 시간 마다 공격
    void OnCollisionStay2D(Collision2D coll)
    {
        // 충돌한 유닛dl 적일 때 정보를 받아옴.
        if (coll.gameObject.tag.Equals("enemy"))
        {
            enemy = coll.gameObject.GetComponent<EnemyController>().Enemy;
        }
        else
        {
            enemy = null;
        }

        if (enemy != null)
        {

            // 다음 공격까지 약간의 텀을 만듬
            // 한번에 여러번 때리는 것 방지
            if (attackTime >= player.AttackSpeed)
            {
                attackTime = 0;
                player.Attack(enemy);

                // 힘이 적보다 더 세면 밀처냄
                if (player.power > enemy.power)
                {
                    KnockBack(coll);
                }
            }
        }
    }


    // 게임이 실행 중이면 오브젝트 활성화
    void IsPlayGame()
    {
        gameObject.SetActive(true);
    }
}
