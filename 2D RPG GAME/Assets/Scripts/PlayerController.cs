using System;
using DG.Tweening;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private float horizontal;
    private float vertical;

    private Vector2 moveVector;
    private Rigidbody2D rb;
    private Animator animator;
    private EnemyController enemyController;
    
    
    public Transform mainCam;
    public Transform wallCollider;
    private bool isPlayerTriggerToMoveCamera;

    public GameObject weaponObj;
    private bool isWeaponEquipped;

    public GameObject weaponCanvas;
    
    private void Start()
    {
        enemyController = FindObjectOfType<EnemyController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameManager.instance.pauseGame) return;

        HandleInputs();
        HandleScale();

        if (isPlayerTriggerToMoveCamera)
        {
            isPlayerTriggerToMoveCamera = false;
            wallCollider.DOMoveX(14f,2f);
            transform.DOMoveX(16f, 2f);
            mainCam.transform.DOMoveX(23f, 2f).OnComplete(()=> weaponCanvas.SetActive(true));
        }

        if (isWeaponEquipped && Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
        
    }
    
    void FixedUpdate()
    {
        if (GameManager.instance.pauseGame) return;
        // handling player movement
        rb.MovePosition(rb.position + moveVector * (moveSpeed * Time.fixedDeltaTime));
    }
    
    void HandleInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveVector = new Vector2(horizontal, vertical);

    }

    void HandleScale()
    {
        if (horizontal < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            weaponCanvas.SetActive(false);
            enemyController.ActivateEnemy();
            isWeaponEquipped = true;
            other.gameObject.SetActive(false);
            weaponObj.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MoveCameraTowardsNewLocation"))
        {
            other.isTrigger = false;
            isPlayerTriggerToMoveCamera = true;
        }

    }
}
