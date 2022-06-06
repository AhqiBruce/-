using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerAudio
{
    eat,
    drink,
    fillwater,
    pee,
}
public enum ReactAudio
{
    Hiccup,
    snack,
    sneeze,
    lollipop,
    mosquito,
    stomachache,
    eatrock,
    earthquake,
    laugh,
    sweat,
    win,
    lemon,
    drunk,
}
public class AudioManager : MonoBehaviour
{
    public AudioClip[] PlayerClips;
    public AudioClip[] ReactClips;
    public AudioSource audioSource;
    public NetworkManager networkManager;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
    public void PlayerAudio(PlayerAudio playerAudio)
    {
        audioSource.PlayOneShot(PlayerClips[playerAudio.GetHashCode()]);
    }
    public void ReactAudio(ReactAudio reactAudio)
    {
        audioSource.PlayOneShot(ReactClips[reactAudio.GetHashCode()]);
    }
}
