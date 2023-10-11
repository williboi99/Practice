using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class FpsCharacterMovement : MonoBehaviour
{
    public Vector3 MovementValue;
    public CharacterController characterController;
    [SerializeField] float BaseSpeed;
    public Transform MainCam;
    public Vector2 mouseLook;
    public float Gravity = 9.81f;
    private float SprintBonus = 1f;
    [SerializeField] GameObject pauseScreen;

    public bool paused = false;


    [SerializeField] InputActionAsset mouseControls;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Application.targetFrameRate = 60;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        
    }

    // Update is called once per frame
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector3>();
        
    }
    void Update()
    {
        Vector3 movement = CalculateMovement();
        characterController.Move(movement * BaseSpeed * Time.deltaTime*SprintBonus);

        Paused();
        
    }
    
    
    public void OnEscape(InputAction.CallbackContext escapeContext)
    {
        if(escapeContext.performed)
        {
            paused = !paused;
        }
        
    }
    public void Paused()
    {

        
        if (paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            mouseControls.Disable();
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0;

        }
        else if (!paused)
        {
            paused = false;
            pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            mouseControls.Enable();

        }

    }




    public void resume()
    {
        paused = !paused;
    }
    public void exit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
   
    
    public void OnLook(InputAction.CallbackContext context)
    {
        mouseLook = Time.deltaTime*context.ReadValue<Vector2>();
    }
    public Vector3 CalculateMovement()
    {

        Vector3 forward = MainCam.forward;
        Vector3 right = MainCam.right;

        //Vector3 up = MainCam.up;

        forward.y = 0f;
        right.y = 0f;

        //up.y = -9.81f;

        forward.Normalize();
        right.Normalize();

        return forward * MovementValue.z + right * MovementValue.x + Vector3.up *-Gravity;
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SprintBonus = 2.5f;
        }
        else
        {
            SprintBonus = 1f;
        }
    }





}
