using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

  [SerializeField] float levelLoadDelay = 2f;
  [SerializeField] AudioClip success;
  [SerializeField] AudioClip crash;
  [SerializeField] ParticleSystem successParticles;
  [SerializeField] ParticleSystem crashParticles;




  AudioSource audioSource;

  bool isTransitioning = false;
  bool collisionDisable = false;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();

  }

  void Update()
  {
    ResondToDebugKeys();
  }

  void ResondToDebugKeys(){
    if(Input.GetKeyDown(KeyCode.L))
    {
      LoadNextLevel();

    }
    else if (Input.GetKeyDown(KeyCode.C))
    {
      collisionDisable = !collisionDisable;
    }
  }

  void OnCollisionEnter(Collision other)
  {
    if (isTransitioning || collisionDisable) { return; }

    switch (other.gameObject.tag)
    {




      case "Friendly":
        Debug.Log("Friendly");
        break;
      case "Finish":
        StartSuccesSequence();
        break;
      default:
        StartCrashSequence();
        break;


    }

  }

  void StartSuccesSequence()
  {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(success);
    successParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }



  void StartCrashSequence()
  {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(crash);
    crashParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", levelLoadDelay);
  }

  void LoadNextLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneInedx = currentSceneIndex + 1;
    if (nextSceneInedx == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneInedx = 0;
    }
    SceneManager.LoadScene(nextSceneInedx);

  }

  void ReloadLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }
}
