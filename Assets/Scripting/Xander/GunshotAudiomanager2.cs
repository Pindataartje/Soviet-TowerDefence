using UnityEngine;

public class GunshotAudiomanager : MonoBehaviour
{
    public ParticleSystem gunshotParticles;
    public ParticleSystem additionalParticles; // The additional particle system to activate
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
            ActivateAdditionalParticles(); // Activate the additional particle system
        }

        // Update the previousParticleCount for the next frame
        previousParticleCount = currentParticleCount;
    }

    private void PlayGunshotSound()
    {
        gunshotAudioSource.PlayOneShot(gunshotClip);
    }

    private void ActivateAdditionalParticles()
    {
        if (additionalParticles != null)
        {
            // Ensure the additional particle system plays when the main gunshot particles fire
            additionalParticles.Play();
        }
    }
}