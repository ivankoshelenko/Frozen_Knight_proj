using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public float wood { get; set; }
    public float rock { get; set; }
    public float food { get; set; }

    public GameObject player;

    public void AddResource(Resource resource)
    {
        wood += resource.wood;
        rock += resource.rock;
        food += resource.food;
    }
    public void SpawnResources(GameObject body,Transform position)
    {
        GameObject instance = Instantiate(body, position);
        instance.transform.parent = transform;
    }
}
