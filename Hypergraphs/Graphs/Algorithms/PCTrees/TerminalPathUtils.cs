using Hypergraphs.Extensions;

namespace Hypergraphs.Graphs.Algorithms.PCTrees;

public static class TerminalPathUtils
{
    // terminalPath must be already labeled (probably matrix and row will not be needed)
    public static bool TrySplit(List<PCNode> terminalPath, int[,] matrix, int row)
    {
        // check C-nodes
        // check if no partial node is in the middle of "1"s or "0"s
        for (var i = 0; i < terminalPath.Count; i++)
        {
            PCNode current = terminalPath[i];

            // find which is left of the current node and which is right
            PCNode? left = null;
            PCNode? right = null;
            if (i == 0)
            {
                right = terminalPath[i + 1];
                left = null;
            }
            else if (i == terminalPath.Count - 1)
            {
                left = terminalPath[i - 1];
                right = null;
            }
            else
            {
                left = terminalPath[i - 1];
                right = terminalPath[i + 1];
            }

            List<PCNode> currentNeighbours = current.Neighbours;

            // find the partial node(s)
            int partialNeighboursCount = currentNeighbours.Where(n => n.Label == NodeLabel.Partial).Count();
            if (partialNeighboursCount > 2)
                return false;

            if (current.Type == NodeType.C)
            {
                if (!OrderCNode(current, left, right)) return false;
            }
            else if (current.Type == NodeType.P) // P-node
            {
                OrderPNode(current, left, right);
            }
        }

        return false;
    }

    public static bool OrderCNode(PCNode current, PCNode? left, PCNode? right)
    {
        List<PCNode> currentNeighbours = current.Neighbours;
        // check that between those two from both sides are only Empty/Full nodes respectively
        if (left == null)
        {
            // leftmost
            // order neighbours from left to right
            int rotationsLeft = currentNeighbours.IndexOf(right);
            currentNeighbours.RotateLeft(rotationsLeft);
            // check if 1s and 0s are consecutive
            int labelChangeCount = 0;
            if (currentNeighbours[1].Label == NodeLabel.Full)
            {
                current.Flip(); // make ones be on top of the path
                currentNeighbours.RotateLeft(1);
                currentNeighbours.Reverse();
            }

            for (int j = 2; j < currentNeighbours.Count; j++)
                if (currentNeighbours[j] != currentNeighbours[j - 1])
                    labelChangeCount++;
            if (labelChangeCount > 1) return false;
        }
        else if (right == null)
        {
            // rightmost                    
            // order neighbours from right to left
            int rotationsLeft = currentNeighbours.IndexOf(left);
            currentNeighbours.RotateLeft(rotationsLeft);
            // check if 1s and 0s are consecutive
            int labelChangeCount = 0;
            if (currentNeighbours[1].Label != NodeLabel.Full) // todo: we need to flip all other nodes that came before if we flip a next one xdddd
            {
                current.Flip(); // make ones be on top of the path
                currentNeighbours.RotateLeft(1);
                currentNeighbours.Reverse();
            }

            for (int j = 2; j < currentNeighbours.Count; j++)
                if (currentNeighbours[j] != currentNeighbours[j - 1])
                    labelChangeCount++;
            if (labelChangeCount > 1) return false;
        }
        else
        {
            // somewhere in the middle
            int rotationsLeft = currentNeighbours.IndexOf(left);
            currentNeighbours.RotateLeft(rotationsLeft);
            // check if 1s and 0s are consecutive
            int labelChangeCount = 0;
            if (currentNeighbours[1].Label != NodeLabel.Full)
            {
                current.Flip(); // make ones be on top of the path
                currentNeighbours.RotateLeft(1);
                currentNeighbours.Reverse();
            }

            for (int j = 2; j < currentNeighbours.Count; j++)
                if (currentNeighbours[j] != currentNeighbours[j - 1])
                    labelChangeCount++;
            if (labelChangeCount > 2) return false;
        }

        return true;
    }

    public static void OrderPNode(PCNode current, PCNode? left, PCNode? right)
    {
        List<PCNode> currentNeighbours = current.Neighbours;
        List<PCNode> oneList = new List<PCNode>();
        if (left != null) oneList.Add(left);
        List<PCNode> zeroList = new List<PCNode>();
        if (right != null) zeroList.Add(right);
        foreach (PCNode neighbour in currentNeighbours)
        {
            if (neighbour.Label == NodeLabel.Full)
                oneList.Add(neighbour);
            if (neighbour.Label != NodeLabel.Full && neighbour.Label != NodeLabel.Partial)
                zeroList.Add(neighbour);
        }

        if (left == null) // leftmost : change needed
        {
            oneList.Reverse();
            oneList.ForEach(node => zeroList.Add(node));
            zeroList.ForEach(node => oneList.Add(node));
            current.ModifyOrder(zeroList);
        }
        else // rightmost and in the middle: no change needed
        {
            zeroList.ForEach(node => oneList.Add(node));
            current.ModifyOrder(oneList);
        }
    }

    private static int LeafValue(PCNode childNode, int[,] matrix, int row)
    {
        if (childNode.Column == null)
            throw new ArgumentException("Child node should have column index assigned.");
        return matrix[row, (int)childNode.Column];
    }
    
}