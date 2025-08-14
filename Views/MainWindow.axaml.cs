using Avalonia.ReactiveUI;
using CustomMVVMDialogs.ViewModels;
using ReactiveUI;

namespace CustomMVVMDialogs.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                d(ViewModel!.OpenFolderInteraction!.RegisterHandler(async interaction =>
                {
                    var output = await interaction.Input.ShowAsync(this);
                    interaction.SetOutput(output);
                }));

                d(ViewModel!.SaveFileInteraction!.RegisterHandler(async interaction =>
                {
                    var output = await interaction.Input.ShowAsync(this);
                    interaction.SetOutput(output);
                }));

                d(ViewModel!.OpenFileInteraction!.RegisterHandler(async interaction =>
                {
                    var output = await interaction.Input.ShowAsync(this);
                    interaction.SetOutput(output);
                }));

                d(this.OneWayBind(ViewModel, vm => vm.OpenFolderPath, view => view.txtFolder.Text));
                d(this.OneWayBind(ViewModel, vm => vm.SaveFilePath, view => view.txtSaveFile.Text));
                d(this.OneWayBind(ViewModel, vm => vm.OpenFilePaths, view => view.cmbOpenFile.ItemsSource));
                d(this.OneWayBind(ViewModel, vm => vm.SelectedFile, view => view.cmbOpenFile.SelectedItem));

                d(this.BindCommand(ViewModel, vm => vm.OpenFolderCmd, view => view.btnBrowseFolder));
                d(this.BindCommand(ViewModel, vm => vm.SaveFileDialogCmd, view => view.btnBrowseSaveFile));
                d(this.BindCommand(ViewModel,vm => vm.OpenFileDialogCmd,view => view.btnBrowseOpenFiles));
            });
        }
    }
}