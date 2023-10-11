using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    [SerializeField] AudioClip nigger;
    [SerializeField] AudioSource AudSource;
    PlayerControls playerControlScript;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void load3rdPerson()
    {
        SceneManager.LoadScene(1);
        
    }
    public void load1stPerson()
    {
        SceneManager.LoadScene(2);
    }
     public void exitGame()
    {
        Application.Quit();
    }

    public void duckButton()
    {
        AudSource.PlayOneShot(nigger);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
