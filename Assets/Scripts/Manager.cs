using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public enum Season { Summer, Autumn, Winter, Spring }
    public TreeManager treeManager;

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

    //public GameObject summerTreePrefab;
    //public GameObject springTreePrefab;
    //public GameObject winterTreePrefab; 
    //public GameObject autumnTreePrefab;

    //GameObject[] Trees;
    //public TreePool summerTreePool;
    //public TreePool autumnTreePool;
    //public TreePool winterTreePool;
    //public TreePool springTreePool;

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
    private MovePlayer player;
    
    TerrainCollider terrainCollider;
    
    public float timer = 0f;
    public float resetTimer = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
        terrainCollider = terrain.GetComponent<TerrainCollider>();
       //Trees = GameObject.FindGameObjectsWithTag("trees");

        treeManager.UpdateTrees(currentSeason); 

        sunParticleSystem.Stop();
        snowParticleSystem.Stop();
        thunderParticleSystem.Stop();
      
        player = FindObjectOfType<MovePlayer>();
        
        StartCoroutine(Weather());
        UpdateSeasonsVisuals();
        //UpdateDetails();
        //UpdateCharacterSliding();
       
    }

    public void ChangeSeason(Season newSeason)
    {
        currentSeason = newSeason;
        UpdateSeasonsVisuals();
        //UpdateDetails();
        //UpdateCharacterSliding();
        UpdateSkyBox();
        UpdateFrictionForSeason();
        treeManager.UpdateTrees(currentSeason);


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
            yield return new WaitForSeconds(200f);
            switch (currentSeason)
            {
                case Season.Summer:
                    ChangeSeason(Season.Autumn);
                    break;
                case Season.Autumn:
                    ChangeSeason(Season.Winter);
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

    //private void UpdateCharacterSliding()
    //{
    //    switch (currentSeason)
    //    {
    //        case Season.Winter:
    //            player.ApplySliding(player.winterFraction);
    //            break;
    //        default:
    //            player.ApplySliding(player.normalFraction);
    //            break;
    //    }
    //}
    //void ApplyTerrainLayer(TerrainLayer terrainLayer)
    //{
    //    {
    //        terrainLayer
    //    };
    //}
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