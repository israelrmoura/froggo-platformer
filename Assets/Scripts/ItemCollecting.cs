using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
Mantém uma contagem de quantos itens colecionáveis o jogador pegou.
*/
public class ItemCollecting : MonoBehaviour
{
    private int kiwiCount = 0;
    [SerializeField]private TMP_Text countText;
    [SerializeField] AudioSource collectSFX;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Collectible")){
            Destroy(other.gameObject);
            kiwiCount++;
            Debug.Log(kiwiCount);
            collectSFX.Play();

            countText.text = "Kiwis: " + kiwiCount;
        }
    }



}
