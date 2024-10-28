using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Manager : MonoBehaviour
{
    public enum Season { Summer, Autumn, Winter, Spring }
    public TreeManager treeManager;
    public GameObject game;   
    public ParticleSystem sunParticleSystem;
    public ParticleSystem thunderParticleSystem;
    public ParticleSystem snowParticleSystem;

    public TerrainLayer[] _summerTerrainLayer;
    public TerrainLayer[] _autumnTerrainLayer;
    public TerrainLayer[] _springTerrainLayer;
    public TerrainLayer[] _winterTerrainLayer;

    public Terrain terrain;

    public Material _summerSkyBox;
    public Material _autumnSkyBox;
    public Material _springSkyBox;
    public Material _winterSkyBox;

   

    public Season currentSeason;
    private int switchWeather;

    public Material summerMaterial;
    public Material autumnMaterial;
    public Material winterMaterial;
    public Material springMaterial;

    public PhysicMaterial summerPhyMat;
    public PhysicMaterial autumnPhyMat;
    public PhysicMaterial winterPhyMat;
    public PhysicMaterial springPhyMat;

    public GameObject environment;
  
    
    TerrainCollider terrainCollider;

    public int score = 0;

    public bool _gameOver = false;

    public GameObject winScreen;
    public GameObject gaameOverScreen;
    public GameObject GuidePanel;
    float time = 5f;
    public Text guideText;

    public Text scoreText;
    public GameObject _player;
    float timer;
    float seasonDuration = 420f;

    public static Manager Instance { get; set;  }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        _player = GameObject.FindGameObjectWithTag("Player");
        terrainCollider = terrain.GetComponent<TerrainCollider>();
        treeManager.UpdateTrees(currentSeason); 

        sunParticleSystem.Stop();
        snowParticleSystem.Stop();
        thunderParticleSystem.Stop();
        StartCoroutine(Weather());
        UpdateSeasonsVisuals();

        StartCoroutine(GuideEnable(time));

        

       
    }

    public void ChangeSeason(Season newSeason)
    {
   

            currentSeason = newSeason;
            UpdateSeasonsVisuals();
            UpdateSkyBox();
            UpdateFrictionForSeason();
            treeManager.UpdateTrees(currentSeason);
           StartCoroutine(SeasonTimer());
    }

    void UpdateSeasonsVisuals()
    {
        sunParticleSystem.Stop();
        snowParticleSystem.Stop();
        thunderParticleSystem.Stop();
        switch (currentSeason)
        {
            case Season.Summer:
                //sunParticleSystem.Play();
                terrain.terrainData.terrainLayers = _summerTerrainLayer;
                break;
            case Season.Autumn:
                terrain.terrainData.terrainLayers = _autumnTerrainLayer;
                break;
            case Season.Winter:
                terrain.terrainData.terrainLayers = _winterTerrainLayer;
                snowParticleSystem.Play();
                //thunderParticleSystem.Play();

                break;
            case Season.Spring:
                terrain.terrainData.terrainLayers = _springTerrainLayer;
                break;
        }
    }
    IEnumerator Weather()
    {

        while (true)
        {
            yield return new WaitForSeconds(seasonDuration);
            switch (currentSeason)
            {
                case Season.Summer:
                   // game.SetActive(true);
                    if (SummerGame.Instance.EndGame(true))
                    {
                      //  game.SetActive(false);
                        Debug.Log("autumn");
                        ChangeSeason(Season.Autumn);
                    }
                    else if (SummerGame.Instance.EndGame(false))
                    {
                        SummerGame.Instance.ResetGame();
                        Debug.Log("reset");
                    }
                    break;
                case Season.Autumn:
                    UpdateSeasonsVisuals();
                    break;
                case Season.Winter:
                    ChangeSeason(Season.Spring);
                    break;
                case Season.Spring:
                    ChangeSeason(Season.Summer);
                    break;
            }
        }
    }
    IEnumerator SeasonTimer()
    {
        timer = seasonDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;

            yield return null;
        }
        if(currentSeason == Season.Summer && SummerGame.Instance.mushroomsCollected < 3)
        {
            Lose();
        }
    }

    void UpdateSkyBox()
    {
        switch (currentSeason)
        {
            case Season.Summer:
                RenderSettings.skybox = _summerSkyBox;
                break;
            case Season.Autumn:
                RenderSettings.skybox = _autumnSkyBox;
                break;
            case Season.Winter:
                RenderSettings.skybox = _winterSkyBox;
                break;
            case Season.Spring:
                RenderSettings.skybox = _springSkyBox;
                break;

        }
        DynamicGI.UpdateEnvironment();
    }
   void UpdateFrictionForSeason()
    {
        switch (currentSeason)
        {
            case Season.Summer:
                terrainCollider.material = summerPhyMat;
                 break;
            case Season.Autumn:

                terrainCollider.material = autumnPhyMat;
                break;
            case Season.Winter:
                terrainCollider.material = winterPhyMat;
                break;
            case Season.Spring:
                terrainCollider.material = springPhyMat;
                break;

        }

    }
    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
        CheckWinCondition();

    }
    public void CheckWinCondition()
    {
        if(score>=3)
        {
            Win();
        }
    }
    public void Win()
    {
        _player.gameObject.SetActive(false);

        
        winScreen.SetActive(true);
        Debug.Log("You Win! ");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void Lose()
    {
        _player.gameObject.SetActive(false);
        gaameOverScreen.SetActive(true);
        Debug.Log("Game over ! you lose!");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }
     IEnumerator GuideEnable(float time)
    {
        GuidePanel.SetActive(true);
        yield return new WaitForSeconds(time-4);

        guideText.text = "You must find them all before the time up...";
        yield return new WaitForSeconds(time-2);
        guideText.text = "Good Luck..!";
        yield return new WaitForSeconds(time);

        GuidePanel.SetActive(false);

    }

    //void UpdateDetails()
    //{

    //    foreach (GameObject tree in Trees)
    //    {
    //        Vector3 treePosi = tree.transform.position;
    //        Quaternion treeRotation = tree.transform.rotation;
    //        Destroy(tree);


    //        switch (currentSeason)
    //        {
    //            case Season.Summer:
    //                summerTreePool.GetTree(treePosi,treeRotation);
    //                break;
    //            case Season.Autumn:
    //                autumnTreePool.GetTree(treePosi, treeRotation);
    //                break;
    //            case Season.Winter:
    //                winterTreePool.GetTree(treePosi, treeRotation);
    //                break;
    //            case Season.Spring:
    //                springTreePool.GetTree(treePosi, treeRotation);
    //                break;

    //        }
    //    }

    //    }
}