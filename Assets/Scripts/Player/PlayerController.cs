using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;


//[RequireComponent(typeof(Rigidbody))]
//[DefaultExecutionOrder(1)]
public class PlayerController : MonoBehaviour
{
    /*public float moveSpeed = 10.0f;
    public float jumpSpeed = 500.0f;

    Rigidbody rb;
    GameObject model;
    Animator anim;

    public bool shoot = false;

    Vector3 curMoveInput;
    Vector3 moveDir;
    public float lookThreshold = 0.6f;

    Plane plane = new Plane(Vector3.up, 0); 

    public LayerMask isGroundLayer;
    public float groundCheckRadius;
    public Transform groundCheck;
    public bool isGrounded;

    Vector3 mousePos;
    public Camera cam;
    private float lookRotation;

    //player insatance
    private PlayerController playerInstance;

    public bool isDead = false;


    int errorCounter = 0;

    Vector2 move;
    Vector2 jump;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            rb = GetComponent<Rigidbody>();
            model = GameObject.FindGameObjectWithTag("PlayerModel");
            anim = model.GetComponent<Animator>();

            if (!rb) throw new UnassignedReferenceException("Rigidbody not set on " + name);
            if (!groundCheck) throw new UnassignedReferenceException("groundCheck not set on " + name);
            if (!cam) throw new UnassignedReferenceException("cam not set on " + name);


        }
        catch (UnassignedReferenceException e)
        {
            Debug.Log(e.Message);
            errorCounter++;
        }
        finally
        {
            Debug.Log("The script ran with " + errorCounter.ToString() + "errors");
        }


        GameManager.Instance.playerActions.Player.Move.performed += ctx => Move(ctx);
        GameManager.Instance.playerActions.Player.Move.canceled += ctx => Move(ctx);

        GameManager.Instance.playerActions.Player.Jump.performed += ctx => Jump(ctx);
        GameManager.Instance.playerActions.Player.Fire.performed += ctx => Fire(ctx);

    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = (Physics.OverlapSphere(groundCheck.position, groundCheckRadius, isGroundLayer)).Length >0;
        
        curMoveInput.y = rb.velocity.y;
        rb.velocity = curMoveInput;

        if(Mathf.Abs(moveDir.magnitude)>0)
        {
            float dotProduct = Vector3.Dot(moveDir, transform.forward);

            if(dotProduct > -lookThreshold && dotProduct < lookThreshold) 
            {
                float horizontalDotProduct = Vector3.Dot(Vector3.left, moveDir);
                float hValue;
                if (horizontalDotProduct > 0)
                    hValue = -1;
                else
                    hValue = 1;

                anim.SetFloat("Horizontal", hValue);
                anim.SetFloat("Vertical", 0);
            }

            if (dotProduct >= lookThreshold)
            {
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", 1);
            }

            if (dotProduct >= lookThreshold)
            {
                anim.SetFloat("Horizontal", 0);
                anim.SetFloat("Vertical", -1);
            }
        }
        else
        {
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Vertical", 0);
        }

        if (!isGrounded)
        {
            jump = new Vector2 (10, 10);
            anim.SetFloat("Horizontal", jump.x);
            anim.SetFloat("Vertical", jump.y);
        }

       
        /*
        float distance;
        Ray mouseRay = GameManager.Instance.MousePos();
        Vector3 worldPos;

        if (plane.Raycast(mouseRay, out distance))
        {
            worldPos = mouseRay.GetPoint(distance);
            Vector3 relativePos = worldPos - transform.position;
            relativePos.y = 0;

            transform.rotation = Quaternion.LookRotation(relativePos);
        }*/

    }

   

   /* public void Move(InputAction.CallbackContext ctx)
    {
        if(ctx.canceled)
        {
            curMoveInput = Vector3.zero;
            moveDir = Vector3.zero;
            return; 
        }
       move = ctx.ReadValue<Vector2>();
       move.Normalize();

        moveDir = new Vector3(move.x, 0, move.y);
        curMoveInput = moveDir * moveSpeed;
        if(isGrounded)
            curMoveInput = transform.TransformDirection(curMoveInput);

    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (!isGrounded)
            return;
        
        rb.AddForce(jumpSpeed * Vector3.up);
      
    }

    private void Fire(InputAction.CallbackContext ctx)
    {
        

    }
}*/
