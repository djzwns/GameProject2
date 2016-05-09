using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private UnitEntity player;
    private UnitEntity enemy;
    private float attackTime;

	// Use this for initialization
	void Awake () {
        player = Player.Instance;
        attackTime = 0;
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
            if (attackTime >= player.AttackSpeed)
            {
                attackTime = 0;
                player.Attack(enemy);
            }
        }
    }
}
