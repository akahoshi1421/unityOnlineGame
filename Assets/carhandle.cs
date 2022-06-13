using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carhandle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waitingController.instance2.gamePlyerOk){
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                Vector3 yourCarPos = transform.position;//-9.7,-5.7,-1.7
                float car_x = yourCarPos.x;
                switch(car_x){
                    case -9.7f:
                        transform.position = new Vector3(-5.7f, -3.5f, 0);
                        break;
                    case -5.7f:
                        transform.position = new Vector3(-1.7f, -3.5f, 0);
                        break;
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                Vector3 yourCarPos = transform.position;//-9.7,-5.7,-1.7
                float car_x = yourCarPos.x;
                switch(car_x){
                    case -1.7f:
                        transform.position = new Vector3(-5.7f, -3.5f, 0);
                        break;
                    case -5.7f:
                        transform.position = new Vector3(-9.7f, -3.5f, 0);
                        break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("衝突");
    }
}
