using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
  [SerializeField] float mainThrust = 1000f;
  [SerializeField] float rotateionThrust = 100f;

  [SerializeField] AudioClip mainEngine;
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
      rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

      if (!audioSource.isPlaying)
      {
        audioSource.PlayOneShot(mainEngine);
      }
      else 
      {
        audioSource.Stop();
      }


    }


  }

  void ProcessRotation()
  {
    if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
    {
      ApplyRotation(rotateionThrust);
    }

    else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
    {
      ApplyRotation(-rotateionThrust);
    }

  }

  void ApplyRotation(float rotationThisFrame)
  {
    rb.freezeRotation = true;
    transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    rb.freezeRotation = false;
  }
}
