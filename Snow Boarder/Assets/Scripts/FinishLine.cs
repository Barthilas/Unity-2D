using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float timeToReload = 2f;
    [SerializeField] ParticleSystem finishEffect;

  private  void OnTriggerEnter2D(Collider2D other) {
      if(other.tag == "Player")
      {
            Debug.Log("Finish");
            finishEffect.Play();
            Invoke("ReloadScene", timeToReload);
            GetComponent<AudioSource>().Play();
      }
  }

  void ReloadScene()
  {
      SceneManager.LoadScene(0);
  }
}
