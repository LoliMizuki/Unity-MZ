using System.IO;
using System.Collections;

public static partial class MZ {

    public interface IProjectTemplateDelgete {
        string NewFilePath();

        string OpenPath();

        string SaveContent();

        void OpenActionWithFileContent(string content);

        void UnloadAction();

        void RefreshScreenAction();
    }
    
    public class ProjectTemplate {
    
        public static ProjectTemplate ProjectTemplateWithDelegate(IProjectTemplateDelgete projectManageDelgete) {
            ProjectTemplate projectManage = new ProjectTemplate();
    
            projectManage._currentProjectPath = null;
            projectManage._projectManageDelgete = projectManageDelgete;
    
            return projectManage;
        }
    
        public string currentProjectPath {
            get {
                return _currentProjectPath;
            }
        }
    
        public string projectName {
            get {
                if (hasWorkingProject == false) {
                    return null;
                }
                return Path.GetFileNameWithoutExtension(currentProjectPath);
            }
        }
    
        public void New() {
            string path = _projectManageDelgete.NewFilePath();
            if (IsVaildedFilePath(path) == false) {
                return;
            }
                 
            _currentProjectPath = path;
            CreateEmptyFileWithPath(_currentProjectPath, true);
        }
    
        public void Open() {
            string path = _projectManageDelgete.OpenPath();
            OpenWithPath(path);
        }
    
        public void OpenWithPath(string path) {
            if (File.Exists(path) == false) {
                return;
            }
    
            _projectManageDelgete.UnloadAction();
            _currentProjectPath = path;
            _projectManageDelgete.OpenActionWithFileContent(File.ReadAllText(path));
        }
    
        public void Save() {
            if (hasWorkingProject == false) {
                New();
            }
    
            if (!IsVaildedFilePath(_currentProjectPath)) {
                return;
            }
    
            File.WriteAllText(_currentProjectPath, _projectManageDelgete.SaveContent());
            _projectManageDelgete.RefreshScreenAction();
        }
    
        public void SaveAs() {
            New();
            Save();
            _projectManageDelgete.RefreshScreenAction();
        }
    
        public void Unload() {
            _projectManageDelgete.UnloadAction();
            _currentProjectPath = null;
        }
    
        public bool hasWorkingProject {
            get {
                return IsPathVaild(_currentProjectPath);
            }
        }
    
        #region - private 
    
        IProjectTemplateDelgete _projectManageDelgete;
        string _currentProjectPath;
    
        FileStream CreateEmptyFileWithPath(string path, bool close) {
            if (IsVaildedFilePath(path)) {
                return null;
            }
    
            if (File.Exists(path)) {
                File.Delete(path);
            }
    
            FileStream fileStream = File.Create(path);
    
            if (close) {
                fileStream.Close();
                return null;
            }
    
            _projectManageDelgete.RefreshScreenAction();
    
            return fileStream;
        }
    
        bool IsVaildedFilePath(string path) {
            string fileName = Path.GetFileName(path);
            string fileExtension = Path.GetExtension(path);
            return (fileName != null && fileExtension != null && fileName.Length > 0 && fileExtension.Length > 0);
        }
    
        bool IsPathVaild(string path) {
            return (path != null && path.Length > 0);
        }
    
        #endregion
    }
}