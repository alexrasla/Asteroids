using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private GameObject ship;
   
    public static int lives = 3;
    public static GameObject[] lifeIcon;

    public GameObject levelCleared;
    public float respawnTime;

    private float timeSinceDied;
    // private float respawnTime;

    public static int numAsteroidsHit = 0;

    public Sprite ship_texture;
    public Sprite asteroid_texture;
    private float time = 0.0f;
    public float interpolationPeriod = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnShip();

        respawnTime = 3.0f;

        lifeIcon = new GameObject[3];

        lifeIcon[0] = GameObject.Find("LivesIcon1");
        lifeIcon[1] = GameObject.Find("LivesIcon2");
        lifeIcon[2] = GameObject.Find("LivesIcon3");

    }

    void SpawnShip(){
       
        ship = new GameObject();
        ship.name = "Spaceship";
        ship.AddComponent<SpriteRenderer>().sprite = ship_texture;
        ship.AddComponent<Rigidbody2D>();
        ship.AddComponent<CircleCollider2D>();
        ship.AddComponent<AudioSource>();
        ship.AddComponent<Movement>();

    }

    // Update is called once per frame
    void Update()
    {
        // respawnTime = Time.time + 2f;

        if(lives == 0){
            SceneManager.LoadScene("GameOver");
        }

        if(ship == null){
            
            timeSinceDied += Time.deltaTime;
            
            if(timeSinceDied >= respawnTime){
                // Debug.Log(timeSinceDied);
                // Debug.Log("RESPawn" + respawnTime);
                timeSinceDied = 0.0f;
                Destroy(lifeIcon[lives - 1]);
                lives -= 1;
                SpawnShip();
            }
   
        }

        if(numAsteroidsHit == 5){ //level up
            levelCleared.SetActive(true);
            StartCoroutine(Pause());
            interpolationPeriod /= 2;
        }
        // Debug.Log(interpolationPeriod);
        UpdateAsteroids();
    }

    void UpdateAsteroids(){
        // Debug.Log(numAsteroidsHit);
        time += Time.deltaTime;
 
        if (time >= interpolationPeriod) {
            time = 0.0f;

            GameObject asteroid = new GameObject();
            asteroid.tag = "asteroid";
            asteroid.name = "Asteroid";

            int scale = Random.Range(1, 4);
            asteroid.transform.localScale = new Vector3(scale, scale, 0);
            CircleCollider2D collider = asteroid.AddComponent<CircleCollider2D>();
            collider.radius = 1;
           
            SpriteRenderer renderer = asteroid.AddComponent<SpriteRenderer>();
            renderer.sprite = asteroid_texture;

            asteroid.AddComponent<Asteroid>();

            Rigidbody2D rb = asteroid.AddComponent<Rigidbody2D>();
            rb.mass = 0;
           
           
            int inital = Random.Range(1, 5);
            // Debug.Log(inital);

            if(inital == 1){
                asteroid.transform.position = new Vector3(Random.Range(-38.0f, 38.0f), -22, 0);
                rb.velocity = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(10.0f, 20.0f));
            } else if(inital == 2){
                asteroid.transform.position = new Vector3(Random.Range(-38.0f, 38.0f), 22, 0);
                rb.velocity = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-20.0f, -10.0f));
            } else if(inital == 3){
                asteroid.transform.position = new Vector3(Random.Range(-22.0f, 22.0f), 38, 0);
                rb.velocity = new Vector2(Random.Range(-20.0f, -10.0f), Random.Range(-5.0f, 5.0f));
            } else if(inital == 4){
                asteroid.transform.position = new Vector3(Random.Range(-22.0f, 22.0f), -38, 0);
                rb.velocity = new Vector2(Random.Range(10.0f, 20.0f), Random.Range(-5.0f, 5.0f));
            }
            

        } 
    }

    IEnumerator Pause(){
        GameController.numAsteroidsHit = 0;
        yield return new WaitForSeconds(3f);
        levelCleared.SetActive(false);
        
        // Debug.Log()
        // 
        
    }
    // IEnumerator ShipPause(){
    //     yield return new WaitForSeconds(3f);
    //     SpawnShip
    // }
}
