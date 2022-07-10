using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;

public class StoneGenerator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject stonePrefab;
    private WebSocket ws4;

    float span = 2.0f;
    float delta = 0;
    //int[] test = {1,3,1,2,3,2,1};
    int cnt = 0;

    [System.Serializable]
    public class StonePos
    {
        public int stonepos;
    }

    StonePos sp;
    bool key = false;

    void Start()
    {
        ws4 = new WebSocket(matchingManagerScript.domain + "ws/stonefall/" + matchingManagerScript.room + "/");
        ws4.OnOpen += (sender, e) =>
        {
            //Debug.Log("WebSocket Open");
        };

        ws4.OnMessage += (sender, e) =>
        {
            //どのレーンに置くかの設定
            sp = JsonUtility.FromJson<StonePos>(e.Data);
            key = true;
        };

        ws4.OnError += (sender, e) =>
        {
            //Debug.Log("WebSocket Error Message: " + e.Message);
        };

        ws4.OnClose += (sender, e) =>
        {
            //Debug.Log("WebSocket Close");
        };

        ws4.Connect();
    }

    void StoneSet()
    {
        GameObject stn = Instantiate(stonePrefab);//自分
        GameObject stn2 = Instantiate(stonePrefab);//相手
        int pos = sp.stonepos;
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
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(waitingController.gamePlayerOk){
            this.delta += Time.deltaTime;
            if(this.delta > this.span){
                this.delta = 0;
                if(this.cnt == 10){
                    this.cnt = 0;
                    if(this.span > 0.5f){
                        this.span -= 0.1f;
                    }
                } 

                //代表者が送信
                if(matchingManagerScript.room == matchingManagerScript.userUuid){
                    ws4.Send("{\"request\": \"randomtrequest\"}");
                }

                this.cnt++;

            }
        }

        if(key){
            key = false;
            StoneSet();
        }
    }
}


