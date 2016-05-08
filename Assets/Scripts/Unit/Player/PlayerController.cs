using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    private UnitEntity player;
    private float attackTime;

	// Use this for initialization
	void Awake () {
        player = Player.Instance;
        attackTime = 0;
	}
	
	// Update is called once per frame
	void Update () {

        attackTime += Time.deltaTime;
    }

    // 충돌해 있을 때 공격 시간 마다 공격
    void OnCollisionStay2D(Collision2D coll)
    {
        if (attackTime >= player.AttackSpeed)
        {
            attackTime = 0;
            player.Attack(player);
        }
    }
}
