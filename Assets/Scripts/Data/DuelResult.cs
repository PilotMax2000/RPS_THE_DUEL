
using DuelsRPG;

public struct DuelResult
{
    public Result Result;
    public Fighter Winner;

    public DuelResult(Result result, Fighter character)
    {
        Result = result;
        Winner = character;
    }

    public DuelResult(Result result)
    {
        Result = result;
        Winner = null;
    }
}

