using Kontur.Sokoban.Game.Blocks;
using Kontur.Sokoban.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Kontur.Sokoban.Services
{
    public class LevelRepo
    {
        private Dictionary<string, IEnumerable<KeyValuePair<IBlock, Vec>>> Levels;
        public static LevelRepo Instance { get; private set; }
        public IEnumerable<KeyValuePair<IBlock, Vec>> GetLevel(string level = "Tutorial")
        {
            if (!Levels.ContainsKey(level))
            {
                throw new ArgumentException("there is no such level");
            }
            return Levels[level];
        }

        public string[] GetLevelNames()
        {
            return Levels.Keys.ToArray();
        }

        static LevelRepo()
        {
            Instance = new LevelRepo();
        }

        public bool ContainsLevel(string level)
        {
            return Levels.ContainsKey(level);
        }

        private LevelRepo()
        {
            Levels = new Dictionary<string, IEnumerable<KeyValuePair<IBlock, Vec>>>();
            var files = Directory.EnumerateFiles($".//Levels");
            foreach (var file in files)
            {
                var level = JsonConvert.DeserializeObject<LevelStore>(File.ReadAllText(file));
                Levels[level.Name] = Parse(level);
            }
        }

        private IEnumerable<KeyValuePair<IBlock, Vec>> Parse(LevelStore levelStore)
        {
            var iBlockType = typeof(IBlock);
            var asm = iBlockType.Assembly;
            foreach (var obj in levelStore.Objects)
            {
                var type = asm.GetType($"{iBlockType.Namespace}.{obj.Name}");
                yield return new KeyValuePair<IBlock, Vec>((IBlock)Activator.CreateInstance(type), obj.Pos);
            }
        }

    }
}
