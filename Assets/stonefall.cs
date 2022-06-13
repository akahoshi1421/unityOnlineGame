using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stonefall : MonoBehaviour
{
    // Start is called before the first frame update
    private float startrate = -0.01f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, this.startrate, 0);
        this.startrate *= 1.002f;

        if(transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }
}
