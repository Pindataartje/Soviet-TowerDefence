using UnityEngine;

public class GunshotAudioManager : MonoBehaviour
{
    public ParticleSystem gunshotParticles;
    public AudioSource gunshotAudioSource;
    public AudioClip gunshotClip;

    private int previousParticleCount;

    void Start()
    {
        // Initialize the previousParticleCount
        previousParticleCount = 0;
    }

    void Update()
    {
        int currentParticleCount = gunshotParticles.particleCount;

        // Check if new particles have been emitted
        if (currentParticleCount > previousParticleCount)
        {
            PlayGunshotSound();
        }

        // Update the previousParticleCount for the next frame
        previousParticleCount = currentParticleCount;
    }

    private void PlayGunshotSound()
    {
        if(gunshotAudioSource != null)
        {
            gunshotAudioSource.PlayOneShot(gunshotClip);
        }
    }
}