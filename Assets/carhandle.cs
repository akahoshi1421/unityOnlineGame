using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;
using UnityEngine.SceneManagement;

public class carhandle : MonoBehaviour
{
    // Start is called before the first frame update
    private WebSocket ws3;

    [System.Serializable]
    public class CarPos
    {
        public string userid;
        public int pos;
    }

    public string userid2;
    public double pos2;
    CarPos rs;
    bool is_enemyok = false;

    void Start()
    {
        ws3 = new WebSocket("ws://localhost:8000/ws/fightingroom/" + matchingManagerScript.room + "/");
        ws3.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket Open");
        };

        ws3.OnMessage += (sender, e) =>
        {
            //ここに開始処理を書く
            rs = JsonUtility.FromJson<CarPos>(e.Data);
            if(rs.userid != matchingManagerScript.userUuid && waitingController.gamePlayerOk){

                
                is_enemyok = true;

            }
            
        };

        ws3.OnError += (sender, e) =>
        {
            Debug.Log("WebSocket Error Message: " + e.Message);
        };

        ws3.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket Close");
        };

        ws3.Connect();

    }

    // Update is called once per frame
    void Update()
    {
        if(is_enemyok){
            is_enemyok = false;
            GameObject enemyCar = GameObject.Find("topview_car_enemy");
            switch(rs.pos){
                case 1:
                    enemyCar.transform.position = new Vector3(1.7f, -3.5f, 0);
                    break;
                case 2:
                    enemyCar.transform.position = new Vector3(5.7f, -3.5f, 0);
                    break;
                case 3:
                    enemyCar.transform.position = new Vector3(9.7f, -3.5f, 0);
                    break;
                case 4:
                    SceneManager.LoadScene("GameClearScene");
                    break;
            }
        }

        if(waitingController.gamePlayerOk){
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                Vector3 yourCarPos = transform.position;//-9.7,-5.7,-1.7
                float car_x = yourCarPos.x;

                CarPos c = new CarPos();
                c.userid = matchingManagerScript.userUuid;

                switch(car_x){
                    case -9.7f:
                        transform.position = new Vector3(-5.7f, -3.5f, 0);
                        c.pos = 2;
                        ServerSendCarPos(c);
                        break;
                    case -5.7f:
                        transform.position = new Vector3(-1.7f, -3.5f, 0);
                        c.pos = 3;
                        ServerSendCarPos(c);
                        break;
                }


            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                Vector3 yourCarPos = transform.position;//-9.7,-5.7,-1.7
                float car_x = yourCarPos.x;

                CarPos c = new CarPos();
                c.userid = matchingManagerScript.userUuid;

                switch(car_x){
                    case -1.7f:
                        transform.position = new Vector3(-5.7f, -3.5f, 0);
                        c.pos = 2;
                        ServerSendCarPos(c);
                        break;
                    case -5.7f:
                        transform.position = new Vector3(-9.7f, -3.5f, 0);
                        c.pos = 1;
                        ServerSendCarPos(c);
                        break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        CarPos c = new CarPos();
        c.userid = matchingManagerScript.userUuid;
        c.pos = 4;
        ServerSendCarPos(c);

        SceneManager.LoadScene("GameOverScene");
    }


    void ServerSendCarPos(CarPos sendC){
        string sendedJson_pos = JsonUtility.ToJson(sendC);
        ws3.Send(sendedJson_pos);
    }
}
