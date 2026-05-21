using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip lineClearSound;
    public AudioClip blockLandSound;
    public AudioClip gameOverSound;

    AudioSource[] sources;

    void Awake()
    {
        instance = this;
        sources = GetComponents<AudioSource>();
    }

    public void PlayLineClear()
    {
        sources[0].PlayOneShot(lineClearSound);
    }

    public void PlayBlockLand()
    {
        sources[1].PlayOneShot(blockLandSound);
    }

    public void PlayGameOver()
    {
        sources[2].PlayOneShot(gameOverSound);
    }
}