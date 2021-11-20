using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Represents the SoundManager of a scene. Each scene is expected to have exactly 1 SoundManager.
///
/// Each SoundManager has a root background music and a stack of additional background musics.
/// When the stack is empty (which is usually the case), the root background music will be played.
/// But, Commands can push and pop other music onto the stack. When the stack is not
/// empty, the music at the top of the stack (the most recently pushed) will be played.
/// This class also has the method PlaySoundEffect that will simply play the given sound on top of the music.
///
/// This stack mechanism can be used for characters like Muriance and Clavis that replace the background 
/// music of the current scene with their own music. The general usage for that should be:
/// <list>
///     <item>Push 'Clavis music' using SoundManager.PushBackgroundMusic(clavisSound)</item>
///     <item>Let Clavis enter the scene</item>
///     <item>Talk with Clavis...</item>
///     <item>Let Clavis leave the scene</item>
///     <item>Pop the music stack using SoundManager.PopBackgroundMusic()</item>
/// </list>
/// </summary>
public class SoundManager : MonoBehaviour 
{
    // This needs to be static because the current SoundManager needs to be accessible for commands
    private static SoundManager instance;

    // These need to be static to handle scene transitions between scenes with the same background sound
    private static AudioClip currentBackgroundMusic;
    private static AudioSource staticBackgroundAudioSource;
    
    // I think this should be static in case we need to handle 'music overrides' in dialogues that take place in more
    // than 1 scene
    private static Stack<AudioClip> backgroundMusicStack = new Stack<AudioClip>();

    /// <summary>
    ///     Pushes a new music on top of the background music stack. The given music will be played right away and
    ///     stop the current music until PopBackgroundMusic() is called.
    /// </summary>
    public static void PushBackgroundMusic(AudioClip music)
    {
        backgroundMusicStack.Push(music);
        DontDestroyOnLoad(music);
        instance.PlayCurrentBackgroundMusic();
    }

    /// <summary>
    ///     Pops the music on top of the background music stack. This will stop the music on top of the stack and
    ///     start playing the music right below the top (or the root background music if there is only 1 music on the
    ///     background music stack).
    /// </summary>
    public static void PopBackgroundMusic()
    {
        backgroundMusicStack.Pop();
        instance.PlayCurrentBackgroundMusic();
    }

    /// <summary>
    ///     Plays the given sound effect on top of the current background music.
    /// </summary>
    public static void PlaySoundEffect(AudioClip sound)
    {
        instance.effectAudioSource.PlayOneShot(sound);
    }

    [SerializeField]
    AudioClip rootBackgroundMusic;

    [SerializeField]
    AudioSource backgroundAudioSource;
    
    [SerializeField]
    AudioSource effectAudioSource;
    
    private void Awake()
    {
        if (staticBackgroundAudioSource == null) {
            this.backgroundAudioSource.transform.parent = null;
            DontDestroyOnLoad(this.backgroundAudioSource);
            staticBackgroundAudioSource = this.backgroundAudioSource;
        }
        instance = this;
        PlayCurrentBackgroundMusic();
    }

    private AudioClip DetermineNextBackgroundMusic()
    {
        if (backgroundMusicStack.Count > 0) {
            return backgroundMusicStack.Peek();
        } else if (rootBackgroundMusic != null) {
            return rootBackgroundMusic;
        } else {
            return null;
        }
    }

    private bool AreAudioClipsEqual(AudioClip a, AudioClip b)
    {
        if (a == null) return b == null;
        if (b == null) return false;

        return a.name == b.name;
    }

    private void PlayCurrentBackgroundMusic() 
    {
        AudioClip nextBackgroundMusic = DetermineNextBackgroundMusic();
        if (!AreAudioClipsEqual(currentBackgroundMusic, nextBackgroundMusic)) {
            if (currentBackgroundMusic != null) {
                staticBackgroundAudioSource.clip = null;
                staticBackgroundAudioSource.Stop();
            }
            if (nextBackgroundMusic != null) {
                staticBackgroundAudioSource.clip = nextBackgroundMusic;
                staticBackgroundAudioSource.Play();
                DontDestroyOnLoad(nextBackgroundMusic);
            }
            currentBackgroundMusic = nextBackgroundMusic;
        }
    }
}
