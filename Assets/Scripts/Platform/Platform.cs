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
        animate = Instantiate(GameObject.Find("Player").transform.GetChild(0)).transform;
        animate.parent = transform;
        animate.SetPositionAndRotation(transform.position, transform.rotation);
        animate.localScale = Vector3.one;

        // Set new sprite and disable old sprite
        spriteRenderer = animate.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite ? sprite : GameObject.Find("GameManager").GetComponent<GlobalVariables>().blank;

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

    public virtual void Activate()
    {

    }
}
