using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Main : MonoBehaviour {

    //---------Red Tank 1--------------
    public GameObject rTank1;
    public float speedR;
    public int rotationSpeedR;
    Vector3 directionR;
    public int rTank1HP = 3;
    Vector3 rTank1InitialPosition;
    Quaternion rTank1InitialRotation;

    //----------Blue Tank 1------------
    public GameObject bTank1;
    public float speedB;
    public int rotationSpeedB;
    Vector3 directionB;
    public int bTank1HP = 3;
    Vector3 bTank1InitialPosition;
    Quaternion bTank1InitialRotation;

    //--------------Bricks--------------
    public GameObject[] normalBricks;
    public GameObject[] metalBricks;
    int brickColisionCounter;
    float brickTimer = 0;
    float brickReset = 0;

    //--------------Bullets-------------
    public GameObject blueBulletSpawnPosition;
    public GameObject blueSecondBulletSpawnPosition;
    public GameObject redBulletSpawnPosition;
    public GameObject redSecondBulletSpawnPosition;
    public GameObject normalBulletPrefab;
    public GameObject piercingBulletPrefab;
    public int redBulletCounter;
    public int blueBulletCounter;
    float cooldownRedBullet = 0;
    float cooldownBlueBullet = 0;
    public int redPiercingBulletCounter = 0;
    public int bluePiercingBulletCounter = 0;
    public int redDoubleBulletCounter = 0;
    public int blueDoubleBulletCounter = 0;



    //-------------Power-Ups-------------
    public GameObject[] spawnPoints;
    public GameObject[] powerUps;
    float powerUpCD = 0;
    int spawnPointSelector1;
    int spawnPointSelector2;
    int powerUpSelector1;
    int powerUpSelector2;
    public bool redPiercingBulletActive = false;
    public bool bluePiercingBulletActive = false;
    public bool redSpeedActive = false;
    public bool blueSpeedActive = false;
    public bool redRotationSpeedActive = false;
    public bool blueRotationSpeedActive = false;
    public bool redDoubleBulletActive = false;
    public bool blueDoubleBulletActive = false;
    float redSpeedTimer;
    float blueSpeedTimer;
    float redRotationSpeedTimer;
    float blueRotationSpeedTimer;

    //---------------Blink---------------
    public bool blueBlink = false;
    public bool redBlink = false;
    float blueBlinkTimer = 0;
    float redBlinkTimer = 0;

//-----------Stage-------------
    public BoxCollider2D stageArea;
    public AudioClip ShootFX;
    public AudioClip CrashFX;
    public AudioClip GettingShotFX;
    public AudioSource musicSource;

    //------------UI----------------
    public Text redTankHP;
    public Text blueTankHP;
    public GameObject panelRedWin;
    public GameObject panelBlueWin;
    public GameObject panelGameOver;

    // Use this for initialization
    void Start () {
        bTank1InitialPosition = bTank1.transform.position;
        bTank1InitialRotation = bTank1.transform.rotation;
        rTank1InitialPosition = rTank1.transform.position;
        rTank1InitialRotation = rTank1.transform.rotation;

        bTank1.SetActive(false);
        rTank1.SetActive(false);
        musicSource = GameObject.Find("BGM").GetComponent<AudioSource>(); 
        StartGame();
    }

    public void MainMenu()
    {
        Destroy(GameObject.Find("BGM"));
        SceneManager.LoadScene(0);
    }


    void MoveTanks()
    {

        //-----------------Red Tank 1 Movement--------------------------------
        Vector3 nextPositionR = rTank1.transform.position + directionR * speedR * Time.deltaTime;

        if (rTank1.activeInHierarchy)
        {
            directionR = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                directionR = rTank1.transform.up;
            }

            if (Input.GetKey(KeyCode.S))
            {
                directionR = -rTank1.transform.up;
            }

            if (Input.GetKey(KeyCode.A))
            {

                rTank1.transform.Rotate(new Vector3(0, 0, rotationSpeedR * Time.deltaTime));

            }

            if (Input.GetKey(KeyCode.D))
            {
                rTank1.transform.Rotate(new Vector3(0, 0, -rotationSpeedR * Time.deltaTime));
            }

            //--------------------Collision with bricks and stage boundries-----------------

            for (int i = 0; i < normalBricks.Length; i++)
            {
                if (normalBricks[i].activeInHierarchy)
                {
                    BoxCollider2D normalBricksCollider = normalBricks[i].GetComponent<BoxCollider2D>();
                    if (normalBricksCollider.bounds.Contains(nextPositionR))
                    {
                        brickColisionCounter++;
                    }

                }
            }

            for (int i = 0; i < metalBricks.Length; i++)
            {
                if (metalBricks[i].activeInHierarchy)
                {
                    BoxCollider2D metalBricksCollider = metalBricks[i].GetComponent<BoxCollider2D>();

                    if (metalBricksCollider.bounds.Contains(nextPositionR))
                    {
                        brickColisionCounter++;
                    }
                }

            }

            if ((stageArea.bounds.Contains(nextPositionR)) && brickColisionCounter == 0)
            {
                rTank1.transform.position += directionR * speedR * Time.deltaTime;
            }
            brickColisionCounter = 0;
        }

        //-----------------Blue Tank 1 Movement--------------------------------
        if (bTank1.activeInHierarchy)
        {
            directionB = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                directionB = bTank1.transform.up;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                directionB = -bTank1.transform.up;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                bTank1.transform.Rotate(new Vector3(0, 0, rotationSpeedB * Time.deltaTime));
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                bTank1.transform.Rotate(new Vector3(0, 0, -rotationSpeedB * Time.deltaTime));
            }

//--------------------Collision with bricks and stage boundries-----------------

            Vector3 nextPositionB = bTank1.transform.position + directionB * speedB * Time.deltaTime;

            for (int i = 0; i < normalBricks.Length; i++)
            {
                if (normalBricks[i].activeInHierarchy)
                {
                    BoxCollider2D normalBricksCollider = normalBricks[i].GetComponent<BoxCollider2D>();
                    if (normalBricksCollider.bounds.Contains(nextPositionB))
                    {
                        brickColisionCounter++;
                    }
                }

            }

            for (int i = 0; i < metalBricks.Length; i++)
            {
                if (metalBricks[i].activeInHierarchy)
                {
                    BoxCollider2D metalBricksCollider = metalBricks[i].GetComponent<BoxCollider2D>();
                    if (metalBricksCollider.bounds.Contains(nextPositionB))
                    {
                        brickColisionCounter++;
                    }
                }

            }

            if (stageArea.bounds.Contains(nextPositionB) && brickColisionCounter == 0)
            {
                bTank1.transform.position += directionB * speedB * Time.deltaTime;
            }
            brickColisionCounter = 0;
        }


    }

   public void TankShoots()
    {
        //----------------Red Tank 1 Shoot and Tag bullet----------------

        if (!redPiercingBulletActive)
        {
            if (rTank1.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Space) && redBulletCounter < 2 && cooldownRedBullet > 0.75)
                {
                    AudioSource.PlayClipAtPoint(ShootFX, rTank1.transform.position);
                    GameObject tempRedBullet = Instantiate(normalBulletPrefab, redBulletSpawnPosition.transform.position, rTank1.transform.rotation);
                    tempRedBullet.tag = "Is from red";
                    redBulletCounter++;
                    cooldownRedBullet = 0;
                    if (redDoubleBulletActive && redDoubleBulletCounter >=0)
                    {
                        GameObject tempRedDoubleBullet = Instantiate(normalBulletPrefab, redSecondBulletSpawnPosition.transform.position, rTank1.transform.rotation);
                        tempRedDoubleBullet.tag = "Double from red";
                        redDoubleBulletCounter--;
                        if (redDoubleBulletCounter <= 0)
                        {
                            redDoubleBulletActive = false;
                        }
                    }
                }

                cooldownRedBullet += Time.deltaTime;
            }
        }

        //----------------Blue Tank 1 Shoot and Tag bullet----------------
        
        if (!bluePiercingBulletActive)
        {
            if (bTank1.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.KeypadEnter) && blueBulletCounter < 2 && cooldownBlueBullet > 0.75)
                {
                    AudioSource.PlayClipAtPoint(ShootFX, bTank1.transform.position);
                    GameObject tempBlueBullet = Instantiate(normalBulletPrefab, blueBulletSpawnPosition.transform.position, bTank1.transform.rotation);
                    tempBlueBullet.tag = "Is from blue";
                    blueBulletCounter++;
                    cooldownBlueBullet = 0;
                    if (blueDoubleBulletActive && blueDoubleBulletCounter >= 0)
                    {
                        GameObject tempBlueDoubleBullet = Instantiate(normalBulletPrefab, blueSecondBulletSpawnPosition.transform.position, bTank1.transform.rotation);
                        tempBlueDoubleBullet.tag = "Double from blue";
                        blueDoubleBulletCounter--;
                        if (blueDoubleBulletCounter <= 0)
                        {
                            blueDoubleBulletActive = false;
                        }
                    }
                }
                cooldownBlueBullet += Time.deltaTime;

            }
        }
    }

    void RespawnBricks()
    {
        brickTimer += Time.deltaTime;

        if (brickTimer >= brickReset)
        {
            for (int i = 0; i < normalBricks.Length; i++)
            {
                normalBricks[i].SetActive(true);
                BoxCollider2D normalBricksCollider = normalBricks[i].GetComponent<BoxCollider2D>();
                if (normalBricksCollider.bounds.Contains(rTank1.transform.position) || normalBricksCollider.bounds.Contains(bTank1.transform.position))
                {
                    normalBricks[i].SetActive(false);
                }                
            }
            brickTimer = 0;
            brickReset = Random.Range(20f, 40f);
        }
    }

    void CheckWinCondition()
    {
        //BoxCollider2D rTank1Collider = rTank1.GetComponent<BoxCollider2D>();
        //BoxCollider2D bTank1Collider = bTank1.GetComponent<BoxCollider2D>();

        if (rTank1HP == 0)
        {
            musicSource.volume = 1f;
            rTank1.SetActive(false);
            panelBlueWin.SetActive(true);
        }
        if (bTank1HP == 0)
        {
            musicSource.volume = 1f;
            bTank1.SetActive(false);
            panelRedWin.SetActive(true);
        }
//--------------Game over in case of Crash-----------------------

        //if (rTank1Collider.bounds.Intersects(bTank1Collider.bounds) || bTank1Collider.bounds.Intersects(rTank1Collider.bounds))
        //{
        //    AudioSource.PlayClipAtPoint(CrashFX, rTank1.transform.position);
        //    panelGameOver.SetActive(true);
        //    rTank1.SetActive(false);
        //    bTank1.SetActive(false);
        //}
    }

    void BlinkingRed()
    {
        Renderer redTankRenderer = rTank1.GetComponent<Renderer>();
        redTankRenderer.enabled = !redTankRenderer.enabled;
    }

    void BlinkingBlue()
    {
        Renderer blueTankRenderer = bTank1.GetComponent<Renderer>();
        blueTankRenderer.enabled = !blueTankRenderer.enabled;
    }

    void TankBlinking()
    {
            if (blueBlink) //Bool changed by Bullet Prefab
            {
            blueBlinkTimer = 0;
                InvokeRepeating("BlinkingBlue", 0f, 0.1f);
                blueBlink = false;
                blueBlinkTimer += Time.deltaTime;
            }
            else
            {
                blueBlinkTimer += Time.deltaTime;
            }
        
            if (redBlink)//Bool changed by Bullet Prefab
        {
            redBlinkTimer = 0;
                InvokeRepeating("BlinkingRed", 0f, 0.1f);
                redBlink = false;
                redBlinkTimer += Time.deltaTime;
            }
            else
            {
                redBlinkTimer += Time.deltaTime;
            }

        if (blueBlinkTimer > 1)
        {
            CancelInvoke("BlinkingBlue");
            Renderer blueTankRenderer = bTank1.GetComponent<Renderer>();
            blueTankRenderer.enabled = true;
            blueBlinkTimer = 0;
        }

        if (redBlinkTimer > 1)
        {
            CancelInvoke("BlinkingRed");
            Renderer redTankRenderer = rTank1.GetComponent<Renderer>();
            redTankRenderer.enabled = true;
            redBlinkTimer = 0;
        }

    }
   
    public void StartGame()
    {
        //---------------BGM---------------------
        musicSource.volume=0.2f;
        //---------------Panels---------------------

        panelBlueWin.SetActive(false);
        panelRedWin.SetActive(false);
        panelGameOver.SetActive(false);

        //---------------Blue Tank---------------------
        bTank1.SetActive(true);
        bTank1.transform.position = bTank1InitialPosition;
        bTank1.transform.rotation = bTank1InitialRotation;
        bTank1HP = 3;
        cooldownBlueBullet = 0;
        blueBulletCounter = 0;
        CancelInvoke("BlinkingBlue");
        Renderer blueTankRenderer = bTank1.GetComponent<Renderer>();
        blueTankRenderer.enabled = true;
        blueBlinkTimer = 0;
        bluePiercingBulletActive = false;
        blueSpeedActive = false;
        blueRotationSpeedActive = false;
        blueDoubleBulletActive = false;
        blueSpeedTimer = 0;
        blueRotationSpeedTimer = 0;

        //---------------Red Tank---------------------

        rTank1.SetActive(true);
        rTank1.transform.position = rTank1InitialPosition;
        rTank1.transform.rotation = rTank1InitialRotation;
        rTank1HP = 3;
        cooldownRedBullet = 0;
        redBulletCounter = 0;
        CancelInvoke("BlinkingRed");
        Renderer redTankRenderer = rTank1.GetComponent<Renderer>();
        redTankRenderer.enabled = true;
        redBlinkTimer = 0;
        redPiercingBulletActive = false;
        redSpeedActive = false;
        redRotationSpeedActive = false;
        redDoubleBulletActive = false;
        redSpeedTimer = 0;
        redRotationSpeedTimer = 0;

        powerUpCD = 20;

        //---------------Bricks---------------------

        brickReset = Random.Range(20f, 40f);
        brickTimer = 0;
        for (int i = 0; i < normalBricks.Length; i++)
        {
            normalBricks[i].SetActive(true);
        }

        //---------------PreFabs---------------------

        GameObject[] tempClones = GameObject.FindObjectsOfType<GameObject>();

        for (int i = 0; i < 2; i++)
        {
            if (tempClones[i].name.Contains("Clone"))
            {
                Destroy(tempClones[i]);
            }
        }

    }

    void SpawnPowerUps()
    {
        if (powerUpCD >= 30)
        {
            powerUpSelector1 = Random.Range(0, powerUps.Length);
            powerUpSelector2 = Random.Range(0, powerUps.Length);
            do
            {
                spawnPointSelector1 = Random.Range(0, spawnPoints.Length);
                spawnPointSelector2 = Random.Range(0, spawnPoints.Length);
            }
            while (spawnPointSelector1 == spawnPointSelector2);

            Instantiate(powerUps[powerUpSelector1], spawnPoints[spawnPointSelector1].transform.position, spawnPoints[spawnPointSelector1].transform.rotation);
            Instantiate(powerUps[powerUpSelector2], spawnPoints[spawnPointSelector2].transform.position, spawnPoints[spawnPointSelector2].transform.rotation);
            powerUpCD = 0;        
        }
        else
        {
            powerUpCD += Time.deltaTime;
        }

  }

    void CheckPowerUpActive()
    {
        //---------------------------------------Piercing Bullets-------------------------------------------------

        //-----------------------------Red Tank-----------------------------------
        if (redPiercingBulletActive)
        {
            if (rTank1.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Space) && redBulletCounter < 2 && cooldownRedBullet > 0.75)
                {
                    AudioSource.PlayClipAtPoint(ShootFX, rTank1.transform.position);
                    GameObject tempRedBullet = Instantiate(piercingBulletPrefab, redBulletSpawnPosition.transform.position, rTank1.transform.rotation);
                    tempRedBullet.tag = "Is from red";
                    redBulletCounter++;
                    cooldownRedBullet = 0;
                    redPiercingBulletCounter--;
                    if (redDoubleBulletActive && redDoubleBulletCounter >= 0)
                    {
                        GameObject tempRedDoubleBullet = Instantiate(piercingBulletPrefab, redSecondBulletSpawnPosition.transform.position, rTank1.transform.rotation);
                        tempRedDoubleBullet.tag = "Double from red";
                        redDoubleBulletCounter--;
                        if (redDoubleBulletCounter <= 0)
                        {
                            redDoubleBulletActive = false;
                        }
                    }
                }

                cooldownRedBullet += Time.deltaTime;
                if (redPiercingBulletCounter <= 0)
                {
                    redPiercingBulletActive = false;
                    redPiercingBulletCounter = 0;
                }
            }
        }

        //------------------------------Blue Tank-------------------------------------
        if (bluePiercingBulletActive)
        {
            if (bTank1.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.KeypadEnter) && blueBulletCounter < 2 && cooldownBlueBullet > 0.75)
                {
                    AudioSource.PlayClipAtPoint(ShootFX, bTank1.transform.position);
                    GameObject tempBlueBullet = Instantiate(piercingBulletPrefab, blueBulletSpawnPosition.transform.position, bTank1.transform.rotation);
                    tempBlueBullet.tag = "Is from blue";
                    blueBulletCounter++;
                    cooldownBlueBullet = 0;
                    bluePiercingBulletCounter--;
                    cooldownBlueBullet = 0;
                    if (blueDoubleBulletActive && blueDoubleBulletCounter >= 0)
                    {
                        GameObject tempBlueDoubleBullet = Instantiate(piercingBulletPrefab, blueSecondBulletSpawnPosition.transform.position, bTank1.transform.rotation);
                        tempBlueDoubleBullet.tag = "Double from blue";
                        blueDoubleBulletCounter--;
                        if (blueDoubleBulletCounter <= 0)
                        {
                            blueDoubleBulletActive = false;
                        }
                    }
                }

                cooldownBlueBullet += Time.deltaTime;
                if (bluePiercingBulletCounter <= 0)
                {
                    bluePiercingBulletActive = false;
                    bluePiercingBulletCounter = 0;
                }
            }
        }
        //--------------------------------------------Speed-------------------------------------------------

        //----------------------------------Red Tank----------------------------------------------
        if (redSpeedActive)
        {
            speedR = 3.5f;

            if (redSpeedTimer >= 7.5)
            {
                redSpeedActive = false;
                redSpeedTimer = 0;
                speedR = 3;
            }
            else
            {
                redSpeedTimer += Time.deltaTime;

            }
        }

        //----------------------------------Blue Tank----------------------------------------------
        if (blueSpeedActive)
        {
            speedB = 3.5f;

            if (blueSpeedTimer >= 7.5)
            {
                
                blueSpeedActive = false;
                blueSpeedTimer = 0;
                speedB = 3;
            }
            else
            {
                blueSpeedTimer += Time.deltaTime;
            }
        }
        //--------------------------------------------Rotation Speed-------------------------------------------------

        //-----------------------------------------Red Tank----------------------------------------------------
        if (redRotationSpeedActive)
        {
            rotationSpeedR = 150;

            if (redSpeedTimer >= 7.5)
            {
                redRotationSpeedActive = false;
                redRotationSpeedTimer = 0;
                rotationSpeedR = 125;
            }
            else
            {
                redRotationSpeedTimer += Time.deltaTime;

            }
        }

        //----------------------------------Blue Tank----------------------------------------------
        if (blueRotationSpeedActive)
        {
            rotationSpeedB = 150;

            if (blueSpeedTimer >= 7.5)
            {
                blueRotationSpeedActive = false;
                blueRotationSpeedTimer = 0;
                rotationSpeedB = 125;
            }
            else
            {
                blueRotationSpeedTimer += Time.deltaTime;

            }
        }

        //---------------------------------------Double Bullets in Shoot code-------------------------------------------------

    }
    public void QuitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update() {
        redTankHP.text = "Red Tank HP: " + rTank1HP;
        blueTankHP.text = "Blue Tank HP: " + bTank1HP;
        if (!panelBlueWin.activeInHierarchy && !panelGameOver.activeInHierarchy && !panelRedWin.activeInHierarchy)
        {
            MoveTanks();
            TankShoots();
            RespawnBricks();
            CheckWinCondition();
            TankBlinking();
            SpawnPowerUps();
            CheckPowerUpActive();
        }
    }
}

