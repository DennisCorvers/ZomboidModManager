using Newtonsoft.Json;
using System;

namespace ZomboidModPicker.Repository
{
    public class ModInfo
    {
        public string[] ModIds { get; }

        public string WorkshopId { get; }

        [JsonIgnore]
        public string ModIdsString
            => string.Join(';', ModIds);

        public ModInfo(string modids, string workshopid)
            : this(modids.Split(';'), workshopid)
        { }

        [JsonConstructor]
        public ModInfo(string[] modids, string workshopid)
        {
            if (modids.Length == 0)
            {
                throw new ArgumentException("Mod requires at least one mod Id.");
            }

            ModIds = modids;
            WorkshopId = workshopid;
        }
    }
}
