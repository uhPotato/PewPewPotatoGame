using System;
using UnityEngine;
using System.Linq;
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get => instance;
        set
        {
            if (instance == null)
                instance = value;
        }
    }
    
    [SerializeField] private Sound[] sounds;
    
    private void Awake()
    {
        Instance = this;
        if(Instance == this)
            DontDestroyOnLoad(gameObject);
        else
        {
            Destroy(gameObject);
            return;
        }
        
        foreach (var sound in sounds) 
            sound.Init(gameObject.AddComponent<AudioSource>());
        
        LoopMusic();
    }

    public Sound Play(string soundName)
    {
        var sound = Array.Find(sounds, s => s.name == soundName);
        if(sound != null)
            sound.Play();
        return sound;
    }

    private void LoopMusic()
    {
        if(sounds.Where((s) => s.name == "Music").Count() == 0) return;
        var sound = Play("Music");
        Invoke(nameof(LoopMusic), sound.source.clip.length);

    }
}
