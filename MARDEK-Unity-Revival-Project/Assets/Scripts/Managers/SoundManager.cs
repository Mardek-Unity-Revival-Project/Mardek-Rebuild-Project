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

    // This needs to be static to handle scene transitions between scenes with the same background sound
    private static AudioSource currentBackgroundMusic;
    
    // I think this should be static in case we need to handle 'music overrides' in dialogues that take place in more
    // than 1 scene
    private static List<AudioSource> backgroundMusicStack = new List<AudioSource>();

    /// <summary>
    ///     Pushes a new music on top of the background music stack. The given music will be played right away and
    ///     stop the current music until PopBackgroundMusic() is called.
    /// </summary>
    public static void PushBackgroundMusic(AudioSource music)
    {
        backgroundMusicStack.Add(music);
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
        backgroundMusicStack.RemoveAt(backgroundMusicStack.Count - 1);
        instance.PlayCurrentBackgroundMusic();
    }

    /// <summary>
    ///     Plays the given sound effect on top of the current background music.
    /// </summary>
    public static void PlaySoundEffect(AudioSource sound)
    {
        sound.Play();
    }

    public AudioSource rootBackgroundMusic;
    
    private void Awake()
    {
        instance = this;
        PlayCurrentBackgroundMusic();
    }

    private AudioSource DetermineNextBackgroundMusic()
    {
        if (backgroundMusicStack.Count > 0) {
            return backgroundMusicStack[backgroundMusicStack.Count - 1];
        } else if (rootBackgroundMusic != null) {
            return rootBackgroundMusic;
        } else {
            return null;
        }
    }

    private bool AreAudioSourcesEqual(AudioSource a, AudioSource b)
    {
        if (a == null) return b == null;
        if (b == null) return false;

        return a.clip.name == b.clip.name;
    }

    private void PlayCurrentBackgroundMusic() 
    {
        AudioSource nextBackgroundMusic = DetermineNextBackgroundMusic();
        if (!AreAudioSourcesEqual(currentBackgroundMusic, nextBackgroundMusic)) {
            if (currentBackgroundMusic != null) {
                currentBackgroundMusic.Stop();
            }
            if (nextBackgroundMusic != null) {
                nextBackgroundMusic.Play();
                DontDestroyOnLoad(nextBackgroundMusic);
            }
            currentBackgroundMusic = nextBackgroundMusic;
        }
    }
}
