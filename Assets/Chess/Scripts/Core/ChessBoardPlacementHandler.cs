using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public sealed class ChessBoardPlacementHandler : MonoBehaviour {
    [SerializeField] private GameObject[] _rowsArray;
    [SerializeField] private GameObject _highlightPrefab;
    private GameObject[,] _chessBoard;

    public static List<Vector2Int> _piecePosition = new List<Vector2Int>();
    public static List<Vector2Int> _highlightPosition = new List<Vector2Int>();

    internal static ChessBoardPlacementHandler Instance;

    private void Awake() {
        Instance = this;
        GenerateArray();
    }


    private void GenerateArray() {
        _chessBoard = new GameObject[8, 8];
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                _chessBoard[i, j] = _rowsArray[i].transform.GetChild(j).gameObject;
            }
        }
    }

    internal GameObject GetTile(int i, int j) {
        try {
            
            return _chessBoard[i, j];
        } catch (Exception) {
            Debug.LogError("Invalid row or column.");
            return null;
        }
    }

    internal void Highlight(List<Vector2Int> _highlightedTiles) {

        ClearHighlights();

        for(int i = 0; i < _highlightedTiles.Count; i++)
        {
            var tile = GetTile(_highlightedTiles[i].x, _highlightedTiles[i].y).transform;

            if (tile == null)
            {
                Debug.LogError("Invalid row or column.");
                return;
            }
            else
            {
                Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
            }
        }
    }

    internal void ClearHighlights() {
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                var tile = GetTile(i, j);
                if (tile.transform.childCount <= 0) continue;
                foreach (Transform childTransform in tile.transform) {
                    Destroy(childTransform.gameObject);
                }
            }
        }
    }

    internal bool IsEmpty(int i, int j)
    {
        Vector2Int _searchPositon = new Vector2Int(i, j);

        for (int it = 0; it < _piecePosition.Count; it++)
        {
            if (_searchPositon == _piecePosition[it])
            {
                return false;
            }
        }
        return true;
    }

    internal void RemovePosition(int i, int j)
    {
        Vector2Int positionToRemove = new Vector2Int(i, j);

        for (int it = 0; it < _piecePosition.Count; it++)
        {
            if (_piecePosition[it] == positionToRemove)
            {
                _piecePosition.RemoveAt(it);
            }
        }
    }

    internal void AddPosition(int i, int j)
    {
        Vector2Int positionToAdd = new Vector2Int(i, j);
        _piecePosition.Add(positionToAdd);
    }

    #region Highlight Testing

    // private void Start() {
    //     StartCoroutine(Testing());
    // }

    // private IEnumerator Testing() {
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(2, 7);
    //     Highlight(2, 6);
    //     Highlight(2, 5);
    //     Highlight(2, 4);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(7, 7);
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    // }

    #endregion
}