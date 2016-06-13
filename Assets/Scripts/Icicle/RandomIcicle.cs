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
            x = IcicleClamp(Random.Range(x - 5f, x + 5f));

            ice.transform.position = new Vector3(x, 0) + ice.transform.parent.position;
        }
    }

    float IcicleClamp(float _x)
    {
        return Mathf.Clamp(_x, -6.4f, 6.4f);
    }
}
