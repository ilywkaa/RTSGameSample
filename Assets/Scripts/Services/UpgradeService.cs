public class UpgradeService : IUpgradeService
{
    private readonly int delta;


    public UpgradeService(int deltaUpgradeValue)
    {
        delta = deltaUpgradeValue;
    }

    public int UpgradeCharacter(int currentValue)
    {
        return currentValue + delta;
    }
}