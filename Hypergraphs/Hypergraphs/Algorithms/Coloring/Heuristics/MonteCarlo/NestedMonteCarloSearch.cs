namespace Hypergraphs.Algorithms;

public class NestedMonteCarloSearch
{
	private int[] _policy;

	public NestedMonteCarloSearch()
	{
		
	}
    /*
     *
     *
playout(state, policy) {
	sequence = {};
	while (true) {
		if (state is terminal) {
			return (score(state), sequence);
		}
		double z = 0.0;
		for (m in possible moves for state) {
			z += exp(policy[m]);
		}
		choose a move m with probability (exp(policy[m]))/z;
		state = play(state, m);
		sequence.Add(m);
	}
}

NMCS(state, level) {
	if (level == 0) {
		// symulacja -> losowe kolorowanie zachlanne?
		return playout(state, uniform);
	}
	bestSequenceOfLevel = {};
	// terminal state bedzie prawdopodobnie pokolorowaniem wszystkich wierzchołków
	// state is terminal -> znaleziono k-pokolorowanie
	while (state is not terminal) {
		for (m in possible moves for state) {
			// generujemy stan s na podstawie stanu state i ruchu m
			s = play(state, m);
			NMCS(s, level-1);
			// dopisz ruch do listy wierzchołków
			update bestSequenceOfLevel;
		}
		bestMove = move of the BestSequenceOfLevel;
		state = play(state, bestMove);
	}
}
     *
     * 
     */


    public void Execute()
    {
	    
    }
    
}