using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using static Manager;

public class TreeManager : MonoBehaviour
{
    public Terrain terrain;  // Reference to the Terrain
    public GameObject summerTreePrefab;
    public GameObject winterTreePrefab;
    public GameObject autumnTreePrefab;
    public GameObject springTreePrefab;

    private TerrainData terrainData;

    void Start()
    {
        terrainData = terrain.terrainData;
    }

    // Call this method to update trees based on the season
    public void UpdateTrees(Season currentSeason)
    {
        TreeInstance[] trees = terrainData.treeInstances;

        for (int i = 0; i < trees.Length; i++)
        {
            switch (currentSeason)
            {
                case Season.Summer:
                    trees[i].prototypeIndex = GetTreePrototypeIndex(summerTreePrefab);
                    break;
                case Season.Autumn:
                    trees[i].prototypeIndex = GetTreePrototypeIndex(autumnTreePrefab);
                    break;
                case Season.Winter:
                    trees[i].prototypeIndex = GetTreePrototypeIndex(winterTreePrefab);
                    break;
                case Season.Spring:
                    trees[i].prototypeIndex = GetTreePrototypeIndex(springTreePrefab);
                    break;
            }
        }

        terrainData.treeInstances = trees;
        //ReduceTreeCount(0.5f);
    }

    private int GetTreePrototypeIndex(GameObject treePrefab)
    {
        for (int i = 0; i < terrainData.treePrototypes.Length; i++)
        {
            if (terrainData.treePrototypes[i].prefab == treePrefab)
            {
                return i;
            }
        }
        Debug.LogError("Tree prefb not found");

        return -1;
    }
    void ReduceTreeCount(float percentage)
    {
        TreeInstance[] trees = terrainData.treeInstances;
        int treeToKeep = Mathf.FloorToInt(trees.Length * percentage);

        List<TreeInstance> reducedTrees = new List<TreeInstance>();
        System.Random random = new System.Random(); 
        for(int i=0; i<treeToKeep; i++)
        {
            int randomIndex = random.Next(trees.Length);
            reducedTrees.Add(trees[randomIndex]);
        }
        terrainData.treeInstances = reducedTrees.ToArray();
    }
}