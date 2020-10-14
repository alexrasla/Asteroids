using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public GameObject explosionprefab;
    public Sprite sprite;

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "bullet"){

            Destroy(collision.gameObject); 

            GameController.numAsteroidsHit += 1;

            GameObject explosion = (GameObject) Resources.Load("AsteroidExplosion", typeof(GameObject));
            
            AudioClip audio = (AudioClip) Resources.Load("AsteroidExplosionSound");
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.PlayOneShot(audio);
            
            GameObject.Instantiate(explosion, transform.position, transform.rotation);   
            
            GameObject asteroid = new GameObject();
            asteroid.tag = "asteroid";
            asteroid.name = "Asteroid";

            scoreScript.score += 100;

            int scale = (int) transform.localScale.x / 2;
            asteroid.transform.localScale = new Vector3(scale, scale, 0);
            CircleCollider2D collider = asteroid.AddComponent<CircleCollider2D>();
            collider.radius = 1;
           
            SpriteRenderer renderer = asteroid.AddComponent<SpriteRenderer>();
            renderer.sprite = (Sprite) Resources.Load("Asteroid", typeof(Sprite));

            asteroid.AddComponent<Asteroid>();

            Rigidbody2D rb = asteroid.AddComponent<Rigidbody2D>();
            rb.mass = 0;
                   
            asteroid.transform.position = transform.position;
            rb.velocity = new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(10.0f, 20.0f));

            transform.localScale = new Vector3(scale, scale, 0);
                      
        }
    }
}
