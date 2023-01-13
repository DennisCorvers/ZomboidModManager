using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZomboidModPicker.Utilities;

namespace ZomboidModPicker.Repository
{
    [Serializable]
    public class Repository
    {
        public IReadOnlyList<ModInfo> Mods
            => m_mods;

        private readonly List<ModInfo> m_mods;

        public Repository()
        {
            m_mods = new List<ModInfo>();
        }

        [JsonConstructor]
        private Repository(List<ModInfo> mods)
        {
            m_mods = mods;
        }

        public bool Insert(ModInfo modInfo)
            => Insert(modInfo, -1);

        public bool Insert(ModInfo modInfo, int index)
        {
            var i = m_mods.FindIndex(x => string.Equals(modInfo.WorkshopId, x.WorkshopId, StringComparison.OrdinalIgnoreCase));

            // Add at the end.
            if (index < 0)
            {
                if (i != -1)
                {
                    return false;
                }
                else
                {
                    m_mods.Add(modInfo);
                    return true;
                }
            }
            // Replace the index
            else
            {
                if (index != i)
                {
                    return false;
                }
                else
                {
                    m_mods[index] = modInfo;
                    return true;
                }
            }
        }

        public bool Remove(ModInfo modInfo)
            => m_mods.Remove(modInfo);

        public bool ShiftItem(int index)
        {
            var absIndex = Math.Abs(index);
            return index < 0 ?
                m_mods.MoveUp(absIndex) :
                m_mods.MoveDown(absIndex);
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
            sb.Append("WorkshopItems=");

            sb.Append(string.Join(';', Mods.Select(x => x.WorkshopId)));
            return sb.ToString();
        }
    }
}
