using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int hitPoints = 5;
    public GameObject droppedRock;
    public ResourceManager manager;
    public static bool cutDown = false;

    private void Start()
    {
        //manager = Object.FindObjectOfType<ResourceManager>().GetComponent<ResourceManager>();
    }
    private void OnEnable()
    {
        manager = Object.FindObjectOfType<ResourceManager>().GetComponent<ResourceManager>();
    }

    private void CutDown()
    {
        manager = Object.FindObjectOfType<ResourceManager>().GetComponent<ResourceManager>();
        droppedRock.GetComponent<Resource>().rock = Random.RandomRange(10, 15);
        Transform pos = transform;
        //pos.position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
        gameObject.SetActive(false);
        manager.SpawnResources(droppedRock, pos);
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
