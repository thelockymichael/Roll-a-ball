using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using InControl;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public Text countText;
    public GameObject winText;

    private Rigidbody rb;
    private int count;

    private AudioSource audioSource;

    public AudioClip victoryClip;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.SetActive(false);
    }

    void FixedUpdate()
    {
        var InputDevice = InputManager.ActiveDevice;


        float moveHorizontal = InputDevice.LeftStickX;
        float moveVertical = InputDevice.LeftStickY;
        //float moveHorizontal = Input.GetAxis("Horizontal");
        // float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            //other.gameObject.transform.Find("Exploson6");
            //other.gameObject.transform.GetChild(0);
            // Destroy(other.gameObject.GetChild(0));
            //StartCoroutine(Delay());

           other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            //StartCoroutine("delay");
            //PlayExplosionSound.PlaySound(2);
            other.gameObject.transform.GetChild(2).gameObject.SetActive(true);
           // other.gameObject.transform.GetChild(1).GetComponent<DestroyEffect>().enabled = true;
            count = count + 1;
            SetCountText();
        }
    }

    /*IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
                
    }*/

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            Debug.Log("WINNING");
            audioSource.clip = victoryClip;
            audioSource.Play();

            countText.text = "";
            winText.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}