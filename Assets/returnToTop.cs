using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnToTop : MonoBehaviour
{
    AudioClip clip;

    void Start()
    {
        clip = gameObject.GetComponent<AudioSource> ().clip;
    }

    public void ReturnTop()
    {
        GetComponent<AudioSource> ().PlayOneShot(clip);
        Invoke("LoadMethod", 0.3f);
        
    }

    void LoadMethod()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
