using UnityEngine;
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
        animate = new GameObject("Animate").transform;
        animate.parent = transform;
        animate.SetPositionAndRotation(transform.position, transform.rotation);
        animate.localScale = Vector3.one;

        // Setup animator
        Animator animator = animate.gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath("Assets/Animations/Height.controller", typeof(RuntimeAnimatorController)) as RuntimeAnimatorController;
        animator.keepAnimatorControllerStateOnDisable = true;

        // Initialize and position sprite
        Transform coke = new GameObject("Sprite").transform;
        coke.parent = animate;
        coke.SetPositionAndRotation(animate.position, animate.rotation);
        coke.localScale = Vector3.one;

        // Set new sprite and disable old sprite
        spriteRenderer = coke.gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite ? sprite : AssetDatabase.LoadAssetAtPath("Assets/Letters/LetterSprites/blank.jpg", typeof(Sprite)) as Sprite;
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
