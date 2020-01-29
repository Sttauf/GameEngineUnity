using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static bool paused = false;
    

    //Variables for points and score
    [SerializeField] private int lifes = 3;
    private int points;
    private int bestScore = 0;
    [SerializeField] private Text scoreTx;

    //Variables for objects
    [SerializeField] private MoveBall ball;
    public GameObject btnExit;
    public GameObject btnRetry;
    public GameObject btnStartOver;
    public GameObject endUI;

    //Variables for levels 
    [SerializeField] private GameObject[] levels;
    private int blocks = 0;
    private int currLvl = 0;
    private GameObject currBoard;
    public string sceneToReload = "SampleScene";
    private AsyncOperation async;

    //Buttons 
    public void StartOver()
    {
        SceneManager.LoadScene(sceneToReload);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Retry()
    {
        points = 0;
        scoreTx.text = "SCORE: " + points.ToString("D8");
        lifes = 3;

        LoadLvl();
        ball.gameObject.SetActive(true);
        ball.Init();

        btnExit.SetActive(false);
        btnRetry.SetActive(false);
        btnStartOver.SetActive(false);

    }

    //Actions
    public void Death()
    {
        lifes--;
        if (lifes > 0)
        {
            ball.Init();
        }
        else
        {
            scoreTx.text = "GAME OVER\nSCORE: " + points.ToString("D8");
            ball.gameObject.SetActive(false);
            if (points > bestScore)
            {
                PlayerPrefs.SetInt("Score", points);
                PlayerPrefs.Save();
            }
            btnExit.SetActive(true);
            btnRetry.SetActive(true);
            btnStartOver.SetActive(true);
        }

    }
    void LoadLvl()
    {
        if (currBoard)
        {
            Destroy(currBoard);
        }
        blocks = 0;
        currBoard = Instantiate(levels[currLvl]); 
       
    }
public void AddPoints()
    {
        points += 1000;
        string preTxt = "";
        if (points > bestScore)
        {
            preTxt = "BEST ";
        }
        scoreTx.text = preTxt + "SCORE: " + points.ToString("D8");
        blocks--;
        if (blocks <= 0)
        {
            if (currLvl < levels.Length - 1)
            {
                currLvl++;
            }
            else
            {
                Scene currScene = SceneManager.GetActiveScene();
                if(currScene.buildIndex + 1 < 9) {                     
                    async = SceneManager.LoadSceneAsync(currScene.buildIndex + 1);
                    async.allowSceneActivation = true;
                }
                else {                
                   endUI.SetActive(true); 
                }
            }
            ball.Init();
            LoadLvl();
            //ball.IncreaseDiff();
        }
        
    }
    public void AddBlock()
    {
        blocks++;
    }
    void Start()
    {
        btnExit.SetActive(false);
        btnRetry.SetActive(false);
        btnStartOver.SetActive(false);
        bestScore = PlayerPrefs.GetInt("Score", 0);
        LoadLvl();
        scoreTx.text = "SCORE: " + points.ToString("D8");
    }
    void Update()
    {

    }

}
