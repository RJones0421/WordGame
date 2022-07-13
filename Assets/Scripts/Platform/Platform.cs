using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
    public static bool activated = false;

    private SpriteRenderer spriteRenderer;
    public ParticleSystem chalkParticles;

    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        chalkParticles = GetComponent<ParticleSystem>();
    }

    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return spriteRenderer;
        }
        set
        {
            spriteRenderer = value;
        }
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
