using System;

public class BinaryTree<T>
{
    public TreeNode<T> Root { get; private set; }

    public void Insert(T data, Func<T, T, int> comparator)
    {
        Root = InsertInternal(Root, data, comparator);
    }

    private TreeNode<T> InsertInternal(TreeNode<T> node, T data, Func<T, T, int> comparator)
    {
        if (node == null)
        {
            return new TreeNode<T>(data);
        }

        int result = comparator(data, node.Data);
        if (result < 0)
        {
            node.Left = InsertInternal(node.Left, data, comparator);
        }
        else if (result > 0)
        {
            node.Right = InsertInternal(node.Right, data, comparator);
        }

        return node;
    }
}