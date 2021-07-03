using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBullet : MonoBehaviour {

    Main gameManager;
    public float speed;


    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<Main>();
    }

    void PiercingBulletCollision()
    {
        //----------------------Out of Bounds----------------
        if (!gameManager.stageArea.bounds.Contains(transform.position))
        {
            if (gameObject.tag == "Is from red")
            {
                gameManager.redBulletCounter--;
            }
            if (gameObject.tag == "Is from blue")
            {
                gameManager.blueBulletCounter--;
            }
            if (gameObject.tag == "Double from red")
            {
                gameManager.redDoubleBulletCounter--;
            }
            if (gameObject.tag == "Double from blue")
            {
                gameManager.blueDoubleBulletCounter--;
            }
            Destroy(gameObject);

        }

        //----------------------Collision with normal bricks----------------

        for (int i = 0; i < gameManager.normalBricks.Length; i++)
        {
            if (gameManager.normalBricks[i].activeInHierarchy)
            {
                BoxCollider2D normalBricksCollider = gameManager.normalBricks[i].GetComponent<BoxCollider2D>();
                if (normalBricksCollider.bounds.Contains(transform.position))
                {
                    gameManager.normalBricks[i].SetActive(false);

                }
            }

        }

        //----------------------------------Check Collision Tanks--------------
        BoxCollider2D rTank1BoxCollider = gameManager.rTank1.GetComponent<BoxCollider2D>();
        BoxCollider2D bTank1BoxCollider = gameManager.bTank1.GetComponent<BoxCollider2D>();

        if (rTank1BoxCollider.bounds.Contains(transform.position) && (gameObject.tag == "Is from blue"))
        {
            gameManager.CancelInvoke("BlinkingRed");
            AudioSource.PlayClipAtPoint(gameManager.GettingShotFX, rTank1BoxCollider.transform.position);
            gameManager.rTank1HP--;
            gameManager.blueBulletCounter--;
            gameManager.redBlink = true;
            if (gameObject.tag == "Is from blue")
            {
                gameManager.blueBulletCounter--;
            }
            Destroy(gameObject);

        }
        if (bTank1BoxCollider.bounds.Contains(transform.position) && (gameObject.tag == "Is from red"))
        {
            gameManager.CancelInvoke("BlinkingBlue");
            AudioSource.PlayClipAtPoint(gameManager.GettingShotFX, bTank1BoxCollider.transform.position);
            gameManager.bTank1HP--;
            gameManager.redBulletCounter--;
            gameManager.blueBlink = true;
            if (gameObject.tag == "Is from red")
            {
                gameManager.redBulletCounter--;
            }
            Destroy(gameObject);


        }
        //Destroy Object

    }

    // Update is called once per frame
    void Update () {
        if ((gameObject.tag == "Double from red") || (gameObject.tag == "Double from blue"))
        {
            transform.position += transform.up * speed * 0.83f * Time.deltaTime;

        }
        else
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
        PiercingBulletCollision();

    }
}
