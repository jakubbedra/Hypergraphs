namespace Hypergraphs.Graphs.Algorithms.PCTrees;

public class TerminalPathFinder
{
    public List<PCNode> FindTerminalPath(PCTree tree)
    {
        Dictionary<PCNode, bool> visited = new Dictionary<PCNode, bool>();
        List<PCNode> terminalPath = new List<PCNode>();
        
        
        
        return terminalPath;
    }

    public void LabelNodes(PCTree tree)
    {
        List<PCNode> treeLeaves = tree.Leaves;
        if (treeLeaves.Count == 0) return;

        treeLeaves.ForEach(leaf => AssignLabel(leaf, tree));
        
        PCNode firstLeaf = treeLeaves[0];
        LabelNode(firstLeaf.Parent!, tree);
    }
    
// todo: check if c-nodes never have parents
    private void LabelNode(PCNode node, PCTree tree)
    {
        if (node.Type == NodeType.Leaf)
        {
            AssignLabel(node, tree);
            return;
        }
        List<PCNode> unlabeledNeighbours = node.Neighbours
            .Where(n => n != node.Parent && node.Parent != null && node.Parent.Parent != node && n.Label != NodeLabel.Undefined)
            .ToList();
        unlabeledNeighbours.ForEach(n => LabelNode(n, tree));
        
        AssignLabel(node, tree);
        if (node.Parent != null && node.Parent.Parent != node) LabelNode(node.Parent, tree);
    }

    private void AssignLabel(PCNode node, PCTree tree)
    {
        if (node.Type == NodeType.Leaf)
        {
            if (tree.GetValueInCurrentRow((int)node.Column) == 1)
                node.Label = NodeLabel.Full;
            else if (tree.GetValueInCurrentRow((int)node.Column) == 0)
                node.Label = NodeLabel.Empty;
        }
        else
        {
            int fullNeighboursCount = node.Neighbours.Count(n => n.Label == NodeLabel.Full);
            if (fullNeighboursCount == node.Neighbours.Count - 1)
                node.Label = NodeLabel.Full;
            else if (fullNeighboursCount > 0)
                node.Label = NodeLabel.Partial;
            else
                node.Label = NodeLabel.Empty; // todo: is this ok?
        }
    }
    
    public void ClearNodeLabels(PCTree tree)
    {
        
    }
    
}