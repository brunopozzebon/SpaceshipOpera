using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public enum SongType
{
    ROCK, ELETRONIC, DEFAULT, CLASSIC        
}

[RequireComponent(typeof(AudioSource))]
public class SongController : MonoBehaviour
{
    private static int SPECTRUM_LENGTH_CONSIDERED = 10;
    public static int SPECTRUM_SIZE = 64;
    private const float HABILITY_TIME_DECREASE=1.0f;
    
    public float habilityBoostTime = 10f;
    public AudioSource audioChosen;
    public AudioSource musicDefault;
    public AudioSource musicRock;
    public AudioSource musicClassic;
    public AudioSource musicEletronic;
    public RawImage coverImage;
    public Texture[] coverTextures = new Texture[4];
    public static float[] spectrum = new float[SPECTRUM_SIZE];
    public static SongType songType = SongType.DEFAULT;
    
    private StarshipController starshipController;
    private bool canCure = false;
    private static Queue intensityQueue = new Queue(20);
   
    void Start()
    {
        audioChosen = musicDefault;
        audioChosen.Play();
       

        starshipController = GameObject.FindWithTag("Player").GetComponent<StarshipController>();
    }

    private void Update()
    {
        if (songType == SongType.DEFAULT)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                changeSong(musicRock, SongType.ROCK, coverTextures[0]);
                StartCoroutine(returnToDefault());
            }else if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                changeSong(musicEletronic, SongType.ELETRONIC, coverTextures[1]);
                StartCoroutine(returnToDefault());
            }else if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                changeSong(musicClassic, SongType.CLASSIC, coverTextures[2]);
                canCure = true;
                StartCoroutine(health());
                StartCoroutine(returnToDefault());
            }
        }

        audioChosen.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        updateSpectrumItensity();
    }

    static void updateSpectrumItensity()
    {
        
        float sum = 0;
        for (int i =0; i < SPECTRUM_LENGTH_CONSIDERED;i++)
        {
            sum += spectrum[i];
        }

        sum /= SPECTRUM_LENGTH_CONSIDERED;
        
        intensityQueue.Enqueue(sum);

        if (intensityQueue.Count > 20)
        {
            intensityQueue.Dequeue();
        }
    }

    public static float getSongIntensityMedia()
    {
        float sum = 0;
        foreach (float q in intensityQueue)
        {
            sum += q;
        }

        return sum / intensityQueue.Count;
    }

    IEnumerator health()
    {
        while (canCure)
        {
            starshipController.updateLife(1f);
            yield return new WaitForSeconds(1f);
        }
      
    }

    IEnumerator returnToDefault()
    {
        yield return new WaitForSeconds(habilityBoostTime);

        if (habilityBoostTime > HABILITY_TIME_DECREASE)
        {
            habilityBoostTime -= HABILITY_TIME_DECREASE;
        }
    

        changeSong(musicDefault, SongType.DEFAULT, coverTextures[3]);
        canCure = false;
    }

    private void changeSong(AudioSource audioSource, SongType songType, Texture coverTexture)
    {
        audioChosen.Stop();
        audioChosen = audioSource;
        SongController.songType = songType;
        audioChosen.Play();
        coverImage.texture =coverTexture;
        starshipController.changeSong(songType);
    }
    
}
