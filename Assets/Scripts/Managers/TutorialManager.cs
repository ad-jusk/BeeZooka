using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject handImage;
    [SerializeField] private GameObject arrowImage;
    [SerializeField] private GameObject text1;
    [SerializeField] private GameObject text2;
    [SerializeField] private GameObject text3;
    List<GameObject> textList;
    int textIteration = 0;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.OnFlowerEntered += HandleFlowerEntered;
        textList = new List<GameObject>() { text1, text2, text3};
    }

    private void HandleFlowerEntered(FlowerColor flowerColor)
    {
        handImage.SetActive(false);
        textList[textIteration].SetActive(false);
        textIteration++;
        textList[textIteration].SetActive(true);
        if(textIteration == 2)
        {
            arrowImage.SetActive(true);
        }
    }
}
