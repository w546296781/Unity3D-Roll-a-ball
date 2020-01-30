using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Diagnostics;

public class PlayerController : MonoBehaviour
{

    public float speed;

    public Text countText;
    public Text winText;

    private Rigidbody rb;

    private int count;
    private Stopwatch stw = new Stopwatch();
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        stw.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count == 12)
        {
            stw.Stop();
            winText.text = "You Win!\nFinish Time: " + stw.Elapsed.Seconds.ToString() + "s";
        }
    }
}