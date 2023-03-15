using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnsController : MonoBehaviour
{
    public GameObject homeBtn;
    public GameObject resetBtn;

    public void loadSceneMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void resetGemeLevel()
    {
        SceneManager.LoadScene("Scene0");
    }
}
