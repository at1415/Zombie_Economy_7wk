using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PLAY : MonoBehaviour
{

    public void LoadByIndex(int SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

}
