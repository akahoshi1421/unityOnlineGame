using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returnToTop : MonoBehaviour
{
    public void ReturnTop()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
