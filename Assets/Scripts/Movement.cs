using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] float mainThrust = 1000f;
  [SerializeField] float rotateionThrust = 100f;

  [SerializeField] AudioClip mainEngine;
  [SerializeField] ParticleSystem mainEngineParticles;
  [SerializeField] ParticleSystem leftThusterParticles;
  [SerializeField] ParticleSystem rightThusterParticles;
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
    ProcessThust();
    ProcessRotation();
  }


  void ProcessThust()
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
            StopRotateing();
        }

    }



  void StartThrusting()
  {
    rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

    if (!audioSource.isPlaying)
    {
      audioSource.PlayOneShot(mainEngine);
    }
    if (!mainEngineParticles.isPlaying)
    {
      mainEngineParticles.Play();

    }
  }
  void StopThrusting()
  {
    audioSource.Stop();
    mainEngineParticles.Stop();
  }



    void StopRotateing()
    {
        rightThusterParticles.Stop();
        leftThusterParticles.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-rotateionThrust);
        if (!rightThusterParticles.isPlaying)
        {
            rightThusterParticles.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotateionThrust);
        if (!leftThusterParticles.isPlaying)
        {
            leftThusterParticles.Play();
        }
    }


    void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true;
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false;
  }
}








