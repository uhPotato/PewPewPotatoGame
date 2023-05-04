using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{
    public static Level instance;

  

    public uint numDestructables = 0;
    bool startNextLevel = false;
    float nextLevelTimer = 3;

    string[] levels = { "Level1", "Level2", "Level3","Level4","Level5","Level6","Level7","Level8","Level9","Level10","Level11","Level12"};
    int currentLevel = 1;

    public static int score = 0;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (instance == null)
        {
        
                    instance = this;
                    DontDestroyOnLoad(gameObject);
                    scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
                
       
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (startNextLevel)
        {
            if (nextLevelTimer <= 0)
            {
                currentLevel++;
                if (currentLevel <= levels.Length)
                {
                    string sceneName = levels[currentLevel - 1];
                    SceneManager.LoadSceneAsync(sceneName);
                }
                else
                {
                    Destroy(gameObject);
                    string sceneName = "GameOver";
                    SceneManager.LoadScene(sceneName);
                    
                }
                nextLevelTimer = 1;
                startNextLevel = false;
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }
            Scene currentScene = SceneManager.GetActiveScene();
            string gameOverScene = currentScene.name;
            

                if(gameOverScene == "GameOver"){
                    Destroy(gameObject);
                }
    }


    public void GameOver() {
      

        foreach(Bullet b in GameObject.FindObjectsOfType<Bullet>())
        {
            Destroy(b.gameObject);
        }
        numDestructables = 0;
        Destroy(gameObject);

        string sceneName = "GameOver";

       
         
        SceneManager.LoadScene(sceneName);
        // PlayFabManager.Instance.SendLeaderboard(score);


    }

    public void ResetLevel()
    {   
        
        foreach(Bullet b in GameObject.FindObjectsOfType<Bullet>())
        {
            Destroy(b.gameObject);
        }
        numDestructables = 0;
        score = 0;
        AddScore(score);
        string sceneName = levels[currentLevel - 1];
        SceneManager.LoadScene(sceneName);
    }


    public void SubScore(int amountToSub) {
        score -= amountToSub;
        scoreText.text = score.ToString();
    }

    public void AddScore(int amountToAdd)
    {
        score += amountToAdd;
        scoreText.text = score.ToString();
    }

    public void AddDestructable()
    {
        numDestructables++;
    }

    public void RemoveDestructable()
    {
        numDestructables--;

        if (numDestructables == 0)
        {
            startNextLevel = true;
        }

    }

}
