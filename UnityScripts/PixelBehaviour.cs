using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;
    public bool isFilled = false;
    void Start()
    {
        
        image = GetComponent<Image>();
     PixelManager.ColorChangeEvent += ChangeColor;   
     // On click event
     GetComponent<Button>().onClick.AddListener(() => {
         isFilled = !isFilled;
         var colors = PixelManager.GetColors();
         ChangeColor(colors.Item1, colors.Item2);
     });
     
    }

    private void ChangeColor(Color arg0, Color arg1)
    {
        image.color = isFilled? arg0:arg1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
