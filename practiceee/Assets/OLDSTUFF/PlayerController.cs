using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Vector2 moveDirection;
    Vector2 lookDirection;
    public float running;
    public float runSpeed;
    public float sideways=0;
    public float sidewaysSpeed;
    public float maxSpeed = 1.1f;
    public float maxSideways = 1.1f;
    public float lookSensitivity = .25f;

    public Transform camTarget;


    public Transform rotPoint;

    const float Acceleration = 4f;
    const float Deceleration = 25f;

    public bool isGrounded;

    Animator anim;
    Rigidbody playerRig;

    bool IsMoveInput
    {
        get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
    }

    

    public void OnRun(InputAction.CallbackContext context)
    {

        if (isGrounded == true)
        {
            if (context.performed)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }
        else if (isGrounded == false)
        {
            anim.SetBool("isRunning", false);
        }
    }

    //Vector2 lastLook;
    
    void FixedUpdate()
    {
       // lastLook += new Vector2(lookDirection.x, lookDirection.y);
        
        // playerRig.transform.Rotate(0, lookDirection.x*lookSensitivity, 0);

        // Move(moveDirection);
    }

    private void LateUpdate()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
    

    public void OnAim(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            anim.SetBool("AimBool", true);
        }
        else
        {
            anim.SetBool("AimBool", false);
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            anim.applyRootMotion = false;
            isGrounded = false;
            anim.SetFloat("SidewaysSpeed", 0);
            anim.SetFloat("ForwardSpeed", 0);
            anim.SetBool("AimBool", false);
            anim.SetBool("Falling",true);

        }
        else
        {
            isGrounded = true;
            return;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("ground"))
        {
            anim.SetBool("Falling", false);
            isGrounded = true;
            anim.applyRootMotion = true;
        }
    }
    void ExitGame()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }


    void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude > 1f)
            direction.Normalize();
        if (direction.sqrMagnitude < 1f)
            direction.Normalize();

        float acceleration = IsMoveInput ? Acceleration : Deceleration;



        if (isGrounded)
            {
            runSpeed = direction.magnitude * maxSpeed * direction.y;

            sidewaysSpeed = direction.magnitude * maxSideways * direction.x;

            running = Mathf.MoveTowards(running, runSpeed, acceleration * Time.deltaTime);

            sideways = Mathf.MoveTowards(sideways, sidewaysSpeed, acceleration * Time.deltaTime);

            anim.SetFloat("ForwardSpeed", running);

            anim.SetFloat("SidewaysSpeed", sideways);
        }


    }



    //TRYING TO MOVE RELATIVE TO CAMERA
    void MoveTarget()
    {
        
        //playerRig.transform.LookAt(camTarget);

    }

    
    // Start is called before the first frame update
    void Start()
    {
        playerRig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        isGrounded = true;
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    public void OnLook(InputAction.CallbackContext context)
    {
        lookDirection = context.ReadValue<Vector2>();
    }

    Vector2 lastLook;
    void Update()
    {

        //lastLook += new Vector2(lookDirection.x, lookDirection.y);
        //playerRig.transform.Rotate(0, lookDirection.x * lookSensitivity, 0);
        Move(moveDirection);
        MoveTarget();
        ExitGame();
    }
    






    // put in fixed Update //rotPoint.Rotate(0, lookDirection.x*lookSensitivity, 0);
}
