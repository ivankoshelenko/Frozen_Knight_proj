using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace resources
{
    public enum hungerStage { full, hungry, starving };
    public class HungerController : MonoBehaviour
    {
        [Header("Hunger main parameters")]
        public float playerHunger = 100.0f;
        [SerializeField]
        private float maxHunger = 100f;

        public float hungerSpeed = 0.5f;

        public float healthRecoveryHunger = 2f;
        private float currentHungerSpeed = 0.5f;


        [Header("Hunger UI")]
        [SerializeField] private Slider hungerUI = null;
        [SerializeField] private CanvasGroup sliderCanvasGroup = null;
        private Player character;
        private healthController health;

        public hungerStage stage;
        public float hungerDamage = 1f;
        // Start is called before the first frame update
        void Start()
        {
            character = GetComponent<Player>();
            health = GetComponent<healthController>();
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.TryGetComponent<Food>(out var food)) return;
            {
                EatFood(food.nutrition);
                Debug.Log("food");
            }
        }
        private void EatFood(float nutrition)
        {
            playerHunger += nutrition;
            if (playerHunger > maxHunger)
            {
                playerHunger = maxHunger;
            }
            if (playerHunger > 0 && playerHunger <= 30)
            {
                stage = hungerStage.hungry;
            }
            else
            {
                stage = hungerStage.full;
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (health.recovering && stage == hungerStage.full)
            {
                currentHungerSpeed = healthRecoveryHunger;
            }
            else
            {
                currentHungerSpeed = hungerSpeed;
            }
            if (playerHunger - currentHungerSpeed * Time.deltaTime > 0.001f)
            {
                playerHunger -= currentHungerSpeed * Time.deltaTime;
            }
            else
            {
                stage = hungerStage.starving;
            }
            if (playerHunger <= 30f && stage!= hungerStage.starving)
            {
                stage = hungerStage.hungry;
            }
            hungerUI.value = playerHunger;
        }
    }
}