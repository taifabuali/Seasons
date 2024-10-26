using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePool : MonoBehaviour
{
    public GameObject treePrefab;
    public int poolSize = 10;
    List<GameObject> pool;

    // Start is called before the first frame update
    void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(treePrefab);
            //obj.SetActive(false);
            pool.Add(obj);

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject GetTree(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject tree in pool)
        {
            if (!tree.activeInHierarchy)
            {
                tree.transform.position = position;
                tree.transform.rotation = rotation;
                tree.SetActive(true);
                return tree;
            }
        }
        GameObject newTree =Instantiate(treePrefab,position,rotation);
        pool.Add(newTree);
        return newTree;

    }
    public void ReturnTree(GameObject tree)
    {
        tree.SetActive(false);
    }
}
