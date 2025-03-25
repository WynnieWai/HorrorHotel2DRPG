using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArea : MonoBehaviour
{
    public GameObject swordPrefab;
  

    void OnTriggerEnter2D(Collider2D collision)
    {
           if (collision.CompareTag("MovableObject") && !GameManager.instance.hasSword)
        {
            Instantiate(swordPrefab, transform.position, Quaternion.identity);
            collision.GetComponent<MovableObject>().canMove = false;
            GameManager.instance.hasSword = true;
                    if (GameManager.instance != null)
            {
                GameManager.instance.AddXP(20);
            }
            GameManager.instance.SavePlayerStats();
        }

 
    }
}
