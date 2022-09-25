using System;
using TMPro;
using UnityEngine;

public class LifeBarController : MonoBehaviour
{
    public GameObject textObject;
    private TextMeshProUGUI text;
    private RectTransform rectTransform;
    private float originalLifeBarWidth;
    private float originalXPosition;

    private void Start()
    {
        text = textObject.GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();

        originalLifeBarWidth = rectTransform.rect.width;
        originalXPosition = rectTransform.localPosition.x;
    }

    public void updateLife(float shipLife)
    {
        text.text = Math.Max(shipLife, 0).ToString();

        float newWidth = originalLifeBarWidth * (shipLife / 100);
        float leftSpacer = (originalLifeBarWidth - newWidth) / 2;
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
        rectTransform.localPosition =
            new Vector3(originalXPosition - leftSpacer, rectTransform.localPosition.y, rectTransform.localPosition.z);
    }
}