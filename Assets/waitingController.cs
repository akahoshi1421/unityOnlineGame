using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;
using WebSocketSharp.Net;
using UnityEngine.SceneManagement;


public class waitingController : MonoBehaviour
{
    private WebSocket ws2;
    public static bool gamePlayerOk = false;//falseがだめ,trueがok

    public static int[] gameRandomRule;

    bool textdelte = false;

    float counter = 0.0f;

    bool gokey = false;

    [System.Serializable]
    public class RoomDatas
    {
        public string uuid;
        public string roomUuid;
    }

    [System.Serializable]
    public class RoomSettings
    {
        public string res;
        public int[] random;//これは使わない
    }

    // Start is called before the first frame update
    void Start()
    {
        ws2 = new WebSocket(matchingManagerScript.domain + "ws/waitingroom/" + matchingManagerScript.room + "/");
        ws2.OnOpen += (sender, e) =>
        {
            //Debug.Log("WebSocket Open");
        };

        ws2.OnMessage += (sender, e) =>
        {
            //ここに開始処理を書く
            RoomSettings rs = JsonUtility.FromJson<RoomSettings>(e.Data);
            if(rs.res == "OK"){
                gamePlayerOk = true;
                gameRandomRule = rs.random;
                textdelte = true;
            }

        };

        ws2.OnError += (sender, e) =>
        {
            //Debug.Log("WebSocket Error Message: " + e.Message);
        };

        ws2.OnClose += (sender, e) =>
        {
            //Debug.Log("WebSocket Close");
        };

        ws2.Connect();

        RoomDatas d = new RoomDatas();
        d.uuid = matchingManagerScript.userUuid;
        d.roomUuid = matchingManagerScript.room;

        string sendedJson_wait = JsonUtility.ToJson(d);

        ws2.Send(sendedJson_wait);
    }

    void TextDelete()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if(textdelte){
            textdelte = false;
            gokey = true;
            GameObject t = GameObject.Find("matching");
            GameObject t2 = GameObject.Find("hosoku");
            t.GetComponent<Text> ().text = "";
            t2.GetComponent<Text> ().text = "";
        }

        if(counter > 10 && !gokey){
            SceneManager.LoadScene("SampleScene");
        }

    }
}
