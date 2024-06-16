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

  AudioSource audioSource;

  bool isTransitioning = false;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  void OnCollisionEnter(Collision other)
  {
    if (isTransitioning)
    { return; }

    switch (other.gameObject.tag)
    {




      case "Friendly":
        Debug.Log("Friendly");
        break;
      case "Finish":
        StartSuccesSequence();
        break;
      case "Fuel":
        Debug.Log("Fuel");
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
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }



  void StartCrashSequence()
  {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(crash);
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
