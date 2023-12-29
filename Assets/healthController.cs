using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using resources;

namespace resources
{
    public class healthController : MonoBehaviour
    {
        [Header("Health main parameters")]
        public float playerHealth = 100.0f;
        [SerializeField]
        private float maxHealth = 100f;


        [Header("Health Regen")]
        [Range(0, 50)] [SerializeField] private float HealthRegen = 0.5f;
        [SerializeField] private float RecoveryTime = 5;
        [Header("Health UI")]
        [SerializeField] private Slider healthUI = null;
        [SerializeField] private CanvasGroup sliderCanvasGroup = null;
        private Player character;
        private float timeUntillRecovery;
        public bool damaged = false;
        private HungerController hunger;
        private ColdController cold;
        public bool recovering = false;
        void Start()
        {
            hunger = GetComponent<HungerController>();
            character = GetComponent<Player>();
            cold = GetComponent<ColdController>();
            timeUntillRecovery = RecoveryTime;
            UpdateHealth(1);
        }


        void Update()
        {
            if (damaged == true && timeUntillRecovery > 0.01)
            {
                timeUntillRecovery -= Time.deltaTime;
            }
            else
            {
                damaged = false;
                timeUntillRecovery = RecoveryTime;
                recovering = false;
            }
            if (!damaged && playerHealth <= maxHealth - 0.001f && hunger.stage == hungerStage.full && cold.freezeState == freezeStates.warm)
            {
                recovering = true;
                playerHealth += HealthRegen * Time.deltaTime;
                UpdateHealth(1);
            }
            if (hunger.stage == hungerStage.starving)
            {
                DamageOverTime(hunger.hungerDamage);
            }
            if (cold.freezeState == freezeStates.freezing)
            {
                DamageOverTime(cold.freezeDamage);
            }
        }

        void DamageOverTime(float damage)
        {
            recovering = false;
            playerHealth -= damage * Time.deltaTime;
            UpdateHealth(1);
            if (playerHealth <= 0)
            {
                Die();
                playerHealth = -10f;
            }
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent<Enemy>(out var bullet)) return;
            {
                GetDamage(20f);
                Debug.Log("hit");
            }
        }

        public void GetDamage(float damage)
        {
            damaged = true;
            playerHealth -= damage;
            timeUntillRecovery = RecoveryTime;
            UpdateHealth(1);
            if (playerHealth <= 0)
            {
                Die();
                playerHealth = 100f;
            }
            healthUI.value = playerHealth;
        }

        void Die()
        {
            Debug.Log("Death");

            //SceneManager.LoadScene(0);
        }

        void UpdateHealth(int value)
        {
            healthUI.value = playerHealth;
            //if (value == 0)
            //{
            //    sliderCanvasGroup.alpha = 0;
            //}
            //else
            //{
            //    sliderCanvasGroup.alpha = 1;
            //}
        }
    }
}