using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartButton(){
        SceneManager.LoadScene("Gameplay");
    }

    public void ExitButton(){
        Application.Quit();
    }

    void Awake()
    {
        Cursor.visible = true;
    }
}