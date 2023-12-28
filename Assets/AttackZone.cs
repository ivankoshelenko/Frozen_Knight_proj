using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<Tree>(out var tree)) return;
        {
            tree.GetChopped();
            Debug.Log("Chop");
        }
    }
}
