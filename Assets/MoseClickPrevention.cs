using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoseClickPrevention : MonoBehaviour
{
    public GameObject button;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            EventSystem.current.SetSelectedGameObject(button);
        }
    }
}