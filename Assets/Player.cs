using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    private PlayerInput input;
    Vector2 playerInput_ { get; set; }
    [SerializeField]
    public GameObject attackPosition;
    public GameObject fireplaceZone;

    public float moveSpeed = 2f;

    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;

    private bool crafting = false;
    private float craftInterval = 0.25f;
    private float craftTimer = 0f;

    private Rigidbody2D rb;

    public ResourceManager resourceManager;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input.enabled = true;
        attackPosition.transform.localPosition = new Vector2(transform.position.x + 1, transform.position.y);
        input.actions["Eat"].performed += Eat_performed;
        input.actions["Craft"].performed += Craft_performed;
    }

    private void Craft_performed(InputAction.CallbackContext obj)
    {
        if (!crafting)
        {
            crafting = true;
            resourceManager.SpawnCampFire(transform);
            transform.position = new Vector3(transform.position.x+0.1f, transform.position.y + 0.1f, transform.position.z);
        }
    }

    private void Eat_performed(InputAction.CallbackContext obj)
    {     
        resourceManager.ConsumeFood(25);
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInput_ = input.actions["Movement"].ReadValue<Vector2>();
        if(attacking)
        {
            timer += Time.deltaTime;
            if (timer > timeToAttack)
            {
                timer = 0f;
                attacking = false;
                attackPosition.SetActive(attacking);
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
        if(input.actions["Attack"].IsPressed() && !attacking)
        {
            Attack();
            //Debug.Log("atatck");    
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = playerInput_ * moveSpeed;
        if(playerInput_.x != 0 || playerInput_.y !=0)
            attackPosition.transform.localPosition = playerInput_;
        if (playerInput_.x != 0 || playerInput_.y != 0)
            fireplaceZone.transform.localPosition = playerInput_;
    }

    private void Attack()
    {
        attacking = true;
        attackPosition.SetActive(attacking);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.TryGetComponent<Resource>(out var resource)) return;
        {
            Debug.Log("found wood");
            resourceManager.AddResource(resource);
            Destroy(other.gameObject);
        }
    }
}
