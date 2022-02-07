using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Inherit from layout group
public class PixelGrid : LayoutGroup
{

    public int rows = 32;
    public int columns = 32;

    public Vector2 cellSize;
    public Vector2 spacing;
    // Use this for initialization
    
    

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        var rect = rectTransform.rect;
        var width = rect.width;
        var height = rect.height;
        var cellWidth = (width / columns) - (spacing.x ) - (padding.left / (float)columns) - (padding.right / (float)columns);        
        var cellHeight = (height / rows) - (spacing.y) - (padding.top / (float)rows) - (padding.bottom / (float)rows);
        cellSize = new Vector2(cellWidth, cellHeight);
        
        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;
            var child = rectChildren[i];
            // add padding
            
            var xPos =  (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;
            SetChildAlongAxis(child, 0, xPos, cellSize.x);
            SetChildAlongAxis(child, 1, yPos, cellSize.y);
        }

    }

    public override void CalculateLayoutInputVertical()
    {
       //
    }

    public override void SetLayoutHorizontal()
    {
        //
    }

    public override void SetLayoutVertical()
    {
        //
    }
}

