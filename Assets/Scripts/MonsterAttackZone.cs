using resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackZone : MonoBehaviour
{
    public float damage = 20f;
    public bool gotDamaged = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.TryGetComponent<healthController>(out var player)) return;
        {
            player.GetDamage(damage);
            gotDamaged = true;
            gameObject.SetActive(false);
        }
    }
}
