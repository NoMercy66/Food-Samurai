using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public int pointValue;
    public ParticleSystem explosionParticle;

    private ClickAndSwipe swiperObject;
    // Start is called before the first frame update
    void Start()
    {
        swiperObject = GameObject.Find("Swiper Object").GetComponent<ClickAndSwipe>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(),ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(),ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    Vector3 RandomForce(){
        return Vector3.up * Random.Range(12,16);
    }
    Vector3 RandomTorque(){
        return new Vector3(Random.Range(-10,10),Random.Range(-10,10),Random.Range(-10,10));
    }
    Vector3 RandomSpawnPos(){
        return new Vector3(Random.Range(-7,7),-2);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Sensor")){
            Destroy(gameObject);
        }
        /*else if(other.CompareTag("Swiper")){
            DestroyTarget();
        }*/
    }
    public void DestroyTarget(){
        if(gameManager.isGameActive){
            if(gameObject.CompareTag("Bad") && gameManager.isGameActive){
                swiperObject.boomSoundEffect.Play();
                gameManager.UpdateLives(-1);
            }
            else if(gameObject.CompareTag("Good")){
                swiperObject.foodSoundEffect.Play();
                gameManager.UpdateScore(pointValue);
            }
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }
    }
}
