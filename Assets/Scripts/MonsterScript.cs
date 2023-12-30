using resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace monsters
{
    public enum AnimStates {Idle, Walk, Attack, Die}

    public class MonsterScript : MonoBehaviour
    {
        public Transform target;
        NavMeshAgent agent;
        public enum States { Patrol, Attack, Follow, Die }
        public States currentState;

        public Transform[] wayPoints;

        public int currentWayPoint;
        public bool inSight;
        public Vector2 directionToPlayer;
        public float attackDistance;
        public float maxFollowDist;
        public float attackPause;
        public float attacktime;
        [HideInInspector]
        public bool ready = false;
        public bool attacks = false;
        private float timer = 0f;
        private float atkTimer = 0f;
        public float monsterDamage = 20f;
        public MonsterAttackZone attackzone;
        private bool mirrored = false;
        private bool freezeRotation = false;
        public Animator animator;
        private AnimStates m_anim;
        public float monsterhealth = 100f;
        private float deathtimer = 0f;
        public float deathtime = 0.5f;
        public GameObject droppedFood;
        public ResourceManager manager;

        private AnimStates anim
        { 
            set
            {
                {
                    m_anim = value;
                    switch (m_anim)
                    {
                        case AnimStates.Idle:
                            //Debug.Log(m_anim);
                            animator.Play("MushroomIdle");
                            break;
                        case AnimStates.Attack:
                            animator.Play("MushroomAttack");
                            //Debug.Log(m_anim);

                            break;
                        case AnimStates.Walk:
                            animator.Play("MushroomRun");
                            //Debug.Log(m_anim);
                            break;
                        case AnimStates.Die:
                            animator.Play("MushroomDeath");
                            break;
                    }
                }
            }
        }

        private void Awake()
        {
            //animator = GetComponent<Animator>();
            //attackzone = GetComponentInChildren(typeof(MonsterAttackZone));  
            agent = GetComponent<NavMeshAgent>();
            //player = target.gameObject.GetComponent<Player>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
        private void OnnTriggerEnter2D(Collision2D other)
        {
            //if (!other.gameObject.TryGetComponent<Player>(out var player)) return;
            //{
            //    Debug.Log("hitPlayer");
            //    player.KnockBack(transform);
            //}
        }
        void Start()
        {
            attackzone.damage = monsterDamage;
        }
        private void OnEnable()
        {
            agent.SetDestination(wayPoints[currentWayPoint + 1].position);
        }
        private void UpdateStates()
        {
            switch (currentState)
            {
                case States.Patrol:
                    Patrol();
                    break;
                case States.Attack:
                    Attack();
                    break;
                case States.Follow:
                    Follow();
                    break;
                case States.Die:
                    attackzone.gameObject.SetActive(false);
                    Die();
                    //Debug.Log("Death");
                    break;
            }
        }
        // Update is called once per frame
        void Update()
        {
            UpdateStates();
            CheckForPlayer();

        }
        private void Patrol()
        {
            if (agent.destination != wayPoints[currentWayPoint].position)
            {
                agent.destination = wayPoints[currentWayPoint].position;
            }
            if (HasReached())
            {
                currentWayPoint = (currentWayPoint + 1) % wayPoints.Length;
            }
            if (inSight && directionToPlayer.magnitude <= maxFollowDist)
            {
                currentState = States.Follow;
            }
        }
        private void Attack()
        {
            //Debug.Log(directionToPlayer.magnitude);
            agent.isStopped = true;
            freezeRotation = true;
            gameObject.GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
            if (timer <= attackPause && !ready)
            {
                timer += Time.deltaTime;
                if (timer >= attackPause)
                {
                    Debug.Log(ready);
                    ready = true;
                    timer = 0;
                }
            }
            if (ready & atkTimer <= attacktime)
            {
                anim = AnimStates.Attack;
                if(!attackzone.gotDamaged)
                    attackzone.gameObject.SetActive(true);
                atkTimer += Time.deltaTime;
                if (atkTimer >= attacktime)
                {
                    Debug.Log("attacks");
                    ready = false;
                    attacks = true;
                    atkTimer = 0;
                    attackzone.gameObject.SetActive(false);
                }
            }
            if (attacks)
            {
                attacks = false;
                currentState = States.Follow;
                freezeRotation = false;
                anim = AnimStates.Idle;
                attackzone.gotDamaged = false;
            }
        }
        private void Follow()
        {
            if (directionToPlayer.magnitude <= attackDistance && inSight)
            {
                agent.ResetPath();
                currentState = States.Attack;
            }
            else
            {
                agent.isStopped = false;
                if (target != null && directionToPlayer.magnitude <= maxFollowDist)
                {
                    agent.SetDestination(new Vector3(target.position.x, target.position.y, transform.position.z));
                }
                else
                {
                    currentState = States.Patrol;
                }
            }
        }
        private bool HasReached()
        {
            //Debug.Log(agent.hasPath);
            //Debug.Log(!agent.pathPending);
            //Debug.Log(agent.remainingDistance <= agent.stoppingDistance);
            //Debug.Log(agent.hasPath && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
            return (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);
        }
        private void CheckForPlayer()
        {
            if (monsterhealth <= 0)
            {
                timer = 0;
                currentState = States.Die;           
            }
            if(agent.isStopped != true)
            {
                anim = AnimStates.Walk;
            }
            else if(!ready)anim = AnimStates.Idle;
            //Debug.Log(transform.rotation.y);
            //Debug.Log(mirrored);
            if ((target.position - transform.position).x <= 0 && mirrored == false && !freezeRotation)
            {
                transform.Rotate(new Vector3(0, 180f, 0));
                mirrored = true;
            }
            else if ((target.position - transform.position).x > 0 && mirrored == true && !freezeRotation)
            {
                mirrored = false;
                transform.Rotate(new Vector3(0, 180f, 0));
            }
            directionToPlayer = (target.position - transform.position).normalized;
            RaycastHit2D hitInfo;
            hitInfo = Physics2D.Raycast(transform.position, directionToPlayer.normalized);
            if (hitInfo)
            {
                inSight = hitInfo.transform.CompareTag("Player");
                Debug.DrawRay(transform.position, directionToPlayer.normalized);
                //if (inSight)
                //    Debug.Log("player sighted");
            }
        }
        public void GetDamage(float damage)
        {
            monsterhealth -= damage;
        }
        private void Die()
        {
            anim = AnimStates.Die;
            deathtimer += Time.deltaTime;
            if(deathtimer >= deathtime)
            {
                droppedFood.GetComponent<Resource>().food = Random.RandomRange(35, 55);
                manager.SpawnResources(droppedFood, transform);
                Destroy(gameObject);
            }
        }
    }
}
