using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float doubleJumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public Camera mainCamera;

    public Animator animator;

    private GameObject respawnPosition;
    [SerializeField] private GameObject startPos;

    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioSource doubleJumpSound;
    [SerializeField] private AudioClip doubleJumpClip;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioClip deathClip;

    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    float horizontalSpeed = 0.0f;

    bool hasDoubleJumped = false;
    bool inBox = false;
    bool hasFlipped = false;
    Vector3 cameraPos;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;
    GameObserver observer;
    

    // Use this for initialization
    void Start()
    {
        observer = GameObject.Find("GameObserver").GetComponent<GameObserver>();
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;

        transform.position = startPos.transform.position;
        respawnPosition = startPos;

        if (mainCamera)
        {
            cameraPos = mainCamera.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Change facing direction
        if (horizontalSpeed != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        horizontalSpeed = Input.GetAxis("Horizontal");
        horizontalSpeed = Mathf.Clamp(horizontalSpeed, -1, 1);

        animator.SetFloat("Speed", Mathf.Abs(horizontalSpeed));

        // Double Jumping
        if (Input.GetButtonDown("Jump") && !isGrounded && !hasDoubleJumped)
        {
            doubleJumpSound.PlayOneShot(doubleJumpClip);
            observer.FlipSwitch();
            hasDoubleJumped = true;
            r2d.velocity = new Vector2(r2d.velocity.x, doubleJumpHeight);
        }
        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpSound.PlayOneShot(jumpClip);
            hasDoubleJumped = false;
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
        }

        // Camera follow
        if (mainCamera)
        {
            mainCamera.transform.position = new Vector3(t.position.x, cameraPos.y, cameraPos.z);
        }
        Flip();

        if (observer.flipped != hasFlipped) {
            animator.Play("Flip");
            hasFlipped = !hasFlipped;
        }

        
    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider && !colliders[i].isTrigger)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        r2d.velocity = new Vector2(horizontalSpeed * maxSpeed, r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }

    void Flip() {
        // Check if player is within a collider
        animator.SetBool("Flipped", observer.flipped);
        
        if (inBox) {
            Explode();
        }
        // GetComponent<SpriteRenderer>().color = observer.flipped ? Color.blue : Color.red;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Spike") {
            Explode();
        }
    }

    public void Explode() {

        observer.takeDamage();
        StartCoroutine(PlayExplode());
        

        Debug.Log("Player exploded!");
        //Destroy(gameObject);

        //respawn
        
    }

    public IEnumerator PlayExplode() {
        deathSound.PlayOneShot(deathClip);
        animator.SetBool("Dying", true);
        if (observer.flipped == false) {
            animator.Play("Death");
        } else if (observer.flipped == true) {
            animator.Play("DeathRed");
        }
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("Dying", false);
        if (observer.health <= 0) {
            Destroy(gameObject);
        } else {
            transform.position = respawnPosition.transform.position;
        }
    }

    public void ChangeRespawnPosition(GameObject newRespawnPosition) {
        respawnPosition = newRespawnPosition;
    }
    
}
