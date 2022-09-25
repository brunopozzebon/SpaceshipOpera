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
    public AudioSource audioChosen;

    public AudioSource musicDefault;
    public AudioSource musicRock;
    public AudioSource musicClassic;
    public AudioSource musicEletronic;
    public RawImage coverImage;
    public Texture[] coverTextures = new Texture[4];
    

    public static int SPECTRUM_SIZE = 64;
    public static float[] spectrum = new float[SPECTRUM_SIZE];

    public float HabilityBoostTime = 10f;
    public static SongType songType = SongType.DEFAULT;
    private StarshipController starshipController;
   
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
                StartCoroutine(returnToDefault());
            }
        }
       
        
        audioChosen.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
    }

    IEnumerator returnToDefault()
    {
        yield return new WaitForSeconds(HabilityBoostTime);

        changeSong(musicDefault, SongType.DEFAULT, coverTextures[3]);
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
