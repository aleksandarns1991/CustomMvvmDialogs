using CustomMVVMDialogs.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace CustomMVVMDialogs.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public Interaction<OpenFolderDialogService, string> OpenFolderInteraction { get; } 
        public Interaction<SaveFileDialogService, string> SaveFileInteraction { get; } 
        public Interaction<OpenFileDialogService, string[]> OpenFileInteraction { get; }
        
        public ReactiveCommand<Unit, Unit> OpenFolderCmd { get; }
        public ReactiveCommand<Unit,Unit> SaveFileDialogCmd { get; }
        public ReactiveCommand<Unit, Unit> OpenFileDialogCmd { get; }

        [Reactive]
        public string? OpenFolderPath { get; set; }
        [Reactive]
        public string? SaveFilePath { get; set; }
        [Reactive]
        public string? SelectedFile { get; set; }

        public ObservableCollection<string> OpenFilePaths { get; } 

        public MainWindowViewModel()
        {
            OpenFolderInteraction = new();
            SaveFileInteraction = new(); 
            OpenFileInteraction = new();

            OpenFilePaths = new();

            OpenFolderCmd = ReactiveCommand.CreateFromTask(OpenFolderAsync);
            SaveFileDialogCmd = ReactiveCommand.CreateFromTask(SaveFileAsync);
            OpenFileDialogCmd = ReactiveCommand.CreateFromTask(OpenFileAsync);
        }

        private async Task OpenFolderAsync()
        {
            var openFolder = new OpenFolderDialogService();
            var output = await OpenFolderInteraction!.Handle(openFolder);

            if (output != null)
            {
                OpenFolderPath = output;
            }
        }

        private async Task OpenFileAsync()
        {
            var openFile = new OpenFileDialogService
            {
                Title = "Open file",
                Extensions = "Textual files:*.txt,*.log|Image files:*.jpg,*.bmp,*.tiff,*.png|Icons:*.ico",
                AllowMultiple = true
            };

            var output = await OpenFileInteraction!.Handle(openFile);

            if (output != null && output.Length > 0)
            {
                OpenFilePaths.Clear();

                foreach(var item in output)
                {
                    OpenFilePaths.Add(item);
                }

                SelectedFile = OpenFilePaths.FirstOrDefault();
            }
        }

        private async Task SaveFileAsync()
        {
            var saveFile = new SaveFileDialogService
            {
                Title = "Save file",
                Extensions = "Textual files:*.txt,*.log"
            };

            var output = await SaveFileInteraction!.Handle(saveFile);

            if (output != null)
            {
                SaveFilePath = output;
            }
        }
    }
}
