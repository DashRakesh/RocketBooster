using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. reference for readbility or speed  
    // STATE - private instance (member) variables

    [SerializeField] float mainTrust = 100f;
    [SerializeField] float rotationTrust = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftTrusterParticle;
    [SerializeField] ParticleSystem rightTrusterParticle;

    Rigidbody rb;
    AudioSource audioSource;

  


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
       if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();
    }

    void RotateLeft()
    {
        ApplyRotation(rotationTrust);
        if (!rightTrusterParticle.isPlaying)
        {
            rightTrusterParticle.Play();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainTrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }
    }


    void RotateRight()
    {
        ApplyRotation(-rotationTrust);
        if (!leftTrusterParticle.isPlaying)
        {
            leftTrusterParticle.Play();
        }
    }

    void StopRotating()
    {
        rightTrusterParticle.Stop();
        leftTrusterParticle.Stop();
    }

   

  

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;  // freeze rotation so we can manualy rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfrezing rotation so that physics system can take over

    }
}
