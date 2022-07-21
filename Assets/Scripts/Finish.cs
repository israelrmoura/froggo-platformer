using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSFX;
    private bool finished = false;

    private void Start()
    {
        finishSFX = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D obj) {
        if (obj.gameObject.name == "Player" && !finished){
            finishSFX.Play();
            finished = true;
            Invoke("CompleteLevel", 1.897f);
        }
    }

    // Chama o próximo nível baseado no build index dele.
    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
