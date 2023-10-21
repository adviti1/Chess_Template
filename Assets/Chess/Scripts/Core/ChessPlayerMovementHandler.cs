using Chess.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ChessPlayerMovementHandler : MonoBehaviour
{
    private bool isSelected = false;
    private ChessPlayerPlacementHandler selectedPiece;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition2D, Vector2.zero);
            
            if (hit.collider != null)
            {
                ChessPlayerPlacementHandler clickedPiece = hit.collider.GetComponent<ChessPlayerPlacementHandler>();
                
                if (clickedPiece != null)
                {
                    // A chess piece was clicked
                    if (!isSelected)
                    {
                        // Select the clicked piece
                        selectedPiece = clickedPiece;
                        isSelected = true;

                        //Highlight valid moves for the selected piece

                        switch (selectedPiece.tag)
                        {
                            case "King":
                                ChessBoardPlacementHandler._highlightPosition = null;
                                ChessBoardPlacementHandler._highlightPosition = clickedPiece.ValidKingMoves(selectedPiece.row, selectedPiece.column);

                                //ChessBoardPlacementHandler.Instance.Highlight(ChessBoardPlacementHandler._highlightPosition);

                                break;
                            
                            case "Queen":
                                ChessBoardPlacementHandler._highlightPosition = null;
                                ChessBoardPlacementHandler._highlightPosition = clickedPiece.ValidQueenMoves(selectedPiece.row, selectedPiece.column);

                                //ChessBoardPlacementHandler.Instance.Highlight(ChessBoardPlacementHandler._highlightPosition);

                                break;
                            
                            case "Rook":
                                ChessBoardPlacementHandler._highlightPosition = null;
                                ChessBoardPlacementHandler._highlightPosition = clickedPiece.ValidBishopMoves(selectedPiece.row, selectedPiece.column);

                                //ChessBoardPlacementHandler.Instance.Highlight(ChessBoardPlacementHandler._highlightPosition);

                                break;

                            case "Bishop":
                                ChessBoardPlacementHandler._highlightPosition = null;
                                ChessBoardPlacementHandler._highlightPosition = clickedPiece.ValidBishopMoves(selectedPiece.row, selectedPiece.column);

                                //ChessBoardPlacementHandler.Instance.Highlight(ChessBoardPlacementHandler._highlightPosition);

                                break;
                            case "Knight":
                                ChessBoardPlacementHandler._highlightPosition = null;
                                ChessBoardPlacementHandler._highlightPosition = clickedPiece.ValidKnightMoves(selectedPiece.row, selectedPiece.column);

                                

                                break;
                            case "Pawn":
                                ChessBoardPlacementHandler._highlightPosition = null;
                                ChessBoardPlacementHandler._highlightPosition = clickedPiece.ValidPawnMoves(selectedPiece.row, selectedPiece.column);

                                //ChessBoardPlacementHandler.Instance.Highlight(ChessBoardPlacementHandler._highlightPosition);

                                break;
                            default:
                                Debug.Log("Click on a piece");
                                break;
                               
                        }
                        ChessBoardPlacementHandler.Instance.Highlight(ChessBoardPlacementHandler._highlightPosition);
                    }


                    else
                    {
                        // Clear the selection
                        isSelected = false;
                        selectedPiece = null;
                    }
                }

            }
            else
            {
                ChessBoardPlacementHandler.Instance.ClearHighlights();
            }
        }
    }

}
