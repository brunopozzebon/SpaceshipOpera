
using UnityEngine;

public class CarExplosion : MonoBehaviour
{
    private static ParticleSystem particles;
    private static AudioSource audio;
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        audio = GetComponent<AudioSource>();
    }

    public static void explode()
    {
        particles.Play();
        audio.Play();
    }
}
