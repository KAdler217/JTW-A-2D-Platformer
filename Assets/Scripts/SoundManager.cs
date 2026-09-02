using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioSource audioSrc;
    public static AudioClip pickCoin;
    public static AudioClip Hit;
    public static AudioClip Attack;
    public static AudioClip Jump;
    public static AudioClip Recovery;
    public static AudioClip Shot;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        pickCoin = Resources.Load<AudioClip>("PickCoin");
        Hit = Resources.Load<AudioClip>("Hit");
        Attack = Resources.Load<AudioClip>("Attack");
        Jump = Resources.Load<AudioClip>("Jump");
        Recovery = Resources.Load<AudioClip>("Recovery");
        Shot = Resources.Load<AudioClip>("Shot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlayerPickCoin()
    {
        audioSrc.PlayOneShot(pickCoin);
    }
    public static void PlayerJump()
    {
        audioSrc.PlayOneShot(Jump);
    }
    public static void PlayerRecovery()
    {
        audioSrc.PlayOneShot(Recovery);
    }
    public static void PlayerAttack()
    {
        audioSrc.PlayOneShot(Attack);
    }
    public static void PlayerHit()
    {
        audioSrc.PlayOneShot(Hit);
    }
    public static void PlayerShot()
    {
        audioSrc.PlayOneShot(Shot);
    }
}
