using Hypergraphs.Extensions;

namespace Hypergraphs.Graphs.Algorithms.PCTrees;

public static class TerminalPathRearrangementUtils
{

    public static PCNode SplitAndMergePathV2(List<PCNode> terminalPath)
    {
        PCNode centralCNode = new PCNode()
        {
            Type = NodeType.C,
            Label = NodeLabel.Partial,
        };

        List<PCNode> lower = new List<PCNode>();
        List<PCNode> upper = new List<PCNode>();
        
        foreach (PCNode currentNode in terminalPath)
        {
            PCNode? upperNode = new PCNode()
            {
                Type = currentNode.Type,
                Label = NodeLabel.Full,
                Column = currentNode.Column,
                Neighbours = currentNode.Neighbours.Where(n => n.Label == NodeLabel.Full).ToList()
            };

            if (upperNode.Neighbours.Count > 0)
            {
                if (upperNode.Neighbours.Count == 1)
                {
                    upperNode.Neighbours.ForEach(n => n.Parent = centralCNode);
                    upperNode.Neighbours.ForEach(n => n.Neighbours[n.Neighbours.IndexOf(currentNode)] = centralCNode);
                    // upperNode.Neighbours.ForEach(n => centralCNode.AppendNeighbour(n));
                    upperNode.Neighbours.ForEach(n => upper.Add(n));
                }
                else if (upperNode.Type == NodeType.P)
                {
                    upperNode.Neighbours.Where(n => n.Type != NodeType.C).ToList().ForEach(n => n.Parent = upperNode);
                    upperNode.Neighbours.ForEach(n => n.Neighbours[n.Neighbours.IndexOf(currentNode)] = upperNode);
                    // centralCNode.AppendNeighbour(upperNode);
                    upper.Add(upperNode);
                    upperNode.Parent = centralCNode;
                    upperNode.AppendNeighbour(centralCNode);
                }
                else if (upperNode.Type == NodeType.C)
                {
                    upperNode.Neighbours.ForEach(n => n.Parent = centralCNode);
                    upperNode.Neighbours.ForEach(n => n.Neighbours[n.Neighbours.IndexOf(currentNode)] = centralCNode);
                    // upperNode.Neighbours.ForEach(n => centralCNode.AppendNeighbour(n));
                    upperNode.Neighbours.ForEach(n => upper.Add(n));
                }
            }

            PCNode? lowerNode = new PCNode()
            {
                Type = currentNode.Type,
                Label = NodeLabel.Empty,
                Column = currentNode.Column,
                Neighbours = currentNode.Neighbours.Where(n => n.Label == NodeLabel.Empty).ToList()
            };
            if (lowerNode.Neighbours.Count > 0)
            {
                List<PCNode> lowerNeighbours = lowerNode.Neighbours;
                if (lowerNeighbours.Count == 1)
                {
                    lowerNeighbours.ForEach(n => n.Parent = centralCNode);
                    lowerNeighbours.ForEach(n => n.Neighbours[n.Neighbours.IndexOf(currentNode)] = centralCNode);
                    // todo: add to central c node
                    // lowerNeighbours.Reverse();
                    // lowerNeighbours.ForEach(n => centralCNode.PrependNeighbour(n)); // todo: reverse?
                    lowerNeighbours.ForEach(n => lower.Add(n));
                }
                else if (lowerNode.Type == NodeType.P)
                {
                    lowerNeighbours.ForEach(n => n.Parent = lowerNode);
                    lowerNeighbours.ForEach(n => n.Neighbours[n.Neighbours.IndexOf(currentNode)] = lowerNode);
                    lowerNode.Parent = centralCNode;
                    lowerNode.AppendNeighbour(centralCNode);
                    // centralCNode.PrependNeighbour(lowerNode);
                    lower.Add(lowerNode);
                }
                else if (lowerNode.Type == NodeType.C)
                {
                    lowerNeighbours.ForEach(n => n.Parent = centralCNode);
                    lowerNeighbours.ForEach(n => n.Neighbours[n.Neighbours.IndexOf(currentNode)] = centralCNode);
                    // lowerNeighbours.Reverse();
                    // lowerNeighbours.ForEach(n => centralCNode.PrependNeighbour(n)); // todo: reverse?
                    lowerNeighbours.Reverse();// TODO : I think we should reverse here also...
                    
                    // todo: reverse only if previous node is on the right?
                    lowerNeighbours.ForEach(n => lower.Add(n));
                }
            }
        }

        upper.ForEach(n => centralCNode.AppendNeighbour(n));
        lower.ForEach(n => centralCNode.PrependNeighbour(n));

        if (centralCNode.Neighbours.Count == 2)
        {
            // merge
            PCNode node1 = centralCNode.Neighbours[0];
            PCNode node2 = centralCNode.Neighbours[1];

            if (node2.Type != NodeType.Leaf && node1.Type != NodeType.C) node1.Parent = node2;
            node1.Neighbours[node1.Neighbours.IndexOf(centralCNode)] = node2;
            if (node1.Type != NodeType.Leaf && node2.Type != NodeType.C) node2.Parent = node1;
            node2.Neighbours[node2.Neighbours.IndexOf(centralCNode)] = node1;
            return node1;
        }
        
        // todo: merge c-nodes
        
        return centralCNode;
    }
    
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
            PCNode? upperNode = new PCNode()
            {
                Type = currentNode.Type,
                Label = NodeLabel.Full,
                Column = currentNode.Column,
                Neighbours = currentNode.Neighbours.Where(n => n.Label == NodeLabel.Full).ToList()
                
                
                
                // todo: remove original node from neighbours list (parent is already replaced)
                
                
                
                
            };// todo: set parent
            upperNode.Neighbours.ForEach(n => n.Neighbours.Remove(currentNode));
            //upperNode.Neighbours.ForEach(n => n.Parent = upperNode);
            //upperNode.Neighbours.ForEach(n => n.AppendNeighbour(upperNode));
            //upperNode.Neighbours.ForEach(n => n.Neighbours.Remove(currentNode));

            if (upperNode.Neighbours.Count == 1)
                upperNode = upperNode.Neighbours[0];
            if (upperNode.Neighbours.Count == 0)
                upperNode = null;

            PCNode? lowerNode = new PCNode()
            {
                Type = currentNode.Type,
                Label = NodeLabel.Empty,
                Column = currentNode.Column,
                Neighbours = currentNode.Neighbours.Where(n => n.Label == NodeLabel.Empty).ToList()
            };
            lowerNode.Neighbours.ForEach(n => n.Neighbours.Remove(currentNode));
            //lowerNode.Neighbours.ForEach(n => n.Parent = lowerNode);
            //lowerNode.Neighbours.ForEach(n => n.AppendNeighbour(lowerNode));
            //lowerNode.Neighbours.ForEach(n => n.Neighbours.Remove(currentNode));
            
            if (lowerNode.Neighbours.Count == 1)
                lowerNode = lowerNode.Neighbours[0];
            else if (lowerNode.Neighbours.Count == 0)
                lowerNode = null;
                    
            // add or merge, do not add if node has no neighbours!!!!
            if (upperNode != null)
                MergeNode(upperNode, upper, centralCNode);
            if (lowerNode != null)
                MergeNode(lowerNode, lower, centralCNode, true);
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
            if (neighbour2.Type != NodeType.Leaf && neighbour1.Type != NodeType.C) neighbour1.Parent = neighbour2;
            if (neighbour1.Type != NodeType.Leaf && neighbour2.Type != NodeType.C) neighbour2.Parent = neighbour1;
            centralCNode = neighbour1;
        }
        
        return centralCNode;
    }

    private static void MergeNode(PCNode nodeToMerge, List<PCNode> nodesList, PCNode centralCNode, bool isLower = false)
    {
        // if (nodeToMerge.Neighbours.Count > 0)
        // {
        //     // merge C-node
        //     if (nodeToMerge.Type == NodeType.C)
        //     {
        //         if (isLower) nodeToMerge.Neighbours.Reverse();
        //         nodeToMerge.Neighbours.ForEach(node => nodesList.Add(node));
        //         nodeToMerge.Neighbours.ForEach(node => node.PrependNeighbour(centralCNode));
        //         nodeToMerge.Neighbours.ForEach(node => node.Parent = centralCNode);
        //     }
        //     else // link P-node
        //     {
        //         nodeToMerge.Parent = centralCNode;
        //         nodeToMerge.PrependNeighbour(centralCNode);
        //         nodesList.Add(nodeToMerge);
        //     }
        // }
        // else if (nodeToMerge.Type == NodeType.Leaf)
        // {
        //     nodeToMerge.Parent = centralCNode;
        //     nodeToMerge.PrependNeighbour(centralCNode);
        //     nodesList.Add(nodeToMerge);
        // }
        // if (nodeToMerge.Type != NodeType.Leaf)
        // {
            // merge C-node
            if (nodeToMerge.Neighbours.Count != 0)
            {
                if (nodeToMerge.Type == NodeType.C)
                {
                    if (isLower) nodeToMerge.Neighbours.Reverse();
                    nodeToMerge.Neighbours.ForEach(node => nodesList.Add(node));
                    nodeToMerge.Neighbours.ForEach(node => node.PrependNeighbour(centralCNode));
                    nodeToMerge.Neighbours.ForEach(node => node.Parent = centralCNode);
                    nodeToMerge.Neighbours.ForEach(node => node.Neighbours.Remove(nodeToMerge));
                }
                else // link P-node/leaf
                {
                    nodeToMerge.Parent = centralCNode;
                    nodeToMerge.PrependNeighbour(centralCNode);
                    nodesList.Add(nodeToMerge);
                }
            }
            // }
        // else
        // {
            // nodeToMerge.Parent = centralCNode;
            // nodeToMerge.PrependNeighbour(centralCNode);
            // nodesList.Add(nodeToMerge);
        // }
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
                if (partialNeighboursCount > 0) return false;//todo: nie powinno byc true?
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
            // if (currentNeighbours[1].Label == NodeLabel.Full)
            if (CheckCOrder(current))
            {
                //current.Flip(); // make ones be on top of the path
                currentNeighbours.RotateLeft(1);
                currentNeighbours.Reverse();
            }

            if (LabelChangeCountExceeded(currentNeighbours)) return false;
        }
        else if (right == null && left != null)
        {
            // rightmost                    
            // order neighbours from right to left
            int rotationsLeft = currentNeighbours.IndexOf(left);
            currentNeighbours.RotateLeft(rotationsLeft);
            // check if 1s and 0s are consecutive
            // if (currentNeighbours[1].Label != NodeLabel.Full) // todo: we need to flip all other nodes that came before if we flip a next one xdddd
            if (!CheckCOrder(current)) // todo: we need to flip all other nodes that came before if we flip a next one xdddd
            {
                //current.Flip(); // make ones be on top of the path
                currentNeighbours.RotateLeft(1);
                currentNeighbours.Reverse();
            }// todo: current naighbours is shallow copy
            if (LabelChangeCountExceeded(currentNeighbours)) return false;
        }
        else if (right != null && left != null)
        {
            // somewhere in the middle
            int rotationsLeft = currentNeighbours.IndexOf(left);//todo: might be null!!!!!!
            currentNeighbours.RotateLeft(rotationsLeft);
            // check if 1s and 0s are consecutive
            if (!CheckCOrder(current))// todo: problem w tym ze i tak czy srak nie mamy fulli tutaj, wiec trzeba sprawdzac caly porzadek cykliczny w sensie left -> full -> right -> empty
            {
                //current.Flip(); // make ones be on top of the path
                currentNeighbours.RotateLeft(1);
                currentNeighbours.Reverse();
            }
            if (LabelChangeCountExceeded(currentNeighbours, 2)) return false;
        }
        else if (right == null && left == null)
        {
            PCNode? firstFullNode = currentNeighbours.FirstOrDefault(n => n.Label == NodeLabel.Full);
            PCNode? firstEmptyNode = currentNeighbours.FirstOrDefault(n => n.Label == NodeLabel.Empty);
            int indexOfFirstFull = firstFullNode != null ? currentNeighbours.IndexOf(firstFullNode) : -1;
            int indexOfFirstEmpty = firstEmptyNode != null ? currentNeighbours.IndexOf(firstEmptyNode) : -1;
            currentNeighbours.RotateLeft(Math.Max(indexOfFirstFull, indexOfFirstEmpty));
            
            // if (fullNode != null)
            // {
            //     int rotationsLeft = currentNeighbours.IndexOf(fullNode);
            //     currentNeighbours.RotateLeft(rotationsLeft);
            // }
            //
            // if (LabelChangeCountExceeded(currentNeighbours))
            // {
            //     PCNode? emptyNode = currentNeighbours.FindLast(n => n.Label == NodeLabel.Empty);
            //     int rotationsLeft = currentNeighbours.IndexOf(emptyNode) + 1;
            //     currentNeighbours.RotateLeft(rotationsLeft);
            // }
        }
        return true;
    }

    // we assume that the right partial is on the right and left is on the left
    private static bool CheckCOrder(PCNode cNode)
    {
        bool anyFullNeighboursPresent = cNode.Neighbours.Any(n => n.Label == NodeLabel.Full);
        bool anyEmptyNeighboursPresent = cNode.Neighbours.Any(n => n.Label == NodeLabel.Empty);

        if (anyFullNeighboursPresent)
        {
            return cNode.Neighbours[1].Label == NodeLabel.Full;
        }
        if (anyEmptyNeighboursPresent)
        {
            return cNode.Neighbours[1].Label == NodeLabel.Partial && cNode.Neighbours[2].Label == NodeLabel.Empty;
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

}