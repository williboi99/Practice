using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AllInputs : MonoBehaviour
{
    public Transform MainCam;
    public Vector2 MovementValue;
    public CharacterController Controller;
    float MvSpeedFloat;
    public Animator playerAnim;
    public float MvSpeedBonus = 2f;
    public float sprintBonus;
    public bool isSprint;
    [SerializeField] Canvas canvas;
    public bool isPaused;


    //gravity//
    public float verticalVelocity;
    public Vector3 GMovement => Vector3.up * verticalVelocity;


    // Start is called before the first frame update
    void Start()
    {


        isPaused = false;
        canvas.enabled = false;
        MainCam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void OnEscape(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            isPaused = !isPaused;
        }
        if (isPaused)
        {

            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;
            canvas.enabled = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            canvas.enabled = false;
        }
    }






    void Update()
    {



        MvSpeedFloat = playerAnim.GetFloat("MvSpeed");
        Vector3 movement = CalculateMovement();



        Controller.Move(movement * Time.deltaTime * MvSpeedFloat * MvSpeedBonus * sprintBonus + GMovement);





        if (MovementValue == Vector2.zero)
        {
            playerAnim.SetFloat("MvSpeed", 0, .2f, Time.deltaTime);
        }
        else
        {
            if (isSprint == true)
            {
                sprintBonus = 1.15f;
                playerAnim.SetFloat("MvSpeed", 1.65f, .2f, Time.deltaTime);
            }
            else
            {
                playerAnim.SetFloat("MvSpeed", .825f, .3f, Time.deltaTime);
            }
        }


        if (movement != Vector3.zero)
        {
            //transform.rotation = Quaternion.LookRotation(movement); use this line only if it doesn't work out
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), 15f * Time.deltaTime);
        }

        //gravity stuff//
        if (verticalVelocity < -.01f)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }




    }


    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprint = true;
            //sprintBonus = 1.65f;
            //playerAnim.SetFloat("MvSpeed", 1.65f, .2f, Time.deltaTime);
        }

        else
        {
            isSprint = false;
            sprintBonus = 1;
            playerAnim.SetFloat("MvSpeed", 1f, .2f, Time.deltaTime);
        }

    }


    public void OnLook(InputAction.CallbackContext context)
    {

    }






    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();

    }
    public Vector3 CalculateMovement()
    {

        Vector3 forward = MainCam.forward;
        Vector3 right = MainCam.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * MovementValue.y + right * MovementValue.x;
    }


}
