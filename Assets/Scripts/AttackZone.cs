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
            FindObjectOfType<AudioManager>().Play("WoodCut");
            Debug.Log("Chop");
        }
        if (other.gameObject.TryGetComponent<MonsterScript>(out var monster))
        {
            FindObjectOfType<AudioManager>().Play("MushroomDamage");
            monster.GetDamage(damage);
            Debug.Log("monsterHit");
        }
        if (other.gameObject.TryGetComponent<Rock>(out var rock))
        {
            rock.GetChopped();
            FindObjectOfType<AudioManager>().Play("PlayerAttack");
            Debug.Log("Chop");
        }
    }
}
