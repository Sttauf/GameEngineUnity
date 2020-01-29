using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{

    private AsyncOperation async;

    public void BtnLoadScene()
    {
        //async == null = !async
        if (async == null)
        {
            Scene currScene = SceneManager.GetActiveScene();
            async = SceneManager.LoadSceneAsync(currScene.buildIndex + 1);
            async.allowSceneActivation = true;
        }
    }

    public void BtnLoadScene(int i)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(i);
            async.allowSceneActivation = true;
        }
    }

    public void BtnLoadScene(string i)
    {
        if (async == null)
        {
            async = SceneManager.LoadSceneAsync(i);
            async.allowSceneActivation = true;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
