using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public string objectId;
    private void Awake()
    {
        objectId = name + transform.position.ToString() + transform.eulerAngles.ToString();
    }
    void Start()
    {
        for (int i =0; i < Object.FindObjectsOfType<DontDestroy>().Length; i++)
        {
            if (Object.FindObjectsOfType<DontDestroy>()[i] != this)

                {
                if (Object.FindObjectsOfType<DontDestroy>()[i].objectId == objectId)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
