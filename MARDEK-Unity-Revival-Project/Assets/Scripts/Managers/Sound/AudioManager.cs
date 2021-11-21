using UnityEngine;
using System.Collections.Generic;
using System;

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
public class AudioManager : MonoBehaviour 
{
    // This needs to be static because the current SoundManager needs to be accessible for commands
    private static AudioManager instance;
    private Stack<Music> musicStack = new Stack<Music>();
    Music currentMusic = null;

    [SerializeField] protected Music defaultMusic;
    [SerializeField] AudioSource musicAudioSource;    
    [SerializeField] AudioSource effectAudioSource;

    private void Awake()
    {
        if(instance != null)
        {
            instance.defaultMusic = defaultMusic;
            Destroy(gameObject); // get rid of this spare manager we don't need;
        }
        else
        {
            instance = this;
            gameObject.transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        instance.UpdateCurrentMusic();
    }
    
    private void UpdateCurrentMusic()
    {
        Music desiredMusic = null;
        if(musicStack.Count > 0)
            desiredMusic = musicStack.Peek();
        if (desiredMusic == null)
            desiredMusic = defaultMusic;
        if(currentMusic != desiredMusic)
        {
            currentMusic = desiredMusic;
            currentMusic.PlayOnSource(musicAudioSource);
        }
    }
    
    /// <summary>
    ///     Pushes a new music on top of the background music stack. The given music will be played right away and
    ///     stop the current music until PopBackgroundMusic() is called.
    /// </summary>
    public static void PushMusic(Music music)
    {
        instance.musicStack.Push(music);
        instance.UpdateCurrentMusic();
    }

    /// <summary>
    ///     Pops the music on top of the background music stack. This will stop the music on top of the stack and
    ///     start playing the music right below the top (or the root background music if there is only 1 music on the
    ///     background music stack).
    /// </summary>
    public static void PopMusic()
    {
        instance.musicStack.Pop();
        instance.UpdateCurrentMusic();
    }

    /// <summary>
    ///     Plays the given sound effect on top of the current background music.
    /// </summary>
    public static void PlaySoundEffect(AudioObject audio)
    {
        audio.PlayOnSource(instance.effectAudioSource);
    }
}