using Kontur.Sokoban.Game;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Kontur.Sokoban.Services
{
    public class GamesRepo
    {
        private readonly Dictionary<Guid, SokobanField> gamesCollection;

        public static GamesRepo Instance { get; private set; }

        private GamesRepo()
        {
            gamesCollection = new Dictionary<Guid, SokobanField>();
        }

        static GamesRepo()
        {
            Instance = new GamesRepo();
        }

        public bool ContainsGame(Guid id)
        {
            return gamesCollection.ContainsKey(id);
        }

        public SokobanField NewGame(Guid id, string level)
        {
            var game = new SokobanField(LevelRepo.Instance.GetLevel(level));
            gamesCollection.Add(id, game);
            return game;
        }

        public SokobanField GetGame(Guid id)
        {
            if (!gamesCollection.ContainsKey(id))
            {
                return null;
            }
            return gamesCollection[id];
        }
    }
}