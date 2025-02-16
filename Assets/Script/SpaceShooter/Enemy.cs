using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //How fast the enemy goes up and down
    public float speed;
    //How high the enemy goes up and down
    public float amplitude;

    //Store the initial position of the enemy
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float verticalMovement = Mathf.Sin(speed * Time.time) * amplitude;
        transform.position = new Vector3(initialPosition.x,initialPosition.y + verticalMovement,initialPosition.z);

    }
}
