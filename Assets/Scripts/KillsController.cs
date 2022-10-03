
using UnityEngine;
using TMPro;
public class KillsController : MonoBehaviour
{
    public GameObject textObject;
    private TextMeshProUGUI text;
    
    private RectTransform rectTransform;
    private  float originalBarWidth;
    private float originalXPosition;

    public static int kills = 0;
    public static int KILLS_TO_WIN = 20;
    private void Start()
    {
        text = textObject.GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();

        originalBarWidth = rectTransform.rect.width;
        originalXPosition = rectTransform.localPosition.x;
    }

    public void addKill()
    {
        kills++;
        text.text = kills.ToString();
        float newWidth = originalBarWidth * (kills / 100);
        float leftSpacer = (originalBarWidth - newWidth) / 2;
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
        rectTransform.localPosition =
            new Vector3(originalXPosition - leftSpacer, rectTransform.localPosition.y, rectTransform.localPosition.z);
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        if (kills > KILLS_TO_WIN && enemies.Length <= 1)
        {
            Debug.Log("WIN");
        }
    }
}
