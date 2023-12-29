using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadTreeCounter : MonoBehaviour
{
    Tree[] trees;
    static GameObject[] cutTrees;
    // Start is called before the first frame update
    void Start()
    {
        trees = GetComponentsInChildren<Tree>();
        //for (int i = 0;i< trees.Length; i++)
        //    Debug.Log(trees[i]);
    }
    private void OnEnable()
    {
        for (int i = 0; i < cutTrees.Length; i++)
        {
            Destroy(cutTrees[i].gameObject);
            Debug.Log("destroys");
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < trees.Length; i++)
        {
            if (trees[i].cutDown)
            {
               cutTrees[i] =  trees[i].gameObject;
                Debug.Log("adds to array");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
