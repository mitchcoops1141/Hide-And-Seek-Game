using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 3;

    public bool canFunction = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canFunction)
        {
            float hori = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
            float verti = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;

            transform.position += new Vector3(hori, 0, verti);
        }
    }

    void SeekerCollision()
    {
        //end round


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Seeker")
        {
            SeekerCollision();
        }
    }
}
