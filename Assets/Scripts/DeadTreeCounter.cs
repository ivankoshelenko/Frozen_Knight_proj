using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeadTreeCounter : MonoBehaviour
{
    Tree[] trees;
    static Tree[] cutTrees;
    // Start is called before the first frame update
    void Start()
    {
        trees = GetComponentsInChildren<Tree>();
        //for(int i = 0;i<trees.Length;i++)
        //    trees[i].manager = Object.FindObjectOfType<ResourceManager>();
        //DontDestroyOnLoad(gameObject);
        //for (int i = 0;i< trees.Length; i++)
        //    Debug.Log(trees[i]);
    }
    private void OnEnable()
    {
        //for (int i = 0; i < cutTrees.Length; i++)
        //{
        //    Destroy(cutTrees[i].gameObject);
        //    Debug.Log("destroys");
        //}
    }
    private void OnDisable()
    {
        //cutTrees = trees;
        //for (int i = 0; i < trees.Length; i++)
        //{
        //    if (trees[i].cutDown)
        //    {
        //        cutTrees =  cutTrees.Append(trees[i].gameObject).toArray();
        //        Debug.Log("adds to array");
        //    }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
