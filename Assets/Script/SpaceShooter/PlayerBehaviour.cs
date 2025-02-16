using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerBehaviour : MonoBehaviour
{
    public float Speed;
    public int healthPoints;
    public TextMeshProUGUI healthText;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        healthPoints = 3;
        //gets the transform component and position sub component
        //makes the gameobject move to the new position using Vector 2
        //transform.position = new Vector2(-7,0);
    }
    private void Update()
    {
        healthText.text = healthPoints.ToString();
        if (healthPoints <= 0)
        {
            gameOverScreen.SetActive(true);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveInput = new Vector3(horizontalInput, verticalInput, 0);
        //Time.deltatime = Stores the seconds since the last frame
        transform.position += Time.deltaTime * Speed * moveInput;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            healthPoints--;
        }
        if (collision.CompareTag("PowerUp"))
        {
            healthPoints++;
            Destroy(collision.gameObject);
        }
    }

    public void RestartButton()
    {
        gameOverScreen.SetActive(false);
        healthPoints = 3;
        transform.position = new Vector3(-9,0,0);
    }
}
