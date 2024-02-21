using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackController : MonoBehaviour {
    public TextMeshProUGUI title;
    public Image poster;
    public TextMeshProUGUI year;
    public Material cubeMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPosterImage(Texture2D texture)
    {
        // Create a Sprite from the texture
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        // Apply the texture to the cube's material
        cubeMaterial.mainTexture = texture;
    }
    
    
}