using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public PlayerInput input;
    Vector2 playerInput_ { get; set; }
    [SerializeField]

    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input.enabled = true;
    }
    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        playerInput_ = input.actions["Movement"].ReadValue<Vector2>();
        Debug.Log(playerInput_.x);
    }

    private void FixedUpdate()
    {
        rb.velocity = playerInput_ * moveSpeed;
    }
}
