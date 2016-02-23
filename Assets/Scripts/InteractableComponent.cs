using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class MenuButton
{
	public Sprite activeButtonSprite;
    public string buttonText;
    public string functionName;  
};

public class InteractableComponent : MonoBehaviour
{
    [Header("Menu Information")]
    public string menuTitle;
    public string menuDescription;
    public List<MenuButton> menuButtons;

    [Header("Interaction")]
    public Color highlightColor = new Color(1,1,0,1);
    public Color normalColor = new Color(1,1,1,1);

    void Start()
    {
        // Get the starting colour
        normalColor = gameObject.GetComponent<Renderer>().material.color;
        highlightColor.a = 0.2f;
    }

    void OnMouseEnter()
    {
        this.gameObject.GetComponent<Renderer>().material.color = highlightColor;
    }

    void OnMouseExit()
    {
        this.gameObject.GetComponent<Renderer>().material.color = normalColor;
    }
}

