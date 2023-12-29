using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int hitPoints = 1;
    public GameObject droppedWood;
    public ResourceManager manager;
    DeadTreeCounter counter;
    public bool cutDown = false;

    private void OnEnable()
    {
        counter = GetComponentInParent<DeadTreeCounter>();
    }
    private void CutDown()
    {
        droppedWood.GetComponent<Resource>().wood = Random.RandomRange(10, 25);
        Transform pos = transform;
        manager.SpawnResources(droppedWood, pos);
        gameObject.SetActive(false);
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
