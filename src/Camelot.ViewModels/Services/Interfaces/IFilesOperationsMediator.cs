using Camelot.ViewModels.Interfaces.MainWindow;

namespace Camelot.ViewModels.Services.Interfaces
{
    public interface IFilesOperationsMediator
    {
        IFilesPanelViewModel ActiveFilesPanelViewModel { get; }
        
        IFilesPanelViewModel InactiveFilesPanelViewModel { get; }
        
        string OutputDirectory { get; }

        void Register(IFilesPanelViewModel activeFilesPanelViewModel, IFilesPanelViewModel inactiveFilesPanelViewModel);
    }
}