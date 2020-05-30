using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource drop;
    public AudioSource rotate;
    public AudioSource lineClear;
    public AudioSource leftSwipe;
    public AudioSource rightSwipe;
    public AudioSource gameLosse;
    public GameObject anotherMaster;
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
    public void ChangeMode()
    {
        if(this.gameObject.activeSelf == true)
        {
            this.gameObject.SetActive(false);
            anotherMaster.SetActive(true);
        }
    }
}
