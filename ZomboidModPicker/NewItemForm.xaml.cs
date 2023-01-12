using System.ComponentModel;
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
        internal ModInfo TempMod { get; private set; }
        internal bool HasUpdate { get; private set; }

        public NewItemForm()
        {
            InitializeComponent();
        }

        public void SetWindow(ModInfo modInfo)
        {
            TempMod = new ModInfo(modInfo.Name, modInfo.Ids);
            TbModName.Text = TempMod.Name;
            TbModId.Text = TempMod.IdsString;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = TbModName.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Mod must have a name.", "Invalid name", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string idText = TbModId.Text;
            if (string.IsNullOrWhiteSpace(idText))
            {
                MessageBox.Show("Mod Id must contain at least one Id.", "Invalid Id", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TempMod = new ModInfo(name, idText);
            HasUpdate = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            TempMod = null;
            HasUpdate = false;

            Close();
        }

        private void Window_Loaded(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!((Window)sender).IsVisible)
                return;

            HasUpdate = false;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            TbModId.Text = "";
            TbModName.Text = "";

            e.Cancel = true;
            FocusManager.SetFocusedElement(this, TbModName);
            Visibility = Visibility.Hidden;
        }
    }
}
