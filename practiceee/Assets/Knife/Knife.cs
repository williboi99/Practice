using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Knife : MonoBehaviour
{
    [SerializeField] Animator KnifeAnim;
    [SerializeField] AudioSource AudSc;

    [SerializeField] AudioClip[] woodHitClips;
    [SerializeField] AudioClip[] metalHitClips;
    [SerializeField] AudioClip[] bottleHitClips;


    [SerializeField] GameObject brokenBottle;
    [SerializeField] GameObject fullBottle;

    public Camera FPCamera;
    public GameObject stabDecal;
    public GameObject knifeSparks;
    public GameObject woodStabDecal;
    public GameObject knifeChips;

    public float knifeRange = 2f;
    public RaycastHit hit;
    GameObject stabSticker;
    GameObject stabSparks;

    AudioClip woodHit;
    AudioClip bottleBreak;
    AudioClip metalHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        


    }

    public void OnStab(InputAction.CallbackContext context)
    {
        if(context.performed)
        {

            KnifeAnim.SetTrigger("Stab");
            woodHit = woodHitClips[Random.Range(0, woodHitClips.Length)];
            metalHit = metalHitClips[Random.Range(0, metalHitClips.Length)];
        }
        
    }

    void decal()
    {
        {
            Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, knifeRange);
            if (hit.collider.CompareTag("Wall"))
            {
                AudSc.PlayOneShot(metalHit);
                GameObject stabSticker = Instantiate(stabDecal, hit.point, Quaternion.LookRotation(hit.normal));
                GameObject stabSparks = Instantiate(knifeSparks, hit.point, Quaternion.LookRotation(hit.normal));

                stabSticker.transform.parent = hit.collider.transform;

                Destroy(stabSticker, 6f);
                Destroy(stabSparks, 6f);
            }
            else if (hit.collider.CompareTag("Door"))
            {
                AudSc.PlayOneShot(woodHit);



                GameObject woodStabSticker = Instantiate(woodStabDecal, hit.point, Quaternion.LookRotation(hit.normal));
                GameObject stabChips = Instantiate(knifeChips, hit.point, Quaternion.LookRotation(hit.normal));

                woodStabSticker.transform.parent = hit.collider.transform;

                Destroy(woodStabSticker, 6f);
                Destroy(stabChips, 1f);
            }
        }
    }

    void breakBottle()
    {
        RaycastHit bottleHit;
        bottleBreak = bottleHitClips[Random.Range(0, bottleHitClips.Length)];

        Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out bottleHit, 1.5f);
        if(bottleHit.collider.CompareTag("Bottle"))
        {
            GameObject NewBottle = Instantiate(brokenBottle, bottleHit.collider.transform.position, Quaternion.identity );
            AudSc.PlayOneShot(bottleBreak);
            GameObject currentBottle = bottleHit.collider.gameObject;
            Destroy(currentBottle);
            Destroy(NewBottle, 10f);

        }

    }

  
    

    

    


}
