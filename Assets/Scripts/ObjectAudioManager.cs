using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ObjectAudio
{
    wine,
    cola,
    vitamin,
    laxative,
    air_conditioner,
    dumbbel,
}
public class ObjectAudioManager : MonoBehaviour
{
    public AudioClip[] ObjectClips;
    public AudioSource audioSource;
    public NetworkManager networkManager;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
    /// <summary>
    /// 物品音效
    /// </summary>
    /// <param name="objectAudio"></param>
    public void ObjectAudio(ObjectAudio objectAudio)
    {
        audioSource.PlayOneShot(ObjectClips[objectAudio.GetHashCode()]);
    }
}
