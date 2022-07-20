﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public class Platform : MonoBehaviour
{
    public static bool activated = false;

    private SpriteRenderer spriteRenderer;
    public ParticleSystem chalkParticles;

    private Transform animate;
    [SerializeField]
    private Sprite sprite;

    public virtual void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
        chalkParticles = GetComponent<ParticleSystem>();

        // Initialize and position animation
        animate = Instantiate(GameObject.Find("Player").transform.GetChild(0)).transform;
        animate.parent = transform;
        animate.SetPositionAndRotation(transform.position, transform.rotation);
        animate.localScale = Vector3.one;

        // Setup animator
        //Animator animator = animate.gameObject.AddComponent<Animator>();
        //animator.runtimeAnimatorController = Instantiate(GameObject.Find("Player").transform.GetChild(0));
        //animator.keepAnimatorControllerStateOnDisable = true;

        // Initialize and position sprite
        //Transform coke = new GameObject("Sprite").transform;
        //coke.parent = animate;
        //coke.SetPositionAndRotation(animate.position, animate.rotation);
        //coke.localScale = Vector3.one;

        //// Set new sprite and disable old sprite
        spriteRenderer = animate.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite ? sprite : GameObject.Find("GameManager").GetComponent<GlobalVariables>().blank;
        //spriteRenderer = coke.gameObject.AddComponent<SpriteRenderer>();
        //spriteRenderer.sprite = GameObject.Find("GameManager").GetComponent<GlobalVariables>().blank;
    }

    public SpriteRenderer SpriteRenderer
    {
        get { return spriteRenderer; }
        set { spriteRenderer = value; }
    }

    public void DarkenSprite()
    {
        spriteRenderer.color = new Color(0.3f, 0.3f, 0.3f);
    }

    public virtual void ResetSprite()
    {
        spriteRenderer.color = Color.white;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Activate()
    {

    }
}
