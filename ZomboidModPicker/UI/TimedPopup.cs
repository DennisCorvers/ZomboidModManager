using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ZomboidModPicker.UI
{
    internal class TimedPopup
    {
        private readonly Popup m_popup;

        public TimedPopup(string text)
        {
            var tb = new TextBlock()
            {
                Text = text,
                Background = Brushes.MintCream
            };

            m_popup = new Popup()
            {
                Child = tb
            };
        }

        public async Task Show(int delay)
        {
            m_popup.Placement = PlacementMode.Mouse;
            m_popup.IsOpen = true;

            await Task.Delay(delay);
            m_popup.IsOpen = false;
        }
    }
}
