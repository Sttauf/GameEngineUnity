using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

    [SerializeField] private Image progressbar;
    [SerializeField] private Text txtpercent;
    [SerializeField] private bool waitForUserInput = false;
    private bool ready = false;
    [SerializeField] private float delay = 0f;
    [SerializeField] private int sceneToLoad = -1;

    private AsyncOperation async;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f; //Reset the timescale
        Input.ResetInputAxes(); //Reset the input
        System.GC.Collect(); //Clean the garbage
        Scene currScene = SceneManager.GetActiveScene(); //Checking the current scene

        if (sceneToLoad == -1)
        {
            async = SceneManager.LoadSceneAsync(currScene.buildIndex + 1); //Load the following scene
        }
        else
        {
            async = SceneManager.LoadSceneAsync(sceneToLoad); //Load the following scene 
        }

        async.allowSceneActivation = false; //Wait before moving to the loaded screen

        if (waitForUserInput == false)
        {
            //ready = true;
            Invoke("Activate", delay);
        }
    }
    void Activate()
    {
        ready = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (waitForUserInput && Input.anyKey)
        {
            if (async.progress >= 0.89f && SplashScreen.isFinished)
            {
                ready = true;
            }
        }
        if (progressbar)
        {
            progressbar.fillAmount = async.progress + 0.1f;
        }
        if (txtpercent)
        {
            txtpercent.text = ((async.progress + 0.1f) * 100).ToString("f2") + " %";
        }

        if (async.progress >= 0.89f && SplashScreen.isFinished && ready)
        { //Check if everything is done
            async.allowSceneActivation = true; //Move to the next scene.
        }

    }
}
