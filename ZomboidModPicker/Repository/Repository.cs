using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZomboidModPicker.Repository
{
    [Serializable]
    public class Repository
    {
        public List<ModInfo> Mods { get; }

        public Repository()
        {
            Mods = new List<ModInfo>();
        }

        [JsonConstructor]
        private Repository(List<ModInfo> mods)
        {
            Mods = mods;
        }

        public string ExportNames()
        {
            var sb = new StringBuilder();
            sb.Append("Mods=");

            var modIds = Mods.Select(x => x.ModIdsString);
            sb.Append(string.Join(';', modIds));
            return sb.ToString();
        }

        public string ExportIds()
        {
            var sb = new StringBuilder();
            sb.Append("Workshopitems=");

            sb.Append(string.Join(';', Mods.Select(x => x.WorkshopId)));
            return sb.ToString();
        }
    }
}
