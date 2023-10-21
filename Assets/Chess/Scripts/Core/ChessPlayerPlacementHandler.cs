using System;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chess.Scripts.Core {
    public class ChessPlayerPlacementHandler : MonoBehaviour {
        [SerializeField] public int row, column;
        private int _prevrow, _prevcol;

        private void Start() {
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
            ChessBoardPlacementHandler.Instance.AddPosition(row, column);
            _prevrow = row;
            _prevcol = column;

        }
        private void Update()
        {
            if(_prevrow != row || _prevcol != column)
            {
                PieceMove(row, column);
            }
        }

        private void PieceMove(int row, int column)
        {
            
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
            
            ChessBoardPlacementHandler.Instance.AddPosition(row, column);
        
            ChessBoardPlacementHandler.Instance.RemovePosition(_prevrow, _prevcol);

            ChessBoardPlacementHandler.Instance.ClearHighlights();

            //AutoHighlight On TileChnage
            {
                //switch(transform.tag)
                //{
                //    case "King":
                //        //ChessBoardPlacementHandler._highlightPosition = null;

                //        ChessBoardPlacementHandler.Instance.Highlight(ValidKingMoves(row, column));

                //        break;

                //    case "Queen":
                //        ChessBoardPlacementHandler._highlightPosition = null;
                //        ChessBoardPlacementHandler._highlightPosition = ValidQueenMoves(row, column);

                //        ChessBoardPlacementHandler.Instance.Highlight(ChessBoardPlacementHandler._highlightPosition);

                //        break;

                //    case "Rook":
                //        ChessBoardPlacementHandler._highlightPosition = null;
                //        ChessBoardPlacementHandler._highlightPosition = ValidBishopMoves(row, column);

                //        ChessBoardPlacementHandler.Instance.Highlight(ChessBoardPlacementHandler._highlightPosition);

                //        break;

                //    case "Bishop":
                //        ChessBoardPlacementHandler._highlightPosition = null;
                //        ChessBoardPlacementHandler._highlightPosition = ValidBishopMoves(row, column);

                //        ChessBoardPlacementHandler.Instance.Highlight(ChessBoardPlacementHandler._highlightPosition);

                //        break;
                //    case "Knight":
                //        ChessBoardPlacementHandler._highlightPosition = null;
                //        ChessBoardPlacementHandler._highlightPosition = ValidKnightMoves(row, column);

                //        ChessBoardPlacementHandler.Instance.Highlight(ChessBoardPlacementHandler._highlightPosition);

                //        break;
                //    case "Pawn":
                //        ChessBoardPlacementHandler._highlightPosition = null;
                //        ChessBoardPlacementHandler._highlightPosition = ValidPawnMoves(row, column);

                //        ChessBoardPlacementHandler.Instance.Highlight(ChessBoardPlacementHandler._highlightPosition);

                //        break;
                //    default:
                //        Debug.Log("Click on a piece");
                //        break;
                //}
            }

            _prevrow = row;
            _prevcol = column;
        }

        public List<Vector2Int> ValidKingMoves(int row, int columns)
        {
            List<Vector2Int> coordinateList = new List<Vector2Int>();

            int _row = row-1, _col = columns-1;

            for(int i = row - 1; i <= row + 1; i++)
            {
                for(int j = column - 1; j <= column + 1; j++)
                {
                    if( (i == row && j == column) || (i > 7 || j > 7))
                    {
                        continue;
                    }
                    else
                    {   
                        if(ChessBoardPlacementHandler.Instance.IsEmpty(i, j))
                            coordinateList.Add(new Vector2Int(i, j));
                    }
                }
            }
            return coordinateList;
        }

        public List<Vector2Int> ValidRookMoves(int row, int column)
        {
            List<Vector2Int> coordinateList = new List<Vector2Int>();

            // Vertical and horizontal moves
            for (int i = row + 1; i < 8; i++)
            {
                if(ChessBoardPlacementHandler.Instance.IsEmpty(i, column))
                {
                    coordinateList.Add(new Vector2Int(i, column));
                }
                else
                {
                    break;
                }
            }

            for(int i = row - 1; i >= 0; i--)
            {
                if(ChessBoardPlacementHandler.Instance.IsEmpty(i, column))
                {
                    coordinateList.Add(new Vector2Int(i, column));
                }
                else
                {
                    break;
                }
            }

            for(int j =  column - 1; j >= 0; j--)
            {
                if(ChessBoardPlacementHandler.Instance.IsEmpty(row, j))
                {
                    coordinateList.Add(new Vector2Int(row, j));
                }
                else
                {
                    break;
                }
            }

            for (int j = column + 1; j < 8; j++)
            {
                if (ChessBoardPlacementHandler.Instance.IsEmpty(row, j))
                {
                    coordinateList.Add(new Vector2Int(row, j));
                }
                else
                {
                    break;
                }
            }

            return coordinateList;
        }

        public List<Vector2Int> ValidBishopMoves(int row, int column)
        {
            List<Vector2Int> coordinateList = new List<Vector2Int>();
            int[] _row = new int[4];

            for(int i = 0; i < 4; i++)
            {
                _row[i] = row;
            }

            // Upper Left
            for(int i = column - 1; i >= 0; i--)
            {
                _row[0]++;
                if(_row[0] > 7)
                {
                    break;
                }
                if (ChessBoardPlacementHandler.Instance.IsEmpty(_row[0], i))
                {
                    coordinateList.Add(new Vector2Int(_row[0], i));
                }
                else
                {
                    break;
                }
            }
            
            //Upper Right
            for (int i = column + 1; i < 8; i++)
            {
                _row[1]++;
                if (_row[1] > 7)
                {
                    break;
                }
                if (ChessBoardPlacementHandler.Instance.IsEmpty(_row[1], i))
                {
                    coordinateList.Add(new Vector2Int(_row[1], i));
                }
                else
                {
                    break;
                }
            }

            //Lower Left
            for (int i = column - 1; i >= 0; i--)
            {
                _row[2]--;
                if (_row[2] < 0)
                {
                    break;
                }
                if (ChessBoardPlacementHandler.Instance.IsEmpty(_row[2], i))
                {
                    coordinateList.Add(new Vector2Int(_row[2], i));
                }
                else
                {
                    break;
                }
            }

            //Lower Right
            for (int i = column + 1; i < 8; i++)
            {
                _row[3]--;
                if (_row[3] < 0)
                {
                    break;
                }
                if(ChessBoardPlacementHandler.Instance.IsEmpty(_row[3], i))
                {
                    coordinateList.Add(new Vector2Int(_row[3], i));
                }
                else
                {
                    break;
                }
                    
            }

            return coordinateList;
        }

        public List<Vector2Int> ValidQueenMoves(int row, int column)
        {
            List<Vector2Int> coordinateList = new List<Vector2Int>();

            // Combine valid rook and bishop moves
            coordinateList.AddRange(ValidRookMoves(row, column));
            coordinateList.AddRange(ValidBishopMoves(row, column));

            return coordinateList;
        }

        public List<Vector2Int> ValidKnightMoves(int row, int column)
        {
            List<Vector2Int> coordinateList = new List<Vector2Int>();

            int[] possibleRows = { row + 2, row + 1, row - 1, row - 2 };
            int[] possibleCols = { column + 1, column + 2, column - 1, column - 2 };

            foreach (int r in possibleRows)
            {
                foreach (int c in possibleCols)
                {
                    if (Math.Abs(row - r) + Math.Abs(column - c) == 3) // Valid L-shape distance
                    {
                        if (r >= 0 && r < 8 && c >= 0 && c < 8)
                        {
                            if (ChessBoardPlacementHandler.Instance.IsEmpty(r, c))
                            {
                                coordinateList.Add(new Vector2Int(r, c));
                            }
                        }
                    }
                }
            }

            return coordinateList;

        }
        
        public List<Vector2Int> ValidPawnMoves(int row, int column)
        {
            List<Vector2Int> coordinateList = new List<Vector2Int>();
            if(row < 7)
            {
                if (row == 1)
                {
                    if (ChessBoardPlacementHandler.Instance.IsEmpty(row + 2, column))
                    {
                        coordinateList.Add(new Vector2Int(row + 2, column));
                    }

                }

                if (ChessBoardPlacementHandler.Instance.IsEmpty(row + 1, column))
                {
                    coordinateList.Add(new Vector2Int(row + 1, column));
                }
            }

            return coordinateList;
        }

    }

}