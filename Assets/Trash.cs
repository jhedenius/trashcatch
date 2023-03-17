using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Trash : MonoBehaviour
{

    public bool falling = true;
    
    void OnCollisionEnter2D(Collision2D collision) => Log(collision);
    void OnCollisionStay2D(Collision2D collision) => Log(collision);
    void OnCollisionExit2D(Collision2D collision) => Log(collision);

    void Log(Collision2D collision, [CallerMemberName] string message = null)
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
