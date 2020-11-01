
public interface ISaveService
{
    void Save(Game.GameOptions options);
    Game.GameOptions Load();
}