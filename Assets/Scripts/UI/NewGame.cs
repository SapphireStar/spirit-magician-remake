using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    public void Onclick()
    {
        Constants.SceneToLoad = "DEMO";
        SceneManager.LoadScene("Loading");
    }
}
