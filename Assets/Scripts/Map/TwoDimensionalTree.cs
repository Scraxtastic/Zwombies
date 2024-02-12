
public class TwoDimensionalTree
{
    private BinaryTree<BinaryTree<TileHealth>> outerTree;

    public TwoDimensionalTree()
    {
        outerTree = new BinaryTree<BinaryTree<TileHealth>>();
    }

    public void Insert(TileHealth obj)
    {
        outerTree.Insert(new BinaryTree<TileHealth>(), (x, y) => x.Root.Data.indexX.CompareTo(y.Root.Data.indexX));
        var node = FindNode(outerTree.Root, obj.indexX);
        if (node != null)
        {
            node.Data.Insert(obj, (x, y) => x.indexY.CompareTo(y.indexY));
        }
    }

    private TreeNode<BinaryTree<TileHealth>> FindNode(TreeNode<BinaryTree<TileHealth>> node, int indexX)
    {
        if (node == null) return null;
        if (node.Data.Root != null && node.Data.Root.Data.indexX == indexX) return node;

        if (indexX < node.Data.Root.Data.indexX)
        {
            return FindNode(node.Left, indexX);
        }
        else
        {
            return FindNode(node.Right, indexX);
        }
    }
}