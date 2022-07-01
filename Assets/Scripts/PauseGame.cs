using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public Button Pause;
    public Button Countine;
    public GameObject uiPasue;
    private void Start()
    {
       
    }

    public void Pausegame()
    {
        uiPasue.SetActive(true);
        Time.timeScale = 0;
    }
    public void Playgame()
    {
        uiPasue.SetActive(false);
        Time.timeScale = 1;
    }
}
