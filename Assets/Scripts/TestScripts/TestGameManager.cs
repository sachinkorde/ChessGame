using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    public GameObject pawnPrefab, rookPrefab, knightPrefab, bishopPrefab, queenPrefab, kingPrefab;
    public GameObject setSprite; // Prefab for the sprite to use on the chessboard
    public GameObject[,] chessBoard = new GameObject[8, 8];
    public float padding = 0.1f; // Adjust for padding around the board

    void Start()
    {
        // Set up the board first, then adjust its size and position
        SetChessBoard();
        AdjustBoardSizeAndPosition();
        SetPiecesOnStart();
    }

    void AdjustBoardSizeAndPosition()
    {
        // Calculate screen boundaries in world units
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Camera.main.aspect;

        // Determine square size to fit within screen dimensions
        float boardSize = Mathf.Min(screenWidth, screenHeight) - padding;
        float tileSize = boardSize / 8f; // Since there are 8 rows and columns

        // Scale and position each tile of the chessboard
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (chessBoard[i, j] != null)
                {
                    chessBoard[i, j].transform.localScale = new Vector3(tileSize, tileSize, 1);
                    chessBoard[i, j].transform.position = new Vector3(
                        (i - 3.5f) * tileSize,
                        (j - 3.5f) * tileSize,
                        0
                    );
                }
            }
        }
    }

    void PlacePiece(GameObject piecePrefab, string pieceName, int col, int row, Color color)
    {
        // Position the piece at the tile's position in the chessBoard array
        Vector3 tilePosition = chessBoard[col, row].transform.position;
        GameObject piece = Instantiate(piecePrefab, tilePosition, Quaternion.identity);
        piece.name = pieceName;

        // Apply the specified color to the piece
        SpriteRenderer spriteRenderer = piece.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }

        // Set the parent for organization
        piece.transform.parent = this.transform;
    }

    void SetPiecesOnStart()
    {
        // Define colors for player pieces
        Color whitePlayerColor = Color.white;
        Color blackPlayerColor = Color.black;

        // Place and color white pieces
        PlacePiece(rookPrefab, "White Rook", 0, 0, whitePlayerColor);
        PlacePiece(knightPrefab, "White Knight", 1, 0, whitePlayerColor);
        PlacePiece(bishopPrefab, "White Bishop", 2, 0, whitePlayerColor);
        PlacePiece(queenPrefab, "White Queen", 3, 0, whitePlayerColor);
        PlacePiece(kingPrefab, "White King", 4, 0, whitePlayerColor);
        PlacePiece(bishopPrefab, "White Bishop", 5, 0, whitePlayerColor);
        PlacePiece(knightPrefab, "White Knight", 6, 0, whitePlayerColor);
        PlacePiece(rookPrefab, "White Rook", 7, 0, whitePlayerColor);

        for (int i = 0; i < 8; i++)
        {
            PlacePiece(pawnPrefab, "White Pawn", i, 1, whitePlayerColor);
        }

        // Place and color black pieces
        PlacePiece(rookPrefab, "Black Rook", 0, 7, blackPlayerColor);
        PlacePiece(knightPrefab, "Black Knight", 1, 7, blackPlayerColor);
        PlacePiece(bishopPrefab, "Black Bishop", 2, 7, blackPlayerColor);
        PlacePiece(queenPrefab, "Black Queen", 3, 7, blackPlayerColor);
        PlacePiece(kingPrefab, "Black King", 4, 7, blackPlayerColor);
        PlacePiece(bishopPrefab, "Black Bishop", 5, 7, blackPlayerColor);
        PlacePiece(knightPrefab, "Black Knight", 6, 7, blackPlayerColor);
        PlacePiece(rookPrefab, "Black Rook", 7, 7, blackPlayerColor);

        for (int i = 0; i < 8; i++)
        {
            PlacePiece(pawnPrefab, "Black Pawn", i, 6, blackPlayerColor);
        }
    }

    void SetChessBoard()
    {
        // Define custom colors
        Color color1 = new Color(0.9f, 0.8f, 0.7f); // Light brown
        Color color2 = new Color(0.6f, 0.4f, 0.2f); // Dark brown

        // Loop through each position on the chessboard
        for (int i = 0; i < chessBoard.GetLength(0); i++)
        {
            for (int j = 0; j < chessBoard.GetLength(1); j++)
            {
                // Instantiate the sprite at position (i, j) and set its parent
                GameObject spriteInstance = Instantiate(setSprite, Vector3.zero, Quaternion.identity);

                // Assign the sprite to the chessboard array
                chessBoard[i, j] = spriteInstance;

                // Set the color based on position for a checkerboard effect
                SpriteRenderer spriteRenderer = spriteInstance.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    // Alternate color based on even/odd positions
                    spriteRenderer.color = (i + j) % 2 == 0 ? color1 : color2;
                }

                // Optionally, set this GameObject as a child of the board manager for organization
                spriteInstance.transform.parent = this.transform;
            }
        }
    }
}
