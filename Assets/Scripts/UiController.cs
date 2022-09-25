using System;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public GameObject spectrumPilar;
    public GameObject[] rays = new GameObject[SongController.SPECTRUM_SIZE];
    public float turbineHeighScalar = 5;
    public GameObject disc;
    private RectTransform discTransform;
    
    void Start()
    {
        for (int i = 0; i<SongController.SPECTRUM_SIZE/2; i++)
        {
            GameObject newRay = Instantiate(spectrumPilar);
            newRay.transform.parent = transform;
            newRay.transform.position=  new Vector3((10*i), 
               0, 0);
            rays[i] = newRay;
        }

        discTransform = disc.GetComponent<RectTransform>();
    }

    void Update()
    {
        
        float spectrumMedia = 0;
        
        for (int i = 0; i < SongController.SPECTRUM_SIZE/2; i++)
        {
            float finalHeightScale = SongController.spectrum[i] * turbineHeighScalar;
            spectrumMedia += SongController.spectrum[i];
            rays[i].transform.localScale = new Vector3(0.033f,  finalHeightScale,0.033f);
        }

        spectrumMedia = (spectrumMedia / SongController.SPECTRUM_SIZE) * 10000f;
        spectrumMedia = Math.Clamp(spectrumMedia, 100, 110);
        
        discTransform.sizeDelta = new Vector2(spectrumMedia, spectrumMedia );
    }
}
