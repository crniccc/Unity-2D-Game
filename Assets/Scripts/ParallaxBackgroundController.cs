using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ParallaxBackgroundController class that inherits from MonoBehaviour, used for managing the parallax background effect in the game
public class ParallaxBackgroundController : MonoBehaviour
{
    // Public field for the background movement speed
    public float backgroundSpeed;

    // Public renderers for different background layers
    public Renderer backgroundRenderer2;
    public Renderer backgroundRenderer3;
    public Renderer backgroundRenderer4;
    public Renderer backgroundRenderer5;

    // Update method is called once per frame
    void Update()
    {
        // Moving the texture of the second background layer, divided by 32 to achieve a slower effect
        backgroundRenderer2.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime / 32, 0f);
        // Moving the texture of the third background layer, divided by 16 to achieve a somewhat faster effect
        backgroundRenderer3.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime / 16, 0f);
        // Moving the texture of the fourth background layer, divided by 8 to achieve an even faster effect
        backgroundRenderer4.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime / 8, 0f);
        // Moving the texture of the fifth background layer, with no division to achieve the fastest effect
        backgroundRenderer5.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0f);
    }
}
