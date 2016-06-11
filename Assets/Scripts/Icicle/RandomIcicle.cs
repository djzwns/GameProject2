using UnityEngine;
using System.Collections;

public class RandomIcicle : FallingIce {
    public GameObject player;

    // Update is called once per frame
    void LateUpdate()
    {
        RandomPosition();
    }

    void RandomPosition()
    {
        if (!isFalling)
        {
            float x = player.transform.position.x;
            ice.transform.position = new Vector3(Random.Range(x - 5f, x + 5f), 0) + ice.transform.parent.position;
        }
    }
}
