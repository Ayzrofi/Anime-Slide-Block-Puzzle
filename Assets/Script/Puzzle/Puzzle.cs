using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour {
    // Image Componnet
    //[Header("Image For Puzzle")]
    //[Tooltip("This Sprite For Puzzle")]
    //public Sprite ImagePuzzle;
    //[Tooltip("This Image For Background Panel")]
    //public Image BgImage,FinalResultImage;
    private Texture2D image;

    public static int ammountBlockPerLine = 3;
    [SerializeField]
    private int ShuffleLength = 50;
    private float defaultMoveDuration = .2f;
    private float ShuffleMoveDuration = .02f;

    enum PuzzleState {Solved,Shuffling,InPlay }
    PuzzleState Status;

    int shuffleMoveRemaining;
    Vector2Int prevShuffleOffset;
    Block emptyBlock;
    Block[,] blocks;

    Queue<Block> inputs;
    bool BlockisMoving;

    [Header("SFX")]
    public AudioSource AudioSrc;
    public AudioClip Sfx;

    //public int ammountMove;
    private void Start()
    {
        if (AudioSrc == null)
            AudioSrc = GetComponent<AudioSource>();

        InitPuzzle();
        createPuzzle();
    }
    //private void Update()
    //{
    //    if (Status== PuzzleState.Solved && Input.GetMouseButtonDown(0))
    //        StartShuffle(); 
    //}
    public void InitPuzzle()
    {
        //BgImage.sprite = ImagePuzzle;
        //FinalResultImage.sprite = ImagePuzzle;
        image = GameMaster.TheInstanceOfGameMaster.levelSpriteImage.texture;
        //ShuffleLength = Random.Range(20, (ammountBlockPerLine * 10));
    }
    [ContextMenu("Create Puzzle")]
    void createPuzzle()
    {
        blocks = new Block[ammountBlockPerLine, ammountBlockPerLine]; 
        Texture2D[,] imageSlices = ImageSlicer.GetSlicer(image, ammountBlockPerLine);
        for (int y = 0; y < ammountBlockPerLine; y++)
        {
            for (int x = 0; x < ammountBlockPerLine; x++)
            {
                GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObject.transform.position = -Vector2.one * (ammountBlockPerLine - 1) * 0.5f + new Vector2 (x,y);
                blockObject.transform.parent = transform;

                Block block = blockObject.AddComponent<Block>();
                block.OnBlockPressed += PlayerMoveBlockInput;
                block.OnFinishMoving += OnBlockFinishedMove;

                block.initial(new Vector2Int(x, y), imageSlices[x, y]);
                blocks[x, y] = block;
                if(y == 0 && x == ammountBlockPerLine - 1)
                {
                    emptyBlock = block;
                }
            }
        }
        Camera.main.orthographicSize = ammountBlockPerLine;
        inputs = new Queue<Block>();

        //Vector2 newPos = gameObject.transform.position;
        //newPos.y += -1;
        //transform.position = newPos;
    }


   
    void PlayerMoveBlockInput(Block blockToMove)
    {
        if (Status== PuzzleState.InPlay)
        {
            inputs.Enqueue(blockToMove);
            MakeNextPlayerMove();
        }
    }

    void MakeNextPlayerMove()
    {
        while (inputs.Count > 0 && !BlockisMoving)
        {
            moveBlock(inputs.Dequeue(), defaultMoveDuration);
            PlaySound(Sfx);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (AudioSrc.isPlaying)
            AudioSrc.Stop();
        // Play Audio
        AudioSrc.PlayOneShot(clip);
    }
    void moveBlock(Block blockToMove, float duration)
    {
        if ((blockToMove.BlockCoordinate - emptyBlock.BlockCoordinate).sqrMagnitude == 1)
        {

            blocks[blockToMove.BlockCoordinate.x, blockToMove.BlockCoordinate.y] = emptyBlock;
            blocks[emptyBlock.BlockCoordinate.x, emptyBlock.BlockCoordinate.y] = blockToMove;

            Vector2Int targetCoor = emptyBlock.BlockCoordinate;
            emptyBlock.BlockCoordinate = blockToMove.BlockCoordinate;
            blockToMove.BlockCoordinate = targetCoor;

            Vector2 targetPos = emptyBlock.transform.position;
            emptyBlock.transform.position = blockToMove.transform.position;
            blockToMove.MoveToPositions(targetPos, duration);
            BlockisMoving = true;
            //if(Status == PuzzleState.InPlay)
            //{
            //    ammountMove += 1;
            //}
            
        }
    }
    void OnBlockFinishedMove()
    {
        BlockisMoving = false;

        CheckIfSolved();

        if (Status == PuzzleState.InPlay)
        {
            MakeNextPlayerMove();
        }
        else if (Status == PuzzleState.Shuffling)
        {
            if (shuffleMoveRemaining > 0)
            {
                MakeNextShuffleMove();
            }
            else
            {
                Status = PuzzleState.InPlay;
            }
        }
    }

 
    public void StartShuffle()
    {
        Status = PuzzleState.Shuffling;
        shuffleMoveRemaining = ShuffleLength;
        emptyBlock.gameObject.SetActive(false);
        MakeNextShuffleMove();
    }

    void MakeNextShuffleMove()
    {
        Vector2Int[] Offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
        int randomIndex = Random.Range(0, Offsets.Length);
        for (int i = 0; i < Offsets.Length; i++)
        {
            Vector2Int offset = Offsets[(randomIndex +i) % Offsets.Length];
            if (offset != prevShuffleOffset * -1)
            {
                Vector2Int moveBlockCoord = emptyBlock.BlockCoordinate + offset;

                if (moveBlockCoord.x >= 0 && moveBlockCoord.x < ammountBlockPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < ammountBlockPerLine)
                {
                    moveBlock(blocks[moveBlockCoord.x, moveBlockCoord.y],ShuffleMoveDuration);
                    shuffleMoveRemaining--;
                    prevShuffleOffset = offset;
                    break;
                }
            }
            
        }
        
    }

    void CheckIfSolved()
    {
        foreach (Block b in blocks)
        {
            if (!b.IsAtStartingCoord())
            {
                return;
            }
        }
        Status = PuzzleState.Solved;
        emptyBlock.gameObject.SetActive(true);
        winGame();
    }

    void winGame()
    {
        GameMaster.TheInstanceOfGameMaster.WinGameConditions();
        //PlayerPrefs.SetInt("Level2", 1);
        //Debug.Log("you Win");
        ////StartCoroutine(LoadLevelSelector());
    }

    //IEnumerator LoadLevelSelector()
    //{
    //    yield return new WaitForSeconds(2);
    //    //SceneManager.LoadScene("LevelSelector");
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
}// end class














