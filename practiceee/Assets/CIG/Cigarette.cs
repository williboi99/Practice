using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cigarette : MonoBehaviour
{
    [SerializeField] ParticleSystem smokeParticles;
    [SerializeField] ParticleSystem sparkParticles;
    [SerializeField] Animator anim;

    [SerializeField] AudioSource AudSc;
    [SerializeField] AudioClip Inhale;
    [SerializeField] AudioClip Exhale;

    [SerializeField] GameObject light1;
    [SerializeField] GameObject light2;
    [SerializeField] GameObject light3;
    [SerializeField] GameObject light4;

    bool isSmoking = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Smoking();
    }


    void PlayParticles()
    {
        smokeParticles.Play();
        sparkParticles.Play();
    }
    void StopParticles()
    {
        smokeParticles.Stop();
        sparkParticles.Stop();
    }

    public void OnSmoke(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isSmoking = !isSmoking;
        }
    }
    public void Smoking()
    {
        if(isSmoking == true)
        {
            anim.SetBool("Smoking", true);
        }
        else
        {
            anim.SetBool("Smoking", false);
        }
    }

    public void CigLightsOn()
    {
        light1.SetActive(true);
        light2.SetActive(true);
        light3.SetActive(true);
        light4.SetActive(true);
    }
    public void CigLightsOff()
    {
        light1.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
        light4.SetActive(false);
    }

    void PlayInhale()
    {
        AudSc.PlayOneShot(Inhale);
    }
    void PlayExhale()
    {
        AudSc.PlayOneShot(Exhale);
    }

}
