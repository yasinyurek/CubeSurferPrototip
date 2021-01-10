using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject cube;
    public Transform pos;
    GameObject lastgo;
    bool once = false;
    public GameObject plus;
    public GameObject particle;
    int coin;
    public Text coinText;
    public Transform coinPos;
    List<GameObject> coinRef;
    public int cubecount = 1;
    public int point;
    bool Finish = false;
    GameObject lastpoint;
    public ScoreUI scoreUI;
    bool dead = false;
    public GameObject tutorial;
    int speed;
    
    
    void Start()
    {
        coinRef = new List<GameObject>();
        coin = PlayerPrefs.GetInt("coin");

        speed = 0;
       
    }

    private void Update()
    {
        
        if (Input.anyKey)
        {
            speed = 1;
            tutorial.gameObject.SetActive(false);

        }
    }

    private void FixedUpdate()
    {

        if (Finish == false)
        {

       
            rb.velocity = new Vector3(0, -2, 10*speed);
          
            //Klavye 
          
            if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x>2.4f)
            {
                transform.position += new Vector3(-0.1f , 0,0 );

                
            }
            if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 5f)
            {
                transform.position += new Vector3(0.1f, 0, 0);
               
            }

  
            
        }
        else
        {
            rb.velocity = new Vector3(0, -2, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="addcube")
        {
           
            if (once==false)
            {
                once = true;
 
                    Destroy(other.gameObject);
                    AddCube();


            }

        }

        if (other.tag=="coin")
        {

            coinRef.Add(other.gameObject);
            AddCoin() ;
        }
        if (other.tag=="last")
        {
            lastpoint = other.gameObject;
        }
        if (other.tag == "obstacle")
        {
            dead = true;
            
        }

    
    }

    public void OnTriggerStay(Collider other)
    {


        if (other.tag == "fireway")
        {
            StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(0.03f);
            dead = true;
            
        }


    }

    void AddCoin()
    {
        coin++;
        coinText.text = coin.ToString();
        InvokeRepeating("CoinMove", 0, 0.01f);
   
    }
    void CoinMove()
    {
        if (coinRef.Count==0)
        {
            CancelInvoke("CoinMove");
        }
        else
        {

       
        for (int i = 0; i < coinRef.Count; i++)
        {
           
            coinRef[i].transform.position = Vector3.MoveTowards(coinRef[i].transform.position, coinPos.position, 0.2f);
            
        }
           
        }
       
    }
    void AddCube()
    {
       
        transform.position += new Vector3(0, 0.7f, 0);
       lastgo= Instantiate(cube, pos.position, Quaternion.identity,transform);
        lastgo.tag = "cube";
        pos.transform.position += new Vector3(0, -0.6f, 0);
       
        plus.SetActive(true);
        particle.SetActive(true);
        StartCoroutine(PlusAnimation());

        cubecount++;
    }
    public void RemoveCube()
    {
        cubecount--;
        if (cubecount<=0 )
        {
            if (  dead == false)
            {
                Finish = true;
                GetComponent<BoxCollider>().size = new Vector3(0.01f, 0.01f, 0.01f);
                GetComponent<BoxCollider>().center = new Vector3(0.01f, 0.54f, 0.01f);
                

                scoreUI.ShowScore(point, coin * point);

            }
            else
            {
                Finish = true;
                scoreUI.ShowLose();
            }
           

        }
        dead = false;
    }

    IEnumerator PlusAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        once = false;
        yield return new WaitForSeconds(0.1f);
        plus.SetActive(false);
        particle.SetActive(false);
        

    }
}
