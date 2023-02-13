using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float timeToReload = 2f;

    [SerializeField] ParticleSystem crashEffect;

    [SerializeField] AudioClip crashSFX;

    private bool hasCrashed = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ground" && !hasCrashed)
        {
            hasCrashed = true;
            FindObjectOfType<PlayerController>().DisableControls();
            Debug.Log("Crashed");
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashSFX);
            Invoke("ReloadScene", timeToReload);
        }

    }

  void ReloadScene()
  {
      SceneManager.LoadScene(0);
  }
}
