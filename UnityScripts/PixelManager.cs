using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
// Event for color change


public class PixelManager : MonoBehaviour
{
    public static UnityEngine.Events.UnityAction<Color, Color> ColorChangeEvent;
    // Array of children objects
    public static Color primaryColor = Color.black;
    public static Color secondaryColor = Color.white;
    public GameObject pixelPrefab;
    
    private PixelGrid pixelGrid;
    // Start is called before the first frame update

    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        Destroy(go);
    }
    private void OnValidate()
    {
        pixelGrid = GetComponent<PixelGrid>();
        var amount = pixelGrid.columns * pixelGrid.rows;
        // delete all children
        if (isActiveAndEnabled)
        {
            foreach (Transform child in transform)
            {
                StartCoroutine(Destroy(child.gameObject));
            }
        
            for (int i = transform.childCount; i < amount; i++)
            {
                var go = Instantiate(pixelPrefab, transform);
            }
        }
        

    }

    public static (Color, Color) GetColors()
    {
        return (primaryColor, secondaryColor);
    }

    public void GetImageData()
    {
        // Convert the array into an svg image
        bool [] filledPixel = new bool[pixelGrid.columns * pixelGrid.rows];
        byte[] flag = new byte[32];
        // Set the filled pixels to true
        for (int i = 0; i < pixelGrid.columns * pixelGrid.rows; i++)
        {
            bool isFilled =pixelGrid.transform.GetChild(i).GetComponent<PixelBehaviour>().isFilled;
            filledPixel[i] = isFilled;
            // Set the bit in flag for that position
            if (isFilled)
            {
                flag[i / 8] |= (byte)(1 << (i % 8));
            }
        }

        string svg =
            $"<svg width=\"512px\" height=\"512px\" viewBox=\"0 0 512 512\" xmlns=\"http://www.w3.org/2000/svg\">";
        // Use the array to create a base64 encoded SVG image
        for (int i = 0; i < pixelGrid.columns * pixelGrid.rows; i++)
        {
          int x = i % pixelGrid.columns;
          int y = i / pixelGrid.columns;
          // add padding and spacing
          x = x * 26 + 5;
          y = y * 26 + 5;
            if (filledPixel[i])
            {
                svg += $"<rect x=\"{x}\" y=\"{y}\" width=\"26\" height=\"26\" stroke=\"black\" fill=\"black\" stroke-width=\"{1}\"/>";
            }
        }

        svg += "</svg>";
        // encode base64
        string svgBase64Encoded = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(svg));
        Debug.Log(svgBase64Encoded);
        Debug.Log(BitConverter.ToString(flag));
        // Convert flag to BigInt
        BigInteger big = new BigInteger(flag);
        Debug.Log(big.ToString());

        //return data;
    }

 


    public void UpdateColors(Color primary, Color secondary)
    {
        primaryColor = primary;
        secondaryColor = secondary;
        // Call event to update color
        ColorChangeEvent?.Invoke(primary, secondary);
    }
}


