using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stonefall : MonoBehaviour
{
    // Start is called before the first frame update
    private float startrate = -0.05f;
    int cnt = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(cnt % 5000 == 0){
            this.startrate *= 2.0f;
        }

        transform.Translate(0, this.startrate, 0);

        if(transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
        cnt++;
    }
}
