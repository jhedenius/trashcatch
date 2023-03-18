using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Trash : MonoBehaviour
{

    public bool falling = true;
    public float RotateSpeedY = 0.01f;
    public float RotateSpeedX = 0.01f;
    public float RotateSpeedZ = 0.01f;

    private void OnEnable()
    {
        RotateSpeedY *= (1.0f - (2.0f*Random.value));
        RotateSpeedX *= (1.0f - (2.0f*Random.value));
        RotateSpeedZ *= (1.0f - (2.0f*Random.value));
    }

    private void Update()
    {
        transform.Rotate(RotateSpeedX, RotateSpeedY, RotateSpeedZ, Space.World);
    }

    void OnCollisionEnter2D(Collision2D collision) => Collide(collision);
    //void OnCollisionStay2D(Collision2D collision) => Collide(collision);
    //void OnCollisionExit2D(Collision2D collision) => Collide(collision);

    void Collide(Collision2D collision, [CallerMemberName] string message = null)
    {
        Debug.Log($"{message} called on {name} because a collision with {collision.collider.name}");
        if (collision.collider.gameObject.GetComponent<Trash>() != null)
        {
            collision.collider.gameObject.SetActive(false);    
            
        }
        if (collision.collider.gameObject.GetComponent<Player>() == null && collision.otherCollider.gameObject.GetComponent<Trash>() != null)
        {
            var trash = collision.otherCollider.gameObject.GetComponent<Trash>();
            if (trash.falling)
            {
                trash.falling = false;
                collision.otherCollider.gameObject.SetActive(false);
                GameLoop.SubtractHP(1);
            }
        }
        if (collision.collider.gameObject.GetComponent<Player>() != null && collision.otherCollider.gameObject.GetComponent<Trash>())
        {
            var trash = collision.otherCollider.gameObject.GetComponent<Trash>();
            if (trash.falling)
            {
                trash.falling = false;
                collision.otherCollider.gameObject.SetActive(false);
                GameLoop.AddPoints(1);
            }
        }
        
    }
}
