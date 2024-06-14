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
        if (handImage != null) handImage.SetActive(false);

        if (textIteration < textList.Count && textList[textIteration] != null)
        {
            textList[textIteration].SetActive(false);
        }

        textIteration++;

        if (textIteration < textList.Count && textList[textIteration] != null)
        {
            textList[textIteration].SetActive(true);
        }
    }
}
