using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Game
{
    public ReactiveProperty<GameState> State { get; set; } 
    public ReactiveProperty<int> Gold { get; set; }
    public Dictionary<int, int> CharactersLevels { get; set; } = new Dictionary<int, int>();


    private Game() {}

    //public Game(int gold = 0, GameState state = GameState.Idle, CharacterInfo[] characters = null)
    //{
    //    Gold = new ReactiveProperty<int>(gold);
    //    State = new ReactiveProperty<GameState>(state);
    //    CharactersLevels = characters;
    //}

    public Game(ISaveService saveService)
    {
        GameOptions options = saveService.Load();
        if (options == null)
        {
            options = new GameOptions(0, new CharacterInfo[]
            {
                new CharacterInfo(1, 1),
                new CharacterInfo(2, 1),
                new CharacterInfo(3, 1)
            });
        }

        State = new ReactiveProperty<GameState>(GameState.Idle);
        Gold = new ReactiveProperty<int>(options.Gold);
        foreach (var info in options.Characters)
        {
            CharactersLevels.Add(info.Type, info.Level);
        }
    }

    [Serializable]
    public class GameOptions
    {
        public int Gold { get; set; }
        public CharacterInfo[] Characters { get; set; }

        public GameOptions(int gold, CharacterInfo[] infos)
        {
            Gold = gold;
            Characters = infos;
        }
    }

    [Serializable]
    public class CharacterInfo
    {
        public int Type { get; set; }
        public int Level { get; set; }

        public CharacterInfo(int type, int level)
        {
            Level = level;
            Type = type;
        }
    }

    public enum GameState
    {
        Idle,
        Play
    }
}
