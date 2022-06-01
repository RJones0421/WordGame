using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public int letterVal;
	
	public ScriptableObject[] letterObjectArray;
	public ScriptableObject letterObject;

    public SpriteRenderer SpriteRenderer {
        get {
            if (spriteRenderer == null) {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return spriteRenderer;
        }
        set {
            spriteRenderer = value;
        }
    }

    public Sprite[] SpriteArray {
        get {
            return spriteArray;
        }
    }

    /* storing letter value */
    public int LetterValue {
        get {
            return letterVal;
        }
        set {
            letterVal = value;
        }
    }
	
	public ScriptableObject[] LetterObjectArray {
        get {
            return letterObjectArray;
        }
    }
	
	public ScriptableObject LetterObject {
        get {
            return letterObject;
        }
		set {
            letterObject = value;
        }
    }
}
