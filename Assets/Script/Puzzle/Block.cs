using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public event System.Action<Block> OnBlockPressed;
    public event System.Action OnFinishMoving;

    public Vector2Int BlockCoordinate;
    Vector2Int startingCoord;

    public void initial(Vector2Int StartCoor,Texture2D image)
    {
        this.startingCoord = StartCoor;
        BlockCoordinate = StartCoor;
        GetComponent<MeshRenderer>().material = Resources.Load<Material>("Block");
        GetComponent<MeshRenderer>().material.mainTexture = image;
    }
    public void MoveToPositions(Vector2 target, float durasi)
    {
        StartCoroutine(AnimatedMove(target, durasi));
    }

    private void OnMouseDown()
    {
        if (OnBlockPressed != null)
        {
            OnBlockPressed(this);
        }
    }

    IEnumerator AnimatedMove(Vector2 target, float durasi )
    {
        Vector2 initialPos = transform.position;
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime / durasi;
            transform.position = Vector2.Lerp(initialPos, target, percent);
            yield return null;
        }
        if(OnFinishMoving != null)
        {
            OnFinishMoving();
        }
    }

    public bool IsAtStartingCoord()
    {
        return BlockCoordinate == startingCoord;
    }
}
