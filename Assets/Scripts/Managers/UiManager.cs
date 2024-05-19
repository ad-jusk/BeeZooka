using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UiManager : MonoBehaviour
{
    private GameManager gameManager; 
    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.OnFlowerEntered += HandleFlowerEntered;
    }
    private void HandleFlowerEntered(FlowerColor flowerColor)
    {
        Debug.Log("A flower of color " + flowerColor + " entered!");

        Transform textTransform = transform.transform.Find(flowerColor.ToString());
        if (textTransform != null)
        {
            TextMeshProUGUI textMeshPro = textTransform.GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                textMeshPro.text = "1/1";
            }
            else
            {
                Debug.LogError("TextMeshPro component not found on the child GameObject.");
            }
        }
        else
        {
            Debug.LogError($"Child GameObject with the name '{flowerColor.ToString()}' not found under the canvas.");
        }
    }
    private void ChangeText(string text, string type)
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
