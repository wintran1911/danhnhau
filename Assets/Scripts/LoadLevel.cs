using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{

    public void aLoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        // SoundManager.instance.PlaySound(SoundManager.instance.but, 1f);

    }



    public void aRestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);


    }
    public void aHomepplay()
    {
        SceneManager.LoadScene(0);


    }
    /*public void aHowToPlay()
    {
        SceneManager.LoadScene("howtoplay");
    }*/
    public void aQuitgame()
    {
        Application.Quit();

    }
    public void aBackgame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }
    /*public void aStartgame()
    {
        SceneManager.LoadScene("Start");

    }*/
       public void aMainMenu()
        {
        SceneManager.LoadScene("MainMenu");
        }
       /* public void aCommingSoon()
        {
            SceneManager.LoadScene("CommingSoon");

        }*/
        public void lv1()
        {
            SceneManager.LoadScene(2);

        }
        public void lv2()
        {
            SceneManager.LoadScene(3);

        }
        public void lv3()
        {
            SceneManager.LoadScene(4);

        }
        public void lv4()
        {
            SceneManager.LoadScene(5);

        }
        public void lv5()
        {
            SceneManager.LoadScene(6);

        }
        public void lv6()
        {
            SceneManager.LoadScene(7);

        }
        public void lv7()
        {
            SceneManager.LoadScene(8);

        }
        public void lv8()
        {
            SceneManager.LoadScene(9);

        }
        public void lv9()
        {
            SceneManager.LoadScene(10);

        }
        public void lv10()
        {
            SceneManager.LoadScene(11);

        }
        public void lv11()
        {
            SceneManager.LoadScene(12);

        }
        public void lv12()
        {
            SceneManager.LoadScene(13);

        }
        public void lv13()
        {
            SceneManager.LoadScene(14);

        }
        public void lv14()
        {
            SceneManager.LoadScene(15);

        }
        public void lv15()
        {
            SceneManager.LoadScene(16);

        }
        public void lv16()
        {
            SceneManager.LoadScene(17);


        }
        public void lv17()
        {
            SceneManager.LoadScene(18);

        }
        public void lv18()
        {
            SceneManager.LoadScene(19);

        }
        public void lv19()
        {
            SceneManager.LoadScene(20);

        }
        public void lv20()
        {
            SceneManager.LoadScene(21);

        }
    }

