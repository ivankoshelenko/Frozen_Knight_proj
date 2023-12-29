using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public bool isSecure = false;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            isSecure = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            isSecure = false;
        }
    }
}
