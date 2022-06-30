using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dotgenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public Text dot;
    int i = 0;
    string dottext = "";
    int dotcnt = 0;
    void Start()
    {
    
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        i++;
        if(i > 20){
            if(dotcnt == 4){
                dotcnt = 0;
                dottext = "";
            }

            i = 0;
            dot.text = "マッチング中" + dottext;
            dottext += ".";
            dotcnt++;
        }
    }
}
