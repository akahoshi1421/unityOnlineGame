using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;



public class waitingController : MonoBehaviour
{
    private WebSocket ws;
    public static waitingController instance2;
    public bool gamePlyerOk = false;//falseがだめ,trueがok

    public int[] gameRandomRule;

    [System.Serializable]
    public class RoomDatas
    {
        public string uuid;
        public string roomUuid;
    }

    [System.Serializable]
    public class RoomSettings
    {
        public string go;
        public int[] random;
    }

    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://localhost:8000/ws/waitingroom/" + matchingManagerScript.instance.room + "/");
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket Open");
        };

        ws.OnMessage += (sender, e) =>
        {
            //ここに開始処理を書く
            RoomSettings rs = JsonUtility.FromJson<RoomSettings>(e.Data);
            if(rs.go == "OK"){
                this.gamePlyerOk = true;
                this.gameRandomRule = rs.random;
            }

        };

        ws.OnError += (sender, e) =>
        {
            Debug.Log("WebSocket Error Message: " + e.Message);
        };

        ws.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket Close");
        };

        ws.Connect();

        RoomDatas d = new RoomDatas();
        d.uuid = matchingManagerScript.instance.userUuid;
        d.roomUuid = matchingManagerScript.instance.room;

        string sendedJson_wait = JsonUtility.ToJson(d);

        ws.Send(sendedJson_wait);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
