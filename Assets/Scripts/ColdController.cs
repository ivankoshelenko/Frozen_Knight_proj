using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace resources
{
    public enum freezeStates{warm, cold, freezing}
    public class ColdController : MonoBehaviour
    {
        [Header("Hunger UI")]
        [SerializeField] private Slider freezeUI = null;
        [SerializeField] private CanvasGroup sliderCanvasGroup = null;
        private Player character;
        private healthController health;

        public float maxCold = 100f;
        public float cold = 0f;
        public float freezingSpeed = 0.1f;
        public float snowFreezingSpeed = 1f;
        public freezeStates freezeState;
        public float freezeDamage = 1f;
        public float campFirewarmth = 10f;
        public bool isNearCampFire;
        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<healthController>();
        }

        public void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Snow>(out var snow))
            {
                if( freezeState != freezeStates.freezing)
                    cold += snowFreezingSpeed * Time.deltaTime;
                //Debug.Log("Walks in snow");
            }
            if (!other.gameObject.TryGetComponent<Fire>(out var warmth)) return;
            {
                if (cold > 0.01f)
                    cold -= campFirewarmth * Time.deltaTime;
                //Debug.Log("NearCampFire");
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (cold <= maxCold - 0.001f)
                cold += freezingSpeed * Time.deltaTime;
            else
                freezeState = freezeStates.freezing;
            UpdateCold(1);
        }
        void UpdateCold(int value)
        {
            freezeUI.value = cold;
            if(cold < 30)
            {
                freezeState = freezeStates.warm;
            }
            if (cold > 30 && cold < 99)
                freezeState = freezeStates.cold;
            if (value == 0)
            {
                sliderCanvasGroup.alpha = 0;
            }
            else
            {
                sliderCanvasGroup.alpha = 1;
            }
        }
    }
}
