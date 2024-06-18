using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;
    private bool swiping = false;
    public AudioSource slicingSoundEffect;

    public AudioSource foodSoundEffect;

    public AudioSource boomSoundEffect;


    // Start is called before the first frame update
    void Awake()
    {
        slicingSoundEffect = gameObject.GetComponents<AudioSource>()[0];
        foodSoundEffect = gameObject.GetComponents<AudioSource>()[1];
        boomSoundEffect = gameObject.GetComponents<AudioSource>()[2]; 
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled = false;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive){

            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                slicingSoundEffect.PlayDelayed(0.1f);
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }
            if (swiping)
            {
            UpdateMousePosition();
            }
        } 

    }
    void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }
    void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Target>())
        {
            other.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }


}
