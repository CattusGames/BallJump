using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private float _jumpLenght = 1f;
    [SerializeField] private float _obstacleDistance = 2f;
    [SerializeField] private float _doorDistance = 2f;
    [SerializeField] private float _fallMultiplier;
    [SerializeField] private float _jumpMultiplier;
    [SerializeField] private LayerMask _obstacleLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _doorLayer;
    [SerializeField] private ParticleSystem _dustParticle;
    private GameManager gameManager;
    private bool isJumping = false;
    private bool isGrounded = false;
    private bool isFinish = false;
    private bool canJump = true;
    private Rigidbody rb;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
        gameManager.onFinish.AddListener(Finish);
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawSphere(transform.position + transform.forward* _obstacleDistance, transform.localScale.x - 1f);
    }
    private void FixedUpdate()
    {
        isGrounded = GroundCheck();
        RaycastHit hit;
        if (canJump)
        {
            if (Physics.SphereCast(transform.position, transform.localScale.x - 1f, transform.forward, out hit, _obstacleDistance, _obstacleLayer))
            {
                isJumping = false;
                Debug.Log("Obstacle");
            }
            else
            {
                isJumping = true;
            }

            if (isGrounded && isJumping)
            {
                _dustParticle.Play();
                rb.AddForce(transform.up * _jumpHeight, ForceMode.Impulse);
                rb.AddForce(transform.forward * _jumpLenght, ForceMode.Impulse);
            }
            else if (!isGrounded)
            {
                if (rb.velocity.y < 0)
                {
                    rb.velocity += Vector3.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime;
                }
                else if (rb.velocity.y > 0)
                {
                    rb.velocity += Vector3.up * Physics.gravity.y * _jumpMultiplier * Time.deltaTime;
                }
            }
        }

        if (Physics.SphereCast(transform.position, transform.localScale.x, transform.forward, out hit, _doorDistance, _doorLayer)&&!isFinish)
        {

            gameManager.onDoorRange.Invoke();
            canJump = false;
        }
    }
    private void Finish()
    {
        isFinish = true;
    }
    private bool GroundCheck()
    {
        if (Physics.CheckSphere(transform.position, transform.localScale.x, _groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnMouseDown()
    {
        canJump = false;
    }
    private void OnMouseUp()
    {
        canJump = true;
    }
}
