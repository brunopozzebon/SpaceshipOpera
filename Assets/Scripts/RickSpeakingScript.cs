
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class RickSpeakingScript : MonoBehaviour
{
    private const int AUDIOS_FOR_KILL_SIZE = 4;
    private const int AUDIOS_FOR_BE_SHOOTED = 5;

    
    static AudioSource[] audiosForBeShooted = new AudioSource[AUDIOS_FOR_BE_SHOOTED];
    static AudioSource[] audiosForKill = new AudioSource[AUDIOS_FOR_KILL_SIZE];

    private void Start()
    {
        audiosForBeShooted[0] = transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        audiosForBeShooted[1] = transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        audiosForBeShooted[2] = transform.GetChild(2).gameObject.GetComponent<AudioSource>();
        audiosForBeShooted[3] = transform.GetChild(3).gameObject.GetComponent<AudioSource>();
        audiosForBeShooted[4] = transform.GetChild(4).gameObject.GetComponent<AudioSource>();

        
        audiosForKill[0] = transform.GetChild(5).gameObject.GetComponent<AudioSource>();
        audiosForKill[1] = transform.GetChild(6).gameObject.GetComponent<AudioSource>();
        audiosForKill[2] = transform.GetChild(7).gameObject.GetComponent<AudioSource>();
        audiosForKill[3] = transform.GetChild(8).gameObject.GetComponent<AudioSource>();
    }

    public static void playAudioWhenShooted()
    {
        float probabilityToSpeak =  Random.Range(0.0f, 1.0f);
        if (probabilityToSpeak > 0.7 )
        {
            int audioSelectedIndex = Random.Range(0, AUDIOS_FOR_BE_SHOOTED);
            AudioSource audioSelected = audiosForBeShooted[audioSelectedIndex];
            audioSelected.Play();
        }
    }
    
    public static void playAudioWhenKill()
    {
        float probabilityToSpeak =  Random.Range(0.0f, 1.0f);
        if (probabilityToSpeak > 0.9 )
        {
            int audioSelectedIndex = Random.Range(0, AUDIOS_FOR_KILL_SIZE);
            AudioSource audioSelected = audiosForKill[audioSelectedIndex];
            audioSelected.Play();
        }
    }

    public static void playAudioWhenDied()
    {
        int audioSelectedIndex = Random.Range(0, AUDIOS_FOR_BE_SHOOTED);
        AudioSource audioSelected = audiosForBeShooted[audioSelectedIndex];
        audioSelected.Play();
    }
    
    
}
