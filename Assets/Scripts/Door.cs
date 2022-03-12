using UnityEngine;

public class Door : MonoBehaviour
{
    public float timeToOpen = 1;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource aud;

    public float DecreaseTimeToOpen()
    {
        return timeToOpen--;
    }

    private void Update()
    {
        if (timeToOpen == 0)
        {
            anim.SetBool("Open", true);
            aud.Play();
            timeToOpen--;
        }
    }
}