using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Map;
using UnityEngine;

public class TilemapHealth : MonoBehaviour
{
    [SerializeField] private BinaryTree<TileHealthBinaryTree> tileHealthTree = new BinaryTree<TileHealthBinaryTree>();


    /**
     * Attack a tile at the given index with the given damage.
     * @return true if the tile was destroyed, false otherwise.
     */
    public bool AttackTile(int indexX, int indexY, float damage)
    {
        TileHealthBinaryTree tileHealthBinaryTree = new TileHealthBinaryTree(indexX);
        BinaryTreeNode<TileHealthBinaryTree> xResult = tileHealthTree.Search(tileHealthBinaryTree);
        if (xResult == null)
        {
            CreateNewTileX(indexX, indexY);
            return false;
        }

        BinaryTreeNode<TileHealthIndex> yResult = xResult.Data.tileHealthTree.Search(new TileHealthIndex(indexY));
        if (yResult == null)
        {
            CreateNewTileY(xResult.Data, indexX, indexY);
            return false;
        }

        TileHealth tileHealth = yResult.Data.tileHealth;
        tileHealth.TakeDamage(damage);
        if (tileHealth.health > 0)
        {
            return false;
        }

        BinaryTree<TileHealthIndex> yTree = xResult.Data.tileHealthTree;
        bool shallRebalance = yTree.Count == 1;
        yTree.Delete(new TileHealthIndex(indexY), shallRebalance);
        if (yTree.Count == 0)
        {
            tileHealthTree.Delete(new TileHealthBinaryTree(indexX), true);
        }

        return true;
    }

    private void CreateNewTileX(int indexX, int indexY)
    {
        TileHealthBinaryTree tileHealthBinaryTree = new TileHealthBinaryTree(indexX);
        tileHealthTree.Insert(tileHealthBinaryTree);
        CreateNewTileY(tileHealthBinaryTree, indexX, indexY);
    }

    private void CreateNewTileY(TileHealthBinaryTree tileHealthBinaryTree, int indexX, int indexY)
    {
        tileHealthBinaryTree.tileHealthTree.Insert(new TileHealthIndex(indexY,
            new TileHealth(indexX, indexY).WithHealth(2)));
    }
}