using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public int hitPoints = 1;
    public GameObject droppedWood;
    public ResourceManager manager;

    private void OnEnable()
    {

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
}
