using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using resources;

public class ResourceManager : MonoBehaviour
{
    public static float wood { get; set; }
    public static float rock { get; set; }
    public static float food { get; set; }
    [SerializeField] private TextMeshProUGUI _woodDisplay;
    [SerializeField] private TextMeshProUGUI _rockDisplay;
    [SerializeField] private TextMeshProUGUI _foodDisplay;

    public GameObject player;
    private HungerController hunger;
    public GameObject campFirePrefab;
    private void Start()
    {
        hunger = player.GetComponent<HungerController>();
    }
    private void OnEnable()
    {
        RefreshIcons();
    }
    public void AddResource(Resource resource)
    {
        wood += resource.wood;
        rock += resource.rock;
        food += resource.food;
        RefreshIcons();
    }

    public void ConsumeFood(float nutrition)
    {
        if (hunger.playerHunger + nutrition >= hunger.maxHunger)
            nutrition = hunger.maxHunger - hunger.playerHunger;
        if (nutrition <= food)
        {
            food -= nutrition;
            hunger.EatFood(nutrition);
            RefreshIcons();
        }
    }
    public void SpawnResources(GameObject body,Transform position)
    {
        GameObject instance = Instantiate(body, position);
        instance.transform.parent = transform;
        instance.transform.position = position.position;
    }


    public bool SpawnCampFire(Transform pos)
    {
        if (wood >= 25)
        {
            wood -= 25;
            GameObject instance = Instantiate(campFirePrefab, pos);
            instance.transform.parent = transform;
            RefreshIcons();
            return true;
        }
        return false;
    }
    public void RefreshIcons()
    {
        _woodDisplay.text = ((int)wood).ToString();
        _rockDisplay.text = (((int)rock).ToString());
        _foodDisplay.text = (((int)food).ToString());
    }
}
