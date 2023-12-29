using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using monsters;

public class AttackZone : MonoBehaviour
{
    public float damage= 20f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent<Tree>(out var tree)) 
        {
            tree.GetChopped();
            Debug.Log("Chop");
        }
        if (other.gameObject.TryGetComponent<MonsterScript>(out var monster))
        {
            monster.GetDamage(damage);
            Debug.Log("monsterHit");
        }
    }
}
