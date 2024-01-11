using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using resources;

public class Player : MonoBehaviour
{
    public PlayerInput input;
    Vector2 playerInput_ { get; set; }
    [SerializeField]
    public GameObject attackPosition;
    public GameObject fireplaceZone;

    public float moveSpeed = 2f;

    private bool attacking = false;
    private float timeToAttack = 0.5f;
    private float timer = 0f;
    

    private bool crafting = false;
    private float craftInterval = 0.25f;
    private float craftTimer = 0f;
    PlayerStates m_currentState;
    bool canMove = true;
    bool stateLock = false;

    private Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sprite;

    public ResourceManager resourceManager;
    AudioSource running;
    public enum PlayerStates { Idle, Run, Strike, Die };
    private void Awake()
    {
        input = new PlayerInput();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //attackPosition.transform.localPosition = new Vector2(transform.position.x + 1, transform.position.y);
        input.Player.Eat.performed += Eat_performed;
        input.Player.Craft.performed += Craft_performed;
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        attackPosition.GetComponent<AttackZone>().damage = ResourceManager.attackDamage;
        attackPosition.transform.localPosition = new Vector3(1.3f, 0, 0);
        running = GetComponent<AudioSource>();
    }

    PlayerStates CurrentState
    {
        set
        {
            {
                if (!stateLock)
                {
                    m_currentState = value;
                    switch (m_currentState)
                    {
                        case PlayerStates.Idle:
                            //Debug.Log(m_currentState);
                            FindObjectOfType<AudioManager>().Stop("Run");
                            animator.Play("Idle");
                            canMove = true;
                            break;
                        case PlayerStates.Strike:
                            FindObjectOfType<AudioManager>().Play("Sword");
                            running.Stop();
                            animator.Play("KnightStrike");
                            canMove = false;
                            stateLock = true;
                            //Debug.Log(m_currentState);
                        
                            break;
                        case PlayerStates.Run:
                            FindObjectOfType<AudioManager>().Play("Run");
                            animator.Play("KnightRun");
                            canMove = true;
                        //Debug.Log(m_currentState);
                        break;
                        case PlayerStates.Die:
                            animator.Play("PlayerDeath");
                            stateLock = true;
                            break;
                    }
                }
            }
        }
        get
        {
            return PlayerStates.Run;
        }
    }
    private void Craft_performed(InputAction.CallbackContext obj)
    {
        if (!crafting)
        {
            crafting = true;
            var crafted = resourceManager.SpawnCampFire(transform);
            if(crafted)
                transform.position = new Vector3(transform.position.x+0.1f, transform.position.y + 0.1f, transform.position.z);
        }
    }

    private void Eat_performed(InputAction.CallbackContext obj)
    {     
        resourceManager.ConsumeFood(25);
    }

    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(m_currentState);
        //Debug.Log(canMove);
        if (canMove)
        {
            playerInput_ = input.Player.Movement.ReadValue<Vector2>();
        }
        else playerInput_ = Vector2.zero;

        if (attacking)
        {
            timer += Time.deltaTime;
            if (timer > timeToAttack)
            {
                timer = 0f;
                attacking = false;
                attackPosition.SetActive(attacking);
                CurrentState = PlayerStates.Idle;
                canMove = true;
                stateLock = false;
            }
        }
        if(crafting)
        {
            craftTimer += Time.deltaTime;
            if(craftTimer > craftInterval)
            {
                craftTimer = 0f;
                crafting = false;
                fireplaceZone.SetActive(crafting);
            }
        }
        if (playerInput_.x != 0)
        {
            if (playerInput_.x > 0)
                sprite.flipX = false;
            else sprite.flipX = true;
        }
            if (input.Player.Attack.IsPressed() && !attacking)
        {
            
            Attack();
            playerInput_ = Vector2.zero;
            //Debug.Log("atatck");    
        }
        if (playerInput_ != Vector2.zero && !attacking)
        {
            if(!running.isPlaying)
            {
                running.Play();
            }
            CurrentState = PlayerStates.Run;
        }
        else if (!attacking)
        {
            CurrentState = PlayerStates.Idle;
            running.Stop();
        }
    }

    private void FixedUpdate()
    {
            rb.velocity = playerInput_ * moveSpeed;
            if (playerInput_.x != 0 || playerInput_.y != 0)
                attackPosition.transform.localPosition = playerInput_;
    }

    private void Attack()
    {
        CurrentState = PlayerStates.Strike;
        attacking = true;
        attackPosition.SetActive(attacking);
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Resource>(out var resource))
        {
            Debug.Log("found resource");
            FindObjectOfType<AudioManager>().Play("PickUp");
            resourceManager.AddResource(resource);
            Destroy(other.gameObject);
        }
        //if (other.gameObject.TryGetComponent<Enemy>(out var enemy))
        //{
        //    Debug.Log("knockback");
        //    KnockBack(enemy.transform);
        //}
    }
    public void Die()
    {
        CurrentState = PlayerStates.Die;
    }
}
