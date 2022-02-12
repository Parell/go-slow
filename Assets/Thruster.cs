using UnityEngine;

public class Thruster : MonoBehaviour
{
    private ParticleSystem particles;
    bool isOn;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void StartThruster()
    {
        if (!isOn)
        {
            particles.Play();
            isOn = true;
        }
    }

    public void StopThruster()
    {
        if (isOn)
        {
            particles.Stop();
            isOn = false;
        }
    }
}
