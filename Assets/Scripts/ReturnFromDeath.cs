using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using resources;

public class ReturnFromDeath : MonoBehaviour
{
    public void MainMenu()
    {
        ResourceManager.Reset1();
        Dialogue.Reset1();
        healthController.Reset1();
        Timer.Reset1();
        Destroy(FindObjectOfType<DeadTreeCounter>().gameObject);
        SceneManager.LoadSceneAsync(0);
    }
}
