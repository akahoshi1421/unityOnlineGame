using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject stonePrefab;

    float span = 2.0f;
    float delta = 0;
    //int[] test = {1,3,1,2,3,2,1};
    int cnt = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(waitingController.gamePlayerOk){
            this.delta += Time.deltaTime;
            if(this.delta > this.span){
                this.delta = 0;
                if(this.cnt == 10) this.cnt = 0;

                GameObject stn = Instantiate(stonePrefab);//自分
                GameObject stn2 = Instantiate(stonePrefab);//相手
                int pos = waitingController.gameRandomRule[cnt];
                float pos_x = 0.0f;
                float pos_x2 = 0.0f;
                switch(pos){
                    case 1:
                        pos_x = -9.7f;
                        pos_x2 = 1.7f;
                        break;
                    case 2:
                        pos_x = -5.7f;
                        pos_x2 = 5.7f;
                        break;
                    case 3:
                        pos_x = -1.7f;
                        pos_x2 = 9.7f;
                        break;
                    
                }

                stn.transform.position = new Vector3(pos_x, 4, 0);
                stn2.transform.position = new Vector3(pos_x2, 4, 0);
                this.cnt++;
            }
        }
    }
}
