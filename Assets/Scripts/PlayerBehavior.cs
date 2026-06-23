using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerBehaviour : MonoBehaviour
{
    // Movement vars
    public float MoveSpeed = 10f;
    public float JumpVelocity = 5f;

    private float _vInput;
    private bool _isJumping;
    private Rigidbody _rb;

    // Ground check vars
    public bool IsOnGround = true;
    public float GroundCheckRadius = 0.3f;
    public LayerMask GroundLayer;

    // Shooting vars
    public GameObject Bullet;
    public float BulletSpeed = 100f;
    private bool _isShooting;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Forward / backward movement
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;

        // Check if player is touching ground
        IsOnGround = Physics.CheckSphere(
            transform.position,
            GroundCheckRadius,
            GroundLayer
        );

        // Jump
        if (Input.GetKeyDown(KeyCode.J) && IsOnGround)
        {
            _isJumping = true;
        }

        // Shoot
        _isShooting |= Input.GetKeyDown(KeyCode.Space);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(
            transform.position,
            GroundCheckRadius
        );
    }

    void FixedUpdate()
    {
        // Move forward/backward
        _rb.MovePosition(
            transform.position +
            transform.forward *
            _vInput *
            Time.fixedDeltaTime
        );

        // Jump
        if (_isJumping)
        {
            _rb.AddForce(
                Vector3.up * JumpVelocity,
                ForceMode.Impulse
            );

            _isJumping = false;
        }

        // Shoot bullet
        if (_isShooting)
        {
            Vector3 spawnPos =
                transform.position +
                transform.forward * 1f;

            GameObject newBullet =
                Instantiate(
                    Bullet,
                    spawnPos,
                    transform.rotation
                );

            Rigidbody bulletRB =
                newBullet.GetComponent<Rigidbody>();

            bulletRB.velocity =
                transform.forward *
                BulletSpeed;

            _isShooting = false;
        }
    }
}