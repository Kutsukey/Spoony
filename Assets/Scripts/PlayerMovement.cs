using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float speed = 12;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3;
    [SerializeField] float cooldownTime = 2;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip spoonThrow;
    [SerializeField] AudioClip bSpoonSpawn;
    Vector3 velocity;
    float nextFireTime = 0;
    float nextFireTime2 = 0;
    bool isGrounded;
    bool spoonSpawn = false;
    [SerializeField] GameObject spoon;
    [SerializeField] GameObject bigSpoon;
    GameObject bSpoon;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject spoonSpawnPoint;


    void Update()
    {
        Move();
        FireSpoon();
        ReleaseBigSpoon();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Menu");
        }
    }

    void FireSpoon()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFireTime)
        {
            Instantiate(spoon, transform.position + transform.forward + Vector3.up * .34f, spoon.transform.rotation);
            aud.PlayOneShot(spoonThrow);
            nextFireTime = Time.time + cooldownTime;
        }
    }

    void SpawnParticleSystem()
    {
        particle.Play();
        StartCoroutine(StopParticleSystem(particle, 1));
    }

    IEnumerator<WaitForSeconds> StopParticleSystem(ParticleSystem particle, float time)
    {
        yield return new WaitForSeconds(time);
        particle.Stop();
    }

    void ReleaseBigSpoon()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !spoonSpawn && Time.time > nextFireTime2)
        {
            BigSpoonSpawn();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && spoonSpawn && Time.time > nextFireTime2)
        {
            Destroy(bSpoon);
            BigSpoonSpawn();
        }
    }

    void BigSpoonSpawn()
    {
        bSpoon = Instantiate(bigSpoon, spoonSpawnPoint.transform.position, Quaternion.Euler(-90, transform.rotation.y, 0));
        spoonSpawn = true;
        bSpoon.transform.rotation = Quaternion.Euler(-90, transform.rotation.y, 0);
        nextFireTime2 = Time.time + cooldownTime;
        aud.PlayOneShot(bSpoonSpawn);
        SpawnParticleSystem();
    }

    void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2);
            aud.PlayOneShot(jump);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        bool isMoving = (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f);

        if (isGrounded && !aud.isPlaying && isMoving)
        {
            aud.volume = Random.Range(0.8f, 1);
            aud.pitch = Random.Range(0.8f, 1.1f);
            aud.Play();
        }
    }
}
