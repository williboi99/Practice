using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fullPlateSounds : MonoBehaviour
{

    [SerializeField] AudioSource AudSc;
    [SerializeField] AudioClip glassSound;
    [SerializeField] GameObject brokenPlate;
    [SerializeField] GameObject thisPlate;
    Quaternion thisRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion thisRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        AudSc.PlayOneShot(glassSound);
        GameObject platePieces = Instantiate(brokenPlate, transform.position, thisRotation);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(platePieces, 5f);
        Destroy(this, 1f);
    }
}
