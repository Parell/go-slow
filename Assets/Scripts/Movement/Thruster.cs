using UnityEngine;

public class Thruster : MonoBehaviour
{
    private ParticleSystem particleSource;
    private AudioSource audioSource;
    private Light lightSource;
    private bool isOn;

    private void Awake()
    {
        particleSource = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        lightSource = GetComponent<Light>();
    }

    public void StartThruster()
    {
        if (!isOn)
        {
            particleSource.Play();
            audioSource.Play();
            lightSource.enabled = true;
            isOn = true;
        }
    }

    public void StopThruster()
    {
        if (isOn)
        {
            particleSource.Stop();
            audioSource.Stop();
            lightSource.enabled = false;
            isOn = false;
        }
    }
}
