using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialToCookImageControl : MonoBehaviour
{
    [SerializeField] Image materialImage;
    [SerializeField] TextMeshProUGUI amountRequireText;

    public void InitMaterialRequireImage(Sprite matSprite, int amount)
    {
        materialImage.sprite = matSprite;
        amountRequireText.text = amount.ToString();
    }
}
