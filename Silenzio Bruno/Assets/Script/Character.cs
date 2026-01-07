using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SIDE { Left, Mid, Right }

public class Player : MonoBehaviour
{
    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    [HideInInspector]
    public bool SwipeLeft, SwipeRight, SwipeUp;
    
    public float XValue = 3f;
    public float SpeedDodge = 10f;
    public float JumpPower = 7f;
    public bool InJump;

    private CharacterController m_char;
    private Animator m_Animator;
    private float x;
    private float y;

    void Start()
    {
        m_char = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();
        transform.position = Vector3.zero;
    }

    void Update()
    {
        // Input
        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);

        // Logica Corsie
        if (SwipeLeft)
        {
            if (m_Side == SIDE.Mid) { NewXPos = -XValue; m_Side = SIDE.Left; m_Animator.Play("dodgeLeft"); }
            else if (m_Side == SIDE.Right) { NewXPos = 0; m_Side = SIDE.Mid; m_Animator.Play("dodgeLeft"); }
        }
        else if (SwipeRight)
        {
            if (m_Side == SIDE.Mid) { NewXPos = XValue; m_Side = SIDE.Right; m_Animator.Play("dodgeRight"); }
            else if (m_Side == SIDE.Left) { NewXPos = 0; m_Side = SIDE.Mid; m_Animator.Play("dodgeRight"); }
        }

        // Gestione Salto e Gravità
        HandleJump();

        // Movimento Finale
        x = Mathf.Lerp(x, NewXPos, Time.deltaTime * SpeedDodge);
        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, 0);
        m_char.Move(moveVector);
    }

    private void HandleJump()
    {
        if (m_char.isGrounded)
        {
            if (InJump)
            {
                m_Animator.Play("Landing");
                InJump = false;
            }
            if (SwipeUp)
            {
                y = JumpPower;
                m_Animator.CrossFadeInFixedTime("Jump", 0.1f);
                InJump = true;
            }
        }
        else
        {
            y -= JumpPower * 2 * Time.deltaTime; // Gravità
            if (m_char.velocity.y < -0.1f && InJump)
                m_Animator.Play("Falling");
        }
    }
}