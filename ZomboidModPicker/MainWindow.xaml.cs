using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ZomboidModPicker.Repository;
using ZomboidModPicker.UI;
using ZomboidModPicker.Utilities;

namespace ZomboidModPicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Repository.Repository m_data;

        private readonly KeydownHandler<DataGrid> m_gridKeyHandler;
        private NewItemForm m_newItemForm;
        private NewItemForm NewItemForm
        {
            get
            {
                if (m_newItemForm == null)
                    m_newItemForm = new NewItemForm();
                return m_newItemForm;
            }
        }

        public MainWindow()
        {
            m_data = new Repository.Repository();

            InitializeComponent();

            m_gridKeyHandler = new KeydownHandler<DataGrid>(ModGrid);
        }


        private void ReloadUI(Repository.Repository data)
        {
            ModGrid.Items.Clear();

            m_data = data;
            foreach (var mod in m_data.Mods)
            {
                _ = ModGrid.Items.Add(mod);
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var state = Persistence.TryOpenFile(out var data);
                if (state.Success)
                {
                    Title = state.FileName;
                    ReloadUI(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to open file: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var state = Persistence.TrySaveFile(m_data);
                if (state.Success)
                {
                    Title = state.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to save data: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetSelectedItem(out var modInfo))
            {
                ModGrid.Items.Remove(modInfo);
                m_data.Remove(modInfo);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = NewItemForm;
            var selectedMod = ModGrid.SelectedItem as ModInfo;

            form.SetWindow(selectedMod);
            form.ShowDialog();

            if (form.HasUpdate)
            {
                if (m_data.Insert(form.TempMod, ModGrid.SelectedIndex))
                {
                    ReloadUI(m_data);
                }
                else
                {
                    MessageBox.Show($"Mod with Workshop ID {form.TempMod.WorkshopId} already added.",
                        "Duplicate ID", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            if (!TryGetSelectedItem(out _))
                return;

            var selectedIndex = ModGrid.SelectedIndex;
            if (m_data.ShiftItem(-selectedIndex))
            {
                ReloadUI(m_data);
                ModGrid.SelectedIndex = selectedIndex - 1;
            }
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if (!TryGetSelectedItem(out _))
                return;

            var selectedIndex = ModGrid.SelectedIndex;
            if (m_data.ShiftItem(ModGrid.SelectedIndex))
            {
                ReloadUI(m_data);
                ModGrid.SelectedIndex = selectedIndex + 1;
            }
        }

        private void btnCopyIds_Click(object sender, RoutedEventArgs e)
            => Clipboard.SetText(m_data.ExportIds());

        private void btnCopyNames_Click(object sender, RoutedEventArgs e)
            => Clipboard.SetText(m_data.ExportNames());

        private void btnWorkshop_Click(object sender, RoutedEventArgs e)
        {
            if (TryGetSelectedItem(out ModInfo modInfo))
            {
                Web.NavigateToWorkshop(modInfo);
            }
        }

        private bool TryGetSelectedItem(out ModInfo? modInfo)
        {
            var selection = ModGrid.SelectedItems;
            if (selection.Count == 0)
            {
                MessageBox.Show("No item selected.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                modInfo = null;
                return false;
            }
            else
            {
                modInfo = (ModInfo)ModGrid.SelectedItem;
                return true;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            NewItemForm.Close();
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
    }
}
