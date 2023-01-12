using System.Diagnostics;
using System.Linq;
using ZomboidModPicker.Repository;

namespace ZomboidModPicker.Utilities
{
    public static class Web
    {
        public static void NavigateToWorkshop(ModInfo mod)
        {
            var url = $"https://steamcommunity.com/sharedfiles/filedetails/?id={mod.WorkshopId}";
            _ = Process.Start(new ProcessStartInfo(url)
            {
                UseShellExecute = true
            });
        }
    }
}
