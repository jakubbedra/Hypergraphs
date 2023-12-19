using Hypergraphs.Graphs.Algorithms.PCTrees;

namespace HypergraphsTests.Graphs.Algorithms.PCTrees.Provider;

public static class PCTreesProvider
{
    public static PCTree VeryComplexCaseLeavesUnlabeled()
    {
        int[,] matrix = new int[,]
        {
            { 1, 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0 }
        };
        // PCNode p1 = new PCNode() { Type = NodeType.P, Label = NodeLabel.Partial };
        // PCNode p2 = new PCNode() { Type = NodeType.P, Label = NodeLabel.Partial };
        // PCNode p3 = new PCNode() { Type = NodeType.P, Label = NodeLabel.Partial };
        // PCNode p4 = new PCNode() { Type = NodeType.P, Label = NodeLabel.Full };
        // PCNode p5 = new PCNode() { Type = NodeType.P, Label = NodeLabel.Full };
        // PCNode p6 = new PCNode() { Type = NodeType.P, Label = NodeLabel.Partial };
        // PCNode p7 = new PCNode() { Type = NodeType.P, Label = NodeLabel.Full };
        // PCNode c1 = new PCNode() { Type = NodeType.C, Label = NodeLabel.Partial };
        // PCNode c2 = new PCNode() { Type = NodeType.C, Label = NodeLabel.Empty };
        PCNode p1 = new PCNode() { Type = NodeType.P };
        PCNode p2 = new PCNode() { Type = NodeType.P };
        PCNode p3 = new PCNode() { Type = NodeType.P };
        PCNode p4 = new PCNode() { Type = NodeType.P };
        PCNode p5 = new PCNode() { Type = NodeType.P };
        PCNode p6 = new PCNode() { Type = NodeType.P };
        PCNode p7 = new PCNode() { Type = NodeType.P };
        PCNode c1 = new PCNode() { Type = NodeType.C };
        PCNode c2 = new PCNode() { Type = NodeType.C };
        List<PCNode> leaves = new List<PCNode>();
        // leaves.Add(PCNodesProvider.FullLeafWithColumn(0));
        // leaves.Add(PCNodesProvider.FullLeafWithColumn(1));
        // leaves.Add(PCNodesProvider.FullLeafWithColumn(2));
        // leaves.Add(PCNodesProvider.EmptyLeafWithColumn(3));
        // leaves.Add(PCNodesProvider.FullLeafWithColumn(4));
        // leaves.Add(PCNodesProvider.FullLeafWithColumn(5));
        // leaves.Add(PCNodesProvider.FullLeafWithColumn(6));
        // leaves.Add(PCNodesProvider.EmptyLeafWithColumn(7));
        // leaves.Add(PCNodesProvider.FullLeafWithColumn(8));
        // leaves.Add(PCNodesProvider.EmptyLeafWithColumn(9));
        // leaves.Add(PCNodesProvider.EmptyLeafWithColumn(10));
        // leaves.Add(PCNodesProvider.EmptyLeafWithColumn(11));
        // leaves.Add(PCNodesProvider.FullLeafWithColumn(12));
        // leaves.Add(PCNodesProvider.FullLeafWithColumn(13));
        // leaves.Add(PCNodesProvider.EmptyLeafWithColumn(14));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(0));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(1));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(2));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(3));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(4));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(5));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(6));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(7));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(8));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(9));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(10));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(11));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(12));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(13));
        leaves.Add(PCNodesProvider.UnlabeledLeafWithColumn(14));
        
        p1.AppendNeighbour(p2);
        p1.Parent = p2;
        p1.AppendNeighbour(leaves[8]);
        leaves[8].Parent = p1;
        leaves[8].AppendNeighbour(p1);
        p1.AppendNeighbour(leaves[7]);
        leaves[7].Parent = p1;
        leaves[7].AppendNeighbour(p1);
        p1.AppendNeighbour(leaves[6]);
        leaves[6].Parent = p1;
        leaves[6].AppendNeighbour(p1);

        p5.AppendNeighbour(p2);
        p5.Parent = p2;
        p5.AppendNeighbour(leaves[5]);
        leaves[5].Parent = p5;
        leaves[5].AppendNeighbour(p5);
        p5.AppendNeighbour(leaves[4]);
        leaves[4].Parent = p5;
        leaves[4].AppendNeighbour(p5);

        p2.AppendNeighbour(p1);
        p2.AppendNeighbour(p5);
        p2.AppendNeighbour(leaves[3]);
        leaves[3].Parent = p2;
        leaves[3].AppendNeighbour(p2);
        p2.AppendNeighbour(p3);
        p2.Parent = p3;

        p3.AppendNeighbour(p2);
        p3.AppendNeighbour(c1);
        p3.AppendNeighbour(c2);

        c1.AppendNeighbour(p3);
        p3.Parent = c1;
        c1.AppendNeighbour(leaves[2]);
        leaves[2].Parent = c1;
        leaves[2].AppendNeighbour(c1);
        c1.AppendNeighbour(p4);
        p4.Parent = c1;
        p4.AppendNeighbour(c1);
        c1.AppendNeighbour(p6);
        p6.Parent = c1;
        p6.AppendNeighbour(c1);

        p4.AppendNeighbour(leaves[1]);
        leaves[1].Parent = p4;
        leaves[1].AppendNeighbour(p4);
        p4.AppendNeighbour(leaves[0]);
        leaves[0].Parent = p4;
        leaves[0].AppendNeighbour(p4);

        p6.AppendNeighbour(leaves[14]);
        leaves[14].Parent = p6;
        leaves[14].AppendNeighbour(p6);
        p6.AppendNeighbour(p7);
        p7.Parent = p6;
        p7.AppendNeighbour(p6);

        leaves[13].Parent = p7;
        leaves[13].AppendNeighbour(p7);
        p7.AppendNeighbour(leaves[13]);
        leaves[12].Parent = p7;
        leaves[12].AppendNeighbour(p7);
        p7.AppendNeighbour(leaves[12]);

        c2.AppendNeighbour(p3);
        c2.AppendNeighbour(leaves[11]);
        leaves[11].Parent = c2;
        leaves[11].AppendNeighbour(c2);
        c2.AppendNeighbour(leaves[10]);
        leaves[10].Parent = c2;
        leaves[10].AppendNeighbour(c2);
        c2.AppendNeighbour(leaves[9]);
        leaves[9].Parent = c2;
        leaves[9].AppendNeighbour(c2);

        PCTree tree = PCTree.TestInstance(leaves, matrix, 1, 15);

        return tree;
    }
}