using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{   

    private float thrust = 7.0f;
    private float bullet_speed = 40.0f;

    private AudioClip shipThrusters;


    // Start is called before the first frame update
    void Start()
    {
        shipThrusters = (AudioClip) Resources.Load("Thrusters");
    }

    // Update is called once per frame
    void Update()
    {

        checkOnScreen();

        if (Input.GetKey("j")){
            transform.Rotate(0, 0, 5);
        } else if(Input.GetKey("k")){
            GetComponent<Rigidbody2D>().AddForce(new Vector3(-Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad),  0) * thrust);
            StartCoroutine(Thrusters());
        } else if(Input.GetKey("l")){
            transform.Rotate(0, 0, -5);
        } else if(Input.GetKeyDown("space")){
            Shoot();
        }
    }

    IEnumerator Thrusters(){
        GetComponent<AudioSource>().PlayOneShot(shipThrusters);
        yield return new WaitForSeconds(1f);
        GetComponent<AudioSource>().Stop();
    }

    void Shoot(){
    
        GameObject bullet = new GameObject();

        AudioClip bulletAudio = (AudioClip) Resources.Load("Laser");
        AudioSource source = bullet.AddComponent<AudioSource>();
        source.PlayOneShot(bulletAudio);
      
        bullet.tag = "bullet";
        bullet.name = "Bullet";
        SpriteRenderer renderer = bullet.AddComponent<SpriteRenderer>();
        renderer.sprite = (Sprite) Resources.Load("Bullet", typeof(Sprite)) ;

        Rigidbody2D rb = bullet.AddComponent<Rigidbody2D>();
        CircleCollider2D collider = bullet.AddComponent<CircleCollider2D>();
        collider.radius = 0.1f;

        bullet.transform.position = new Vector3(transform.position.x + 3*-Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), transform.position.y + 3*Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), 0);
        bullet.transform.localScale = new Vector3(7, 7 , 0);
        rb.velocity = new Vector2(-Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)) * bullet_speed;

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "asteroid"){
            
            GameObject explosion = (GameObject) Resources.Load("ShipExplosion", typeof(GameObject));
            
            AudioClip audio = (AudioClip) Resources.Load("ShipExplosionSound");
            AudioSource source = collision.gameObject.AddComponent<AudioSource>();
            source.PlayOneShot(audio);

            GameObject.Instantiate(explosion, transform.position, transform.rotation);   

            Destroy(gameObject);
       } 
    }

    void checkOnScreen(){
        if(transform.position.x < -33){
            transform.position = new Vector3(33, transform.position.y, 0);
        } 
        if(transform.position.y < -22){
            transform.position = new Vector3(transform.position.x, 22, 0);
        }
        if(transform.position.x > 33){
            transform.position = new Vector3(-33, transform.position.y, 0);
        } 
        if(transform.position.y > 22){
            transform.position = new Vector3(transform.position.x, -22, 0);
        }
    }

    

}
