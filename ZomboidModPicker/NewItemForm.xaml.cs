using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ZomboidModPicker.Repository;

namespace ZomboidModPicker
{
    /// <summary>
    /// Interaction logic for NewItemForm.xaml
    /// </summary>
    public partial class NewItemForm : Window
    {
        internal ModInfo? TempMod { get; private set; }
        internal bool HasUpdate { get; private set; }

        public NewItemForm()
        {
            InitializeComponent();
        }

        public void SetWindow(ModInfo? modInfo)
        {
            HasUpdate = false;
            if (modInfo == null)
            {
                Title = "Adding new mod";
                TempMod = null;
                TbModName.Text = string.Empty;
                TbModId.Text = string.Empty;
            }
            else
            {
                Title = $"Editing '{modInfo.ModIds.First()}'";
                TempMod = new ModInfo(modInfo.ModIdsString, modInfo.WorkshopId);
                TbModName.Text = TempMod.ModIdsString;
                TbModId.Text = TempMod.WorkshopId;
            }
        }

        private void Add()
        {
            string name = TbModName.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Mod ID must contain at least one ID.", "Invalid name", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string idText = TbModId.Text;
            if (string.IsNullOrWhiteSpace(idText))
            {
                MessageBox.Show("Workshop ID may not be empty.", "Invalid Id", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TempMod = new ModInfo(name, idText);
            HasUpdate = true;
            Close();
        }

        private void Cancel()
        {
            TempMod = null;
            HasUpdate = false;

            Close();
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Add();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
            => Add();

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
            => Cancel();

        private void Window_Loaded(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!((Window)sender).IsVisible)
                return;

            HasUpdate = false;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            FocusManager.SetFocusedElement(this, TbModName);
            Visibility = Visibility.Hidden;
        }
    }
}
