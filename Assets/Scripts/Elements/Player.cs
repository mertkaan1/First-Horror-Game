using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;
    public bool isAppleCollected;
    public float speed = 3f;
    private Rigidbody _rb;
    private bool _isCharacterMoving;
    public Animator Animator;
    public Transform HandHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Collactable"))
        {
            UIManager.Instance.CollectSound();
            isAppleCollected = true;
            gameDirector.levelManager.AppleCollected();
            Debug.Log("Apple collected!");
        }
        if (other.CompareTag("Door") && isAppleCollected)
        {
            gameDirector.LevelCompleted();
            Debug.Log("Level Completed!");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            UIManager.Instance.LoseSound();
            gameDirector.LevelCompleted("Kaybettin!");
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Arrow keys controls
        MovePlayer();
    }

    private void MovePlayer()
    {
        var direction = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetWalkAnimationSpeed(2f);
            speed = 6;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SetWalkAnimationSpeed(1f);
            speed = 3;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (direction.magnitude <= .1f)
        {
            TriggerIdleAnimation(); // Ensure no vertical movement

        }
        else
        {
            TriggerWalkAnimation(); // Ensure no vertical movement
        }
        if (direction != Vector3.zero)
        {
            transform.LookAt(transform.position + direction);
            _rb.MovePosition(_rb.position + direction.normalized * speed * Time.deltaTime);
        }
    }

    private void SetWalkAnimationSpeed(float speed)
    {
        Animator.SetFloat("WalkAnimationSpeed", speed);
    }

    private void TriggerWalkAnimation()
    {
        if (!_isCharacterMoving)
        {
            Animator.SetTrigger("Walk");
            _isCharacterMoving = true;
            // Trigger walk animation here
            // Example: animator.SetTrigger("Walk");

        }
    }

    private void TriggerIdleAnimation()
    {
        if (_isCharacterMoving)
        {
            Animator.SetTrigger("Idle");
            _isCharacterMoving = false;
        }
    }
}
