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

    public static string userUuid;
    public static string room;

    private bool key = false;

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
            PlayerData d = JsonUtility.FromJson<PlayerData>(e.Data);
            //Debug.Log(d.playerA, d.playerB);
            Debug.Log(userUuid);
            if(d.playerA == userUuid || d.playerB == userUuid){
                room = d.playerA;
                Debug.Log(room);
                key = true;
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

        userUuid = guidValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(key) SceneManager.LoadScene("FightingScene");
        
    }
}
