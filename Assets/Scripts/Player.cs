using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Player : MonoBehaviour
{
    public Camera cam;

    private Material curMat;
    private Material nextMat;
    private Material telMat;
    private Material tempMat;
    private Material backupMat;

    private Vector3 jump;
    private Vector3 damage;
    private Vector3 inputVector;
    private Vector3 lastPos;

    private bool grounded;
    private bool immune;
    private bool moving;
    private bool destroyed;
    public bool invisible;
    public bool laser;
    public bool speedBoost;
    public string currentLevel;
    public int teleport;
    private float jumpForce = 2.0f;
    private float speed = 4.0f;
    private float timer;
    private float timer2;
    private float timer3;
    private float speedTimer;
    private float waitTime;
    public bool extraJump;
    public int points;
    public int enemiesKilled;

    public int health = 5;
    private int enemyDir;

    public GameObject Tracker;
    public ZoneTracker zScript;

    private Rigidbody rigbod;

    Rigidbody rb;

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().name;

        rigbod = gameObject.GetComponent<Rigidbody>();
        Tracker = GameObject.Find("Tracker");
        zScript = Tracker.GetComponent<ZoneTracker>();

        points = 0;
        cam = UnityEngine.Camera.main;
        enemyDir = 0;
        rb = GetComponent<Rigidbody>();

        curMat = gameObject.GetComponent<Renderer>().material;
        tempMat = curMat;
        nextMat = Resources.Load("Transparent Red", typeof(Material)) as Material;
        telMat = Resources.Load("purple", typeof(Material)) as Material;
        backupMat = telMat;

        timer = 5f;
        timer2 = 0.5f;
        timer3 = 30f;
        speedTimer = 6f;
        enemiesKilled = 0;
        waitTime = 0f;
        teleport = 0;

        jump = new Vector3(0.0f, 2.0f, 0.0f);
        damage = new Vector3(5.0f, 0.0f, 0.0f);
        immune = false;
        invisible = false;
        destroyed = false;
        extraJump = false;
        speedBoost = false;
        laser = false;
    }

    void OnCollisionEnter(Collision col) 
    {
        colCheck(col);                      //called twice for fast projectiles which can otherwise go ignored

        if (col.gameObject.tag == "floor" || col.gameObject.tag == "platform")
            zScript.currentGround = col.transform.root.gameObject;

        if (col.gameObject.tag == "flag")
        {
            if (currentLevel == "Level 1")
                SceneManager.LoadScene("Level 2");
            else if (currentLevel == "Level 2")
                SceneManager.LoadScene("Level 3");
            else if (currentLevel == "Level 3")
                SceneManager.LoadScene("Main Menu");
        }

    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "floor" || col.gameObject.tag == "platform")
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        colCheck(col);
    }

    void drawLine(Vector3 end) 
    {
        Vector3 temp = new Vector3(0f, 0f, -11f);

        GameObject newLine = new GameObject();
        newLine.transform.position = transform.position;
        newLine.AddComponent<LineRenderer>();
        LineRenderer l = newLine.GetComponent<LineRenderer>();

        l.sortingOrder = 0;
        l.startWidth = 0.1f;
        l.endWidth = 0.1f;
        //l.material = new Material(Shader.Find("Sprites/Default"));
        //l.material.color = Color.red;
        l.startColor = Color.red;
        l.endColor = Color.red;
        l.SetPosition(0, transform.position - temp);       //start
        l.SetPosition(1, end - temp);                      //end
        Destroy(newLine, 0.01f);

    }

    void Update()
    {
        
        if (rigbod.velocity.y > 0 || Input.GetKey("s") || Input.GetKey("down")) {
            foreach(GameObject platform in zScript.platforms) 
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), platform.GetComponentInChildren<Collider>(), true);  
        }
        else
        {
            foreach (GameObject platform in zScript.platforms)
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), platform.GetComponentInChildren<Collider>(), false);
        }
    

        if (invisible) 
        {
            gameObject.GetComponent<Renderer>().material = nextMat;
            timer -= Time.deltaTime;

            if (timer <= 0) 
            {
                timer = 5f;
                invisible = false;
                gameObject.GetComponent<Renderer>().material = curMat;
            }
        }

        if (speedBoost)
        {
            speed = 12f;
            speedTimer -= Time.deltaTime;
            if (speedTimer <= 0)
            {
                speedTimer = 6f;
                speedBoost = false;

            }
        }

        if (teleport > 0) 
        {
            timer2 -= Time.deltaTime;

            if (timer2 <= 0) 
            {
                tempMat = telMat;
                telMat = gameObject.GetComponent<Renderer>().material;
                gameObject.GetComponent<Renderer>().material = tempMat;
                timer2 = 0.5f;
            }

            if (Input.GetMouseButtonDown(0)) 
            {
                Ray ray;
                RaycastHit hit;

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (!Physics.Raycast(ray, out hit) || hit.collider.tag == "coin" || hit.collider.tag == "powerup")
                {
                    Vector3 temp = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - cam.transform.position.z));
                    transform.position = new Vector3(temp.x, temp.y, 0f);

                    waitTime = 2f;
                    teleport -= 1;
                    timer2 = 0.5f;
                    telMat = backupMat;
                    gameObject.GetComponent<Renderer>().material = curMat;
                }
            }
        }

        if (laser)
        {
            timer3 -= Time.deltaTime;

            if (timer3 <= 0) 
            {
                timer3 = 30f;
                laser = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 temp = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - cam.transform.position.z));

                RaycastHit hit;
                Ray ray = new Ray(transform.position, new Vector3(temp.x, temp.y, 0f) - transform.position);

                FindObjectOfType<AudioManager>().Play("laser");
                drawLine(new Vector3(temp.x, temp.y, transform.position.z));

                if (Physics.Raycast(ray, out hit)) 
                {
                    Vector3 viewPos = cam.WorldToViewportPoint(hit.transform.position);
                  
                    if (hit.collider.tag == "enemy" && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.z > 0) 
                    {
                        if (hit.collider.name == "Enemy_4") { FindObjectOfType<AudioManager>().Stop("flame"); }
                        else if (hit.collider.name == "Enemy_5") { FindObjectOfType<AudioManager>().Play("pop"); }

                        if (hit.collider.name != "Boss") 
                        {
                            Destroy(hit.transform.gameObject);
                        }
                    }
                }
            }
        }

        if (waitTime > 0f)
        {
            waitTime -= Time.deltaTime;
        }

        if (health <= 0 || transform.position.y <= -10f) 
        {
            SceneManager.LoadScene("Game Over");
        }

        inputVector = new Vector3(Input.GetAxis("Horizontal"), rb.velocity.y, 0);

        if (cam.WorldToScreenPoint(transform.position).x + inputVector.x <= 0 && inputVector.x < 0) 
        {
            inputVector.x = 0;
        }

        transform.LookAt(transform.position + new Vector3(inputVector.x, 0, 0));

        if (Input.GetKeyDown("space"))
        {
            if (grounded)
            {
                FindObjectOfType<AudioManager>().Play("jump");
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                grounded = false;
            }
            else if (extraJump && teleport == 0) 
            {
                FindObjectOfType<AudioManager>().Play("jump");
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);
                extraJump = false;
            }
        }

        if (!speedBoost)
        {
            if (Input.GetKey("right ctrl") || Input.GetKey("left ctrl"))
            {
                speed = 6.0f;
            }
            else
            {
                speed = 4.0f;
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputVector * speed * Time.deltaTime);
    }

    bool hitEnemy(Vector3 dir) 
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, dir);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "enemy" && !destroyed) //destroyed is used to prevent killing enemies below other enemies who are hit by the raycast
            {
                rb.AddForce(jump * jumpForce, ForceMode.Impulse);

                if (hit.collider.name == "Enemy_2" && hit.collider.GetComponent<enemyType2>().invinsible) //special property that makes that particular enemy temporarily invincible 
                {
                    return false;
                }
                else if (hit.collider.name == "Enemy_4") { FindObjectOfType<AudioManager>().Stop("flame"); }
                else if (hit.collider.name == "Enemy_5") { FindObjectOfType<AudioManager>().Play("pop"); }
                else if (hit.collider.name == "Boss")
                {
                    if (hit.collider.GetComponent<Boss>().health > 0)
                    {
                        if (!hit.collider.GetComponent<Boss>().immunity)
                        {
                            FindObjectOfType<AudioManager>().Play("growl");

                            hit.collider.GetComponent<Boss>().health -= 1;
                            hit.collider.GetComponent<Boss>().immunity = true;
                        }

                        //enemiesKilled += 1;
                        return true;
                    }
                }

                FindObjectOfType<AudioManager>().Play("stomp");
                Destroy(hit.transform.gameObject);

                enemiesKilled += 1;
                destroyed = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    void colCheck(Collision col) 
    {
        if (col.gameObject.tag == "enemy")
        {
            if (!hitEnemy(Vector3.down) && !hitEnemy(new Vector3(0.5f, -1, 0)) && !hitEnemy(new Vector3(-0.5f, -1, 0))) //checks to see if player is jumping on enemy
            {
                if (col.transform.position.x > transform.position.x)
                {
                    enemyDir = 1;
                }
                else if (col.transform.position.x < transform.position.x)
                {
                    enemyDir = -1;
                }
                else 
                {
                    enemyDir = 0;
                }

                StartCoroutine(recDamage(enemyDir));
            }

            destroyed = false;
        }
    }

    IEnumerator recDamage(int dir) 
    {
        if (!immune)                //temporary immunity from damage when first hit to prevent losing multiple health from 1 hit
        {
            FindObjectOfType<AudioManager>().Play("damage");
            immune = true;
            takeDMG(1);
        }

        rb.AddForce(-1 * dir * damage, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);
        immune = false;
    }

    private void takeDMG(int dmg) 
    {
        if (waitTime <= 0f) 
        {
            health -= dmg;
        }
    }
}
