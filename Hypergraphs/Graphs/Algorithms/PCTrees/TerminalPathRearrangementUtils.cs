using Hypergraphs.Extensions;

namespace Hypergraphs.Graphs.Algorithms.PCTrees;

public static class TerminalPathRearrangementUtils
{
    // given terminalPath is already rearranged
    public static PCNode SplitAndMergePath(List<PCNode> terminalPath)
    {
        PCNode centralCNode = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial,
        };

        List<PCNode> upper = new List<PCNode>();
        List<PCNode> lower = new List<PCNode>();
        
        foreach (PCNode currentNode in terminalPath)
        {
            PCNode upperNode = new PCNode()
            {
                Type = currentNode.Type,
                Label = NodeLabel.Full,
                Column = currentNode.Column,
                Neighbours = currentNode.Neighbours.Where(n => n.Label == NodeLabel.Full).ToList()
            };// todo: set parent

            if (upperNode.Neighbours.Count == 1)
                upperNode = upperNode.Neighbours[0];
            
            PCNode lowerNode = new PCNode()
            {
                Type = currentNode.Type,
                Label = NodeLabel.Empty,
                Column = currentNode.Column,
                Neighbours = currentNode.Neighbours.Where(n => n.Label == NodeLabel.Empty).ToList()
            };
            if (lowerNode.Neighbours.Count == 1)
                lowerNode = lowerNode.Neighbours[0];

            
            // add or merge, do node add if node has no neighbours!!!!
            MergeNode(upperNode, upper, centralCNode);
            MergeNode(lowerNode, lower, centralCNode, true);
            // todo: MERGE C-NODES !!!!!
        }

        lower.Reverse();
        
        upper.ForEach(node => centralCNode.AppendNeighbour(node));
        lower.ForEach(node => centralCNode.AppendNeighbour(node));
        
        // merge edges connected to a node of degree 2
        if (centralCNode.Neighbours.Count == 2)
        {
            PCNode neighbour1 = centralCNode.Neighbours[0];
            PCNode neighbour2 = centralCNode.Neighbours[1];
            neighbour1.Neighbours.Remove(centralCNode);
            neighbour2.Neighbours.Remove(centralCNode);
            neighbour1.PrependNeighbour(neighbour2);
            neighbour2.PrependNeighbour(neighbour1);
            neighbour1.Parent = neighbour2;
            neighbour2.Parent = neighbour1;
            centralCNode = neighbour1;
        }
        
        return centralCNode;
    }

    private static void MergeNode(PCNode nodeToMerge, List<PCNode> nodesList, PCNode centralCNode, bool isLower = false)
    {
        if (nodeToMerge.Neighbours.Count > 0)
        {
            // merge C-node
            if (nodeToMerge.Type == NodeType.C)
            {
                if (isLower) nodeToMerge.Neighbours.Reverse();
                nodeToMerge.Neighbours.ForEach(node => nodesList.Add(node));
                nodeToMerge.Neighbours.ForEach(node => node.PrependNeighbour(centralCNode));
                nodeToMerge.Neighbours.ForEach(node => node.Parent = centralCNode);
            }
            else // link P-node
            {
                nodeToMerge.Parent = centralCNode;
                nodeToMerge.PrependNeighbour(centralCNode);
                nodesList.Add(nodeToMerge);
            }
        }
        else if (nodeToMerge.Type == NodeType.Leaf)
        {
            nodeToMerge.Parent = centralCNode;
            nodeToMerge.PrependNeighbour(centralCNode);
            nodesList.Add(nodeToMerge);
        }
    }

    // terminalPath must be already labeled (probably matrix and row will not be needed)
    public static bool RearrangePath(List<PCNode> terminalPath)
    {
        for (var i = 0; i < terminalPath.Count; i++)
        {
            PCNode current = terminalPath[i];

            List<PCNode> currentNeighbours = current.Neighbours;
            int partialNeighboursCount = currentNeighbours.Where(n => n.Label == NodeLabel.Partial).Count();

            // find which is left of the current node and which is right
            PCNode? left = null;
            PCNode? right = null;
            
            if (terminalPath.Count != 1 && i == 0)
            {
                right = terminalPath[i + 1];
                left = null;
                if (partialNeighboursCount > 1) return false;
            }
            else if (terminalPath.Count != 1 && i == terminalPath.Count - 1)
            {
                left = terminalPath[i - 1];
                right = null;
                if (partialNeighboursCount > 1) return false;
            }
            else if (terminalPath.Count != 1)
            {
                left = terminalPath[i - 1];
                right = terminalPath[i + 1];
                if (partialNeighboursCount > 2) return false;
            }
            else
            {
                if (partialNeighboursCount > 0) return false;
            }

            // find the partial node(s)
// todo: we need to flip all other nodes that came before if we flip a next one xdddd (albo i nie bo lewe i prawe powinny zostac w tych samych miejscach)
            if (current.Type == NodeType.C)
            {
                if (!OrderCNode(current, left, right)) return false;
            }
            else if (current.Type == NodeType.P) // P-node
            {
                OrderPNode(current, left, right);
            }
        }

        return true;
    }

    public static bool OrderCNode(PCNode current, PCNode? left, PCNode? right)
    {
        List<PCNode> currentNeighbours = current.Neighbours;//todo:cahngeto deep copy?
        // check that between those two from both sides are only Empty/Full nodes respectively
        if (left == null && right != null)
        {
            // leftmost
            // order neighbours from left to right
            int rotationsLeft = currentNeighbours.IndexOf(right);
            currentNeighbours.RotateLeft(rotationsLeft);
            // check if 1s and 0s are consecutive
            if (currentNeighbours[1].Label == NodeLabel.Full)
            {
                //current.Flip(); // make ones be on top of the path
                currentNeighbours.RotateLeft(1);
                currentNeighbours.Reverse();
            }

            if (LabelChangeCountExceeded(currentNeighbours)) return false;
        }
        else if (right == null)
        {
            // rightmost                    
            // order neighbours from right to left
            int rotationsLeft = currentNeighbours.IndexOf(left);
            currentNeighbours.RotateLeft(rotationsLeft);
            // check if 1s and 0s are consecutive
            if (currentNeighbours[1].Label != NodeLabel.Full) // todo: we need to flip all other nodes that came before if we flip a next one xdddd
            {
                //current.Flip(); // make ones be on top of the path
                currentNeighbours.RotateLeft(1);
                currentNeighbours.Reverse();
            }// todo: current naighbours is shallow copy
            if (LabelChangeCountExceeded(currentNeighbours)) return false;
        }
        else
        {
            // somewhere in the middle
            int rotationsLeft = currentNeighbours.IndexOf(left);
            currentNeighbours.RotateLeft(rotationsLeft);
            // check if 1s and 0s are consecutive
            if (currentNeighbours[1].Label != NodeLabel.Full)
            {
                //current.Flip(); // make ones be on top of the path
                currentNeighbours.RotateLeft(1);
                currentNeighbours.Reverse();
            }
            if (LabelChangeCountExceeded(currentNeighbours, 2)) return false;
        }
        return true;
    }

    private static bool LabelChangeCountExceeded(List<PCNode> currentNeighbours, int maxCount = 1)
    {
        int labelChangeCount = 0;
        for (int j = 2; j < currentNeighbours.Count; j++)
            if (currentNeighbours[j].Label != currentNeighbours[j - 1].Label)
                labelChangeCount++;
        if (labelChangeCount > maxCount) return true;
        return false;
    }
    
    public static bool OrderPNode(PCNode current, PCNode? left, PCNode? right)
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

        return true;
    }

    private static int LeafValue(PCNode childNode, int[,] matrix, int row)
    {
        if (childNode.Column == null)
            throw new ArgumentException("Child node should have column index assigned.");
        return matrix[row, (int)childNode.Column];
    }
    
}