using UnityEngine;
using System.Collections;
using WebSocketSharp;
using WebSocketSharp.Net;

public class test : MonoBehaviour {

   private WebSocket ws;

   void Start()
   {
       ws = new WebSocket("ws://localhost:8000/ws/matchingroom/1/");
       ws.OnOpen += (sender, e) =>
       {
           Debug.Log("WebSocket Open");
       };

       ws.OnMessage += (sender, e) =>
       {
           Debug.Log("Data: " + e.Data);
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

   }

   void Update()
   {

       if (Input.GetKeyUp("s"))
       {
            Debug.Log("aaaa");
           ws.Send("Test Message");
       }

   }

   void OnDestroy()
   {
       ws.Close();
       ws = null;
   }
}