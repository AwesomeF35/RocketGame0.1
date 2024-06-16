using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
      void OnCollisionEnter(Collision other){

        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                LoadNextLevel();
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                ReloadLevel();
                break;


        }

      }

      void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneInedx = currentSceneIndex + 1;
        if (nextSceneInedx == SceneManager.sceneCountInBuildSettings){
            nextSceneInedx = 0;
        }
        SceneManager.LoadScene(nextSceneInedx);

      }

      void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
      }
}
