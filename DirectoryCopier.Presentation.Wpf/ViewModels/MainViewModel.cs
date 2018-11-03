using FileSearch.Utilities.Wpf;
using FileSearch.Utilities.Wpf.Attributes;
using FileSearch.Utilities.Wpf.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Forms;
using System.Windows.Input;
using DirectoryCopier.Domain;

namespace DirectoryCopier.Presentation.Wpf.ViewModels
{
    public sealed class MainViewModel : ViewModel
    {
        private List<string> files = new List<string>();
        private readonly ICommand browseFromCommand;
        private readonly ICommand browseToCommand;
        private readonly ICommand copyFilesCommand;
        private readonly ICopyManager copyManager;
        private int countCopiedFiles;
        private int countAllFiles;
        private string fromValue = string.Empty;
        private string toValue = string.Empty;

        public MainViewModel(ICopyManager copyManager)
        {
            browseFromCommand = new DelegateCommand(BrowseFrom);
            browseToCommand = new DelegateCommand(BrowseTo);
            copyFilesCommand = new DelegateCommand(CopyFiles, () => CanCopyFiles);
            this.copyManager = copyManager;
        }

        private void CopyFiles()
        {
            CountAllFiles = copyManager.CountOfAllFiles(fromValue);
            CountCopiedFiles = 0;
            copyManager.CopyFileAsync(files, toValue);
        }

        private void BrowseTo()
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                ToValue = dialog.FileName;
            }
        }

        private void BrowseFrom()
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            CommonFileDialogResult result = dialog.ShowDialog();
            if(result == CommonFileDialogResult.Ok)
            {
                FromValue = dialog.FileName;
                files.AddRange(copyManager.GetAllFiles(fromValue));
            }
        }

        public ICommand BrowseFromCommand => browseFromCommand;

        public string FromValue
        {
            get => fromValue;
            set => SetProperty(ref fromValue, value);
        }

        public ICommand BrowseToCommand => browseToCommand;

        public string ToValue
        {
            get => toValue;
            set => SetProperty(ref toValue, value);
        }

        [RaiseCanExecuteDependsUpon(nameof(CanCopyFiles))]
        public ICommand CopyFilesCommand => copyFilesCommand;

        [DependsUponPropertyAttribute(nameof(FromValue))]
        [DependsUponPropertyAttribute(nameof(ToValue))]
        public bool CanCopyFiles => fromValue.Length > 0 && toValue.Length > 0;

        
        public int CountAllFiles
        {
            get => countAllFiles;
            set => SetProperty(ref countAllFiles, value);
        }
        public int CountCopiedFiles
        {
            get => countCopiedFiles;
            set => SetProperty(ref countCopiedFiles, value);
        }
    }
}
