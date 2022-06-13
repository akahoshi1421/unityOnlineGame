using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Net;
using UnityEngine.SceneManagement;

public class matchingManagerScript : MonoBehaviour
{
    private WebSocket ws;
    public static matchingManagerScript instance;
    public string userUuid;
    public string room;

    [System.Serializable]
    public class PlayerData
    {
        public string playerA;
        public string playerB;
    }

    // Start is called before the first frame update
    void Start()
    {
        ws = new WebSocket("ws://localhost:8000/ws/matchingroom/1/");
        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket Open");
        };

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log(e.Data);
            PlayerData d = JsonUtility.FromJson<PlayerData>(e.Data);
            Debug.Log(d.playerA);
            Debug.Log(d.playerB);
            Debug.Log(this.userUuid);
            if(d.playerA == this.userUuid || d.playerB == this.userUuid){
                this.room = d.playerA;
                SceneManager.LoadScene("FightingScene");
            }
            else{
                Debug.Log("まだ待機");
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

        Guid guidValue = Guid.NewGuid();
        string sendedJson = "{\"uuid\": \"" + guidValue.ToString() + "\"}";
        ws.Send(sendedJson);

        this.userUuid = guidValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
