using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{

    public float JumpForce;
    public float Score;

    public bool isGrounded = false;
    public bool isAlive = true;

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;

    public Text ScoreText;
    public GameObject RestartBN;

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Score = 0;
        RestartBN.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true)
            {
                Rigidbody2D.AddForce(Vector2.up * JumpForce);
                isGrounded = false;
            }
        }
        CalculateScore();
    }

    private void CalculateScore()
    {
        if (isAlive)
        {
            Score += Time.deltaTime * 4;
            ScoreText.text = "SCORE : " + Score.ToString("F");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            if(isGrounded == false)
            {
                isGrounded = true;
            }
        }
        if (collision.gameObject.CompareTag("spike"))
        {
            isAlive = false;
            Time.timeScale = 0;
            RestartBN.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("CorgiGame");
        Time.timeScale = 1;
    }
}
