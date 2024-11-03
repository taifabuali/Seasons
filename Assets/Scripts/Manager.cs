using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Manager : MonoBehaviour, IGameManager
{
    public enum Season { Summer, Autumn, Winter, Spring }
    public Season currentSeason;
    public GameObject environment;

    public int score = 0;

    public bool _gameOver = false;
    public GameObject _player;
    float timer;
    float seasonDuration = 420f;


    [Header("Season Game Objects")]

    public GameObject summerGameObject;
    public GameObject autumnGameObject;
    public GameObject winterGameObject;
    public GameObject springGameObject;

    public TreeManager treeManager;

    [Header("ParticleSystem")]
    public ParticleSystem sunParticleSystem;
    public ParticleSystem thunderParticleSystem;
    public ParticleSystem snowParticleSystem;

    [Header("TerrainLayer")]
    public TerrainLayer[] _summerTerrainLayer;
    public TerrainLayer[] _autumnTerrainLayer;
    public TerrainLayer[] _springTerrainLayer;
    public TerrainLayer[] _winterTerrainLayer;

    public Terrain terrain;

    [Header("Sky Box Material")]
    public Material _summerSkyBox;
    public Material _autumnSkyBox;
    public Material _springSkyBox;
    public Material _winterSkyBox;


    [Header("Material")]
    public Material summerMaterial;
    public Material autumnMaterial;
    public Material winterMaterial;
    public Material springMaterial;

    [Header("PhysicMaterial")]
    public PhysicMaterial seasonalPhysicMaterial;

   
    [Header("UI")]
    public GameObject winScreen;
    public GameObject gaameOverScreen;
    public Text scoreText;

    

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

        _player = Player.Instance.gameObject;
        treeManager.UpdateTrees(currentSeason);

        UpdateFrictionForSeason();

        sunParticleSystem.Stop();
        snowParticleSystem.Stop();
        thunderParticleSystem.Stop();

        StartCoroutine(Weather());
        UpdateSeasonsVisuals();
       
    }

    public void ChangeSeason(Season newSeason)
    {
        currentSeason = newSeason;
        UpdateSeasonsVisuals();
        UpdateSkyBox();
        UpdateFrictionForSeason();
        treeManager.UpdateTrees(currentSeason);
        StartCoroutine(SeasonTimer());
        PlayerSeasonsMovement playerMovement = _player.GetComponent<PlayerSeasonsMovement>();
        if (playerMovement != null)
        {
            playerMovement.UpdateMovementSpeed();
        }
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
                   
                    break;

                case Season.Autumn:

                    summerGameObject.SetActive(false);
                    break;

                case Season.Winter:
                    autumnGameObject.SetActive(false);

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
                seasonalPhysicMaterial.dynamicFriction = 0.6f; 
                seasonalPhysicMaterial.staticFriction = 0.6f;
                break;
            case Season.Autumn:
                seasonalPhysicMaterial.dynamicFriction = 0.4f; 
                seasonalPhysicMaterial.staticFriction = 0.4f;
                break;
            case Season.Winter:
                seasonalPhysicMaterial.dynamicFriction = 0.1f; //(slippery)
                seasonalPhysicMaterial.staticFriction = 0.1f;
                break;
            case Season.Spring:
                seasonalPhysicMaterial.dynamicFriction = 0.3f; 
                seasonalPhysicMaterial.staticFriction = 0.3f;
                break;
        }
        Collider playerCollider = _player.GetComponent<Collider>();
        if (playerCollider != null)
        {
            playerCollider.material = seasonalPhysicMaterial;
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
        if(score>=4)
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
    public void OnGameEnded(bool success, Season currentSason, Season nextSeason, GameObject nextGame)
    {

        if (success)
        {
            ChangeSeason(nextSeason);
            if (nextGame != null)
            {
                nextGame.SetActive(true);
                
            }
            Debug.Log($"Transitioning to{nextSeason}");
        }else
        {
            ResetCurrentGame(currentSason);
            Debug.Log($"Resetig {nextSeason} Game");


        }
    }
    void ResetCurrentGame(Season season)
    {
        switch(season)
        {
            case Season.Summer:
                SummerGame.Instance.ResetGame();
                break;
            case Season.Autumn:
                AutumnGame.Instance.ResetGame();
                break;
            case Season.Winter:
                WinterGame.Instance.ResetGame();
                break;

        }

    }
   

}