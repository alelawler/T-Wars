using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour {

    Main gameManager;
    BoxCollider2D rTank1Collider;
    BoxCollider2D bTank1Collider;
    BoxCollider2D powerUpCollider;
    GameObject [] powerUps;
    float powerUpTimer;



    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<Main>();
        rTank1Collider = gameManager.rTank1.GetComponent<BoxCollider2D>();
        bTank1Collider = gameManager.bTank1.GetComponent<BoxCollider2D>();
    }

    void PowerUpHP()
    {
   //-----------------------Red Tank----------------------------------
        if (rTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
        {
            if (gameManager.rTank1HP <= 4)
            {
                gameManager.rTank1HP++;
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
   //-----------------------Blue Tank----------------------------------

        if (bTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
        {
            if (gameManager.bTank1HP <= 4)
            {
                gameManager.bTank1HP++;
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);

            }
        }
    }

    void PowerUpDoubleBullet()
    {
        //-----------------------Red Tank----------------------------------

        if (rTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
        {
            gameManager.redDoubleBulletActive = true;
            gameManager.redDoubleBulletCounter = gameManager.redDoubleBulletCounter + 3;
            Destroy(gameObject);
        }

        //-----------------------Blue Tank----------------------------------

        if (bTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
        {
            gameManager.blueDoubleBulletActive = true;
            gameManager.blueDoubleBulletCounter = gameManager.blueDoubleBulletCounter + 3;
            Destroy(gameObject);
        }
      
    }

    void PowerUpPiercingBullet()
    {
        //-----------------------Red Tank----------------------------------

        if (rTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
        {
            gameManager.redPiercingBulletActive = true;
            gameManager.redPiercingBulletCounter = 4;
            Destroy(gameObject);
        }

        //-----------------------Blue Tank----------------------------------

        if (bTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
        {
            gameManager.bluePiercingBulletActive = true;
            gameManager.bluePiercingBulletCounter = 4;
            Destroy(gameObject);
        }

    }

    void PowerUpSpeed()
    {
        //-----------------------Red Tank-----------------------------------
        if (rTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
        {
            gameManager.redSpeedActive = true;
            Destroy(gameObject);
        }

        //-----------------------Blue Tank-----------------------------------

        if (bTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
        {
            gameManager.blueSpeedActive = true;
            Destroy(gameObject);
        }

    }

    void PowerUpRotationSpeed()
    {
        //-----------------------Red Tank-----------------------------------
        if (rTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
        {
            gameManager.redRotationSpeedActive = true;
            Destroy(gameObject);
        }

        //-----------------------Blue Tank-----------------------------------

        if (bTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
        {
            gameManager.blueRotationSpeedActive = true;
            Destroy(gameObject);
        }        
    }

    //void PowerUpRandom()
    //{
    //    //-----------------------Red Tank-----------------------------------
    //    if (rTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
    //    {
    //        gameManager. = true;
    //        Destroy(gameObject);
    //    }

    //    //-----------------------Blue Tank-----------------------------------

    //    if (bTank1Collider.bounds.Intersects(GetComponent<BoxCollider2D>().bounds))
    //    {
    //        gameManager. = true;
    //        Destroy(gameObject);
    //    }
    //}

    // Update is called once per frame
    void Update () {

            if (gameObject.tag == "Power Up - HP")
            {
                PowerUpHP();
            }

            if (gameObject.tag == "Power Up - Random")
            {

            }

            if (gameObject.tag == "Power Up - Rotation Speed")
            {
                PowerUpRotationSpeed();
            }

            if (gameObject.tag == "Power Up - Speed")
            {
                PowerUpSpeed();

            }

            if (gameObject.tag == "Power Up - Double Bullet")
            {
                PowerUpDoubleBullet();
            }

            if (gameObject.tag == "Power Up - Piercing Bullet")
            {
                PowerUpPiercingBullet();
            }
        if (powerUpTimer > 20)
        {
            Destroy(gameObject);
        }
            powerUpTimer += Time.deltaTime;
    }
}
