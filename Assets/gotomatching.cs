using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotomatching : MonoBehaviour
{
    AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        clip = gameObject.GetComponent<AudioSource> ().clip;
        waitingController.gamePlayerOk = false;
    }

    public void StartBtnClick()
    {
        GetComponent<AudioSource> ().PlayOneShot(clip);
        Invoke("LoadMethod", 0.3f);
    }

    void LoadMethod()
    {
        SceneManager.LoadScene("MatchingScene");
    }    
}
