using System.Linq;
using System.Collections.Generic;

namespace Minimon.Domain;

public class MoveSet
{
    int levelCache = -1;
    Move[] moveSetCache = [];
    readonly List<(int lv, Move mv)> LearnMovePairs = [];
    
    public Move[] GetMoves(int level)
    {
        if (level == levelCache)
            return moveSetCache;
        
        levelCache = level;
        moveSetCache = [ ..   
            from pair in LearnMovePairs
            where pair.lv <= level
            select pair.mv
        ];
        return moveSetCache;
    }

    public MoveSet Add(Move move, int learnLevel)
    {
        LearnMovePairs.Add((learnLevel, move));
        return this;
    }

    public static MoveSet New() => new();
}