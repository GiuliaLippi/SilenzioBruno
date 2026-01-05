using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum SIDE {Left,Mid,Right}
public class Player : MonoBehaviour
{
    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    [HideInInspector]
    public bool SwipeLeft,SwipeRight,SwipeUp; 
    public float XValue;
    private CharacterController m_char;
    private Animator m_Animator;
    private float x;
    public float SpeedDodge; 
    public float JumpPower = 7f;
    private float y;
    public bool InJump;


    // Start is called before the first frame update
    void Start()
    {
        m_char = GetComponent<CharacterController>();
        m_Animator= GetComponent<Animator>();
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        SwipeLeft = Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow);
        if (SwipeLeft)
        {
            if(m_Side == SIDE.Mid)
            {
                NewXPos= - XValue;
                m_Side = SIDE.Left;
                m_Animator.Play("dodgeLeft");
            }
            else if (m_Side == SIDE.Right)
            {
               NewXPos = 0;
               m_Side = SIDE.Mid;
               m_Animator.Play ("dodgeLeft");

            }
        }
        else if (SwipeRight)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = XValue; 
                m_Side = SIDE.Right;
                m_Animator.Play("dodgeRight");

            }
            else if (m_Side == SIDE.Left)
            {
              NewXPos = 0; 
              m_Side = SIDE.Mid;  
              m_Animator.Play("dodgeRight");

            }
        }

        Vector3 moveVector = new Vector3(x - transform.position.x , y*Time.deltaTime, 0);
        x = Mathf.Lerp(x,NewXPos,Time.deltaTime*SpeedDodge);
        m_char.Move (moveVector);
    }
    public void Jump()
    {
        if (m_char.isGrounded)
        {
        
          if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {
                m_Animator.Play("Landing");
                InJump = false;
            }
          if  (SwipeUp)
            {
                y = JumpPower;
                m_Animator.CrossFadeInFixedTime("Jump", 0.1f);
                InJump = true;
            }
            else
            {
                y -= JumpPower * 2 * Time.deltaTime;
                if(m_char.velocity.y<-0.1f)
                m_Animator.Play("Falling");

            }
          
        }
    }
}