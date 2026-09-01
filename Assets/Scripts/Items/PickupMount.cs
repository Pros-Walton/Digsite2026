using UnityEngine;
using System.Collections;

public class PickupMount : MonoBehaviour
{
    public Pickup pickup = null;
    public AudioSource sound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Mount(Pickup pickUp)
    {
        sound = GetComponent<AudioSource>();
        pickup = pickUp;
        pickup.applyBack();
    }

    public void Sound()
    {
        // StartCoroutine(player());
        sound.Play(0);
    }

    IEnumerator player()
    {
        sound.Play(0);
        Debug.Log (sound.clip.length);
        yield return new WaitForSeconds(0.45f);
        Debug.Log("Play!");
    }
}
