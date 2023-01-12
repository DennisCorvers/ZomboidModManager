using System.Diagnostics;
using System.Linq;
using ZomboidModPicker.Repository;

namespace ZomboidModPicker.Utilities
{
    public static class Web
    {
        public static void NavigateToWorkshop(ModInfo mod)
        {
            var workshopId = mod.Ids.First();
            var url = $"https://steamcommunity.com/sharedfiles/filedetails/?id={workshopId}";
            _ = Process.Start(new ProcessStartInfo(url)
            {
                UseShellExecute = true
            });
        }
    }
}
