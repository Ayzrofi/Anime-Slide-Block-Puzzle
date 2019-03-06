using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This Script For Slicing Image From Texture 2d 
public static class ImageSlicer { 

    public static Texture2D[,] GetSlicer(Texture2D img, int BlockPerLine)
    {
        int imageSize = Mathf.Min(img.width, img.height);
        int blockSize = imageSize / BlockPerLine;

        Texture2D[,] blocks = new Texture2D[BlockPerLine, BlockPerLine];
        for (int y = 0; y < BlockPerLine; y++)
        {
            for (int x = 0; x < BlockPerLine; x++)
            {
                Texture2D block = new Texture2D(blockSize, blockSize);
                block.wrapMode = TextureWrapMode.Clamp;
                block.SetPixels(img.GetPixels(x * blockSize, y * blockSize, blockSize, blockSize));
                block.Apply();
                blocks[x, y] = block;
            }
        }
        return blocks;
    }
}
