using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tree : MonoBehaviour
{
    public int hitPoints = 1;
    public GameObject droppedWood;
    public ResourceManager manager;
    DeadTreeCounter counter;
    public bool cutDown = false;

    private void Start()
    {
        manager = Object.FindObjectOfType<ResourceManager>().GetComponent<ResourceManager>();
    }
    private void OnEnable()
    {
        manager = Object.FindObjectOfType<ResourceManager>().GetComponent<ResourceManager>();
        counter = GetComponentInParent<DeadTreeCounter>();
    }
 
    private void CutDown()
    {
        manager = Object.FindObjectOfType<ResourceManager>().GetComponent<ResourceManager>();
        droppedWood.GetComponent<Resource>().wood = Random.RandomRange(10, 25);
        Transform pos = transform;
        gameObject.SetActive(false);
        manager.SpawnResources(droppedWood, pos);  
    }
    public void GetChopped()
    {
        hitPoints -= 1;
        Debug.Log(hitPoints);
        if (hitPoints <= 0)
            CutDown();
    }
    private void Reset1()
    {
        cutDown = false;
    }
}
