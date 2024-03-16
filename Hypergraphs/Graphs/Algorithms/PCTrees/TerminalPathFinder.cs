namespace Hypergraphs.Graphs.Algorithms.PCTrees;

public class TerminalPathFinder
{
    private PCTree _tree;
    private List<PCNode> _partialNodes;

    // used only for testing
    public TerminalPathFinder(List<PCNode> partialNodes)
    {
        _partialNodes = partialNodes;
    }
    
    public TerminalPathFinder(PCTree tree)
    {
        _tree = tree;
        _partialNodes = new List<PCNode>();
    }
    
    public List<PCNode>? FindTerminalPath()
    {
        // Dictionary<PCNode, bool> visited = new Dictionary<PCNode, bool>();
        List<PCNode> terminalPath = new List<PCNode>();
        
        // check if all nodes labeled
        
        // todo: find all partial nodes during labeling

        if (_partialNodes.Count >= 0 && _partialNodes.Count < 3)
            return _partialNodes;

        // albo po porstu znalezc dowolnego node'a ktory ma tylko jednego sasiada
        // bedacego w _partialNodes i bierzemy kazdego kolejnego sasiada ktory jest
        // w partial nodes a nie ma go w terminalPath jeszcze
        PCNode currentNode = _partialNodes.First(node => node.Neighbours.Count(neighbour => _partialNodes.Contains(neighbour)) == 1);
        terminalPath.Add(currentNode);
        
        while (terminalPath.Count != _partialNodes.Count) // todo: trza tp na ifa jakos zmienic, bo to jest warunek zwracajacy nulla jak nie przejdziemy przez wzsystkie
        {
            PCNode? neighbour = currentNode!.Neighbours
                .FirstOrDefault(node => _partialNodes.Contains(node) && !terminalPath.Contains(node));
            if (neighbour == null && terminalPath.Count != _partialNodes.Count)
                return null;
            terminalPath.Add(neighbour);
            currentNode = neighbour;
        }
        
        return terminalPath;
    }

    public void LabelNodes(int dupa)
    {
        List<PCNode> treeLeaves = _tree.Leaves;
        if (treeLeaves.Count == 0) return;

        foreach (PCNode leaf in treeLeaves)
        {
            if (leaf.Label == NodeLabel.Undefined)
            {
                LabelNode(leaf);
            }
        }
    }

    
    // TODO: TO FIX
    
    
    public void LabelNodes()
    {
        List<PCNode> treeLeaves = _tree.Leaves;
        if (treeLeaves.Count == 0) return;

        HashSet<PCNode> visited = new HashSet<PCNode>();
        Queue<PCNode> queue = new Queue<PCNode>();
        treeLeaves.ForEach(l => queue.Enqueue(l));
        treeLeaves.ForEach(l => visited.Add(l));
        while (queue.Count > 0)
        {
            PCNode node = queue.Dequeue();
            node.Neighbours
                .Where(neighbour => !visited.Contains(neighbour))
                .ToList()
                .ForEach(neighbour =>
                {
                    visited.Add(neighbour);
                    queue.Enqueue(neighbour);
                });
            int unlabeledNeighboursCount = node.Neighbours.Count(n => n.Label == NodeLabel.Undefined);
            if (unlabeledNeighboursCount > 1 || (node.Type == NodeType.C && node.Neighbours.Any(n => n.Type != NodeType.C && n.Label == NodeLabel.Undefined)))// or if c-node has any non c-node unlabeled neighbours
            {
                queue.Enqueue(node);
            }
            else
            {
                AssignLabel(node);
            }
        }
    }
    
    private void LabelNode(PCNode node)
    {
        // jezeli sa jakies dzieci nieolabelowane to labeluj je
        List<PCNode> unlabeledChildren = node.Neighbours.Where(n => n.Parent == node && node.Parent != n && n.Label == NodeLabel.Undefined).ToList();
        unlabeledChildren.ForEach(c => LabelNode(c));
        
        // labelujemy
        AssignLabel(node);
        
        // idziemy do przodka
        if (node.Parent != null && node.Parent.Label == NodeLabel.Undefined) LabelNode(node.Parent);
    }

    private void AssignLabel(PCNode node)
    {
        if (node.Label != NodeLabel.Undefined) return;
        if (node.Type == NodeType.Leaf)
        {
            if (_tree.GetValueInCurrentRow((int)node.Column) == 1)
                node.Label = NodeLabel.Full;
            else if (_tree.GetValueInCurrentRow((int)node.Column) == 0)
                node.Label = NodeLabel.Empty;
        }
        else
        {
            int fullNeighboursCount = node.Neighbours.Count(n => n.Label == NodeLabel.Full);
            int emptyNeighboursCount = node.Neighbours.Count(n => n.Label == NodeLabel.Empty);
            if (fullNeighboursCount == node.Neighbours.Count - 1)
            {
                node.Label = NodeLabel.Full;
            }
            else if (emptyNeighboursCount == node.Neighbours.Count - 1)
            {
                node.Label = NodeLabel.Empty; // todo: is this ok?
            }
            else 
            {
                node.Label = NodeLabel.Partial;
                _partialNodes.Add(node);
            }
        }
    }
    
    public void ClearNodeLabels()
    {
        PCNode node = _tree.Leaves[0];
        Queue<PCNode> queue = new Queue<PCNode>();
        queue.Enqueue(node);
        HashSet<PCNode> visited = new HashSet<PCNode>();

        while (queue.Count != 0)
        {
            node = queue.Dequeue();
            foreach (PCNode neighbour in node.Neighbours.Where(n => n.Label != NodeLabel.Undefined))
            {
                queue.Enqueue(neighbour);
            }

            visited.Add(node);
            node.Label = NodeLabel.Undefined;
        }
        _partialNodes.Clear();
    }
    
}