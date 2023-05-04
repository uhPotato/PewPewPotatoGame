using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    
    public AudioClip[] possibleClips;

    [Range(.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float volume;
    
    [HideInInspector] public AudioSource source;

    public void Init(AudioSource s)
    {
        source = s;
        source.clip = GetRandomClip();
        source.pitch = pitch;
        source.volume = volume;
    }

    public void Play()
    {
        source.clip = GetRandomClip();
        source.Play();
    }
    
    private AudioClip GetRandomClip() => 
        possibleClips[Random.Range(0, possibleClips.Length)];
}
