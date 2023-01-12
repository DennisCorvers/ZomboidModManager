using Newtonsoft.Json;
using System;

namespace ZomboidModPicker.Repository
{
    public class ModInfo
    {
        public string Name { get; }

        public string[] Ids { get; }

        [JsonIgnore]
        public string IdsString
            => string.Join(';', Ids);

        public ModInfo(string name, string ids)
            : this(name, ids.Split(';'))
        { }

        [JsonConstructor]
        public ModInfo(string name, string[] ids)
        {
            if (ids.Length == 0)
            {
                throw new ArgumentException("Mod requires at least one workshop Id.");
            }

            Name = name;
            Ids = ids;
        }
    }
}
