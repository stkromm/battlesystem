using UnityEngine;
using System.IO;



public class FileManager : MonoBehaviour
{
    private static FileManager _instance;		// Instance of the fileManager
    public string Path { get; private set; }					// Holds the application Path

    public static FileManager Instance
    {
        get { return _instance ?? (_instance = new GameObject("FileManager").AddComponent<FileManager>()); }
    }


    public void OnApplicationQuit()
    {
        _instance = null;
    }

    public void Initialize()
    {
        Path = Application.dataPath;
        // Check for and create the gamedata directory
        if (CheckDirectory("gamedata") == false)
        {
            _createDirectory("gamedata");
        }
    }

    public bool CheckFile(string filePath)
    {
        return File.Exists(Path + "/" + filePath);
    }

    public void CreateFile(string directory, string filename, string filetype, string fileData)
    {
        if (CheckDirectory(directory) != true) return;
        if (CheckFile(directory + "/" + filename + "." + filetype) == false)
        {
            File.WriteAllText(Path + "/" + directory + "/" + filename + "." + filetype, fileData);
        }
    }

    public bool CheckDirectory(string directory)
    {
        return Directory.Exists(Path + "/" + directory);
    }

    private void _createDirectory(string directory)
    {
        if (CheckDirectory(directory) == false)
        {
            Directory.CreateDirectory(Path + "/" + directory);
        }
    }


    private void _deleteDirectory(string directory)
    {
        if (CheckDirectory(directory))
        {
            Directory.Delete(Path + "/" + directory, true);
        }
    }

    private void _moveDirectory(string originalDestination, string newDestination)
    {
        if (CheckDirectory(originalDestination) && CheckDirectory(newDestination) == false)
        {
            Directory.Move(Path + "/" + originalDestination, Path + "/" + newDestination);
        }
    }

    public string[] FindSubDirectories(string directory)
    {
        var subdirectoryList = Directory.GetDirectories(Path + "/" + directory);
        return subdirectoryList;
    }

    public string[] FindFiles(string directory)
    {
        var fileList = Directory.GetFiles(Path + "/" + directory);
        return fileList;
    }

    public string ReadFile(string directory, string filename, string filetype)
    {
        if (CheckDirectory(directory) != true) return null;
        if (CheckFile(directory + "/" + filename + "." + filetype) != true) return null;
        // Read the file
        var fileContents = File.ReadAllText(Path + "/" + directory + "/" + filename + "." + filetype);
        return fileContents;
    }

    public void DeleteFile(string filePath)
    {
        if (File.Exists(Path + "/" + filePath))
        {
            File.Delete(Path + "/" + filePath);
        }
    }

    public enum FileUpdateMode { Replace, Append }

    public void UpdateFile(string directory, string filename, string filetype, string fileData, FileUpdateMode mode)
    {
        if (!CheckDirectory(directory)) return;
        if (!CheckFile(directory + "/" + filename + "." + filetype)) return;
        if (mode == FileUpdateMode.Replace)
        {
            File.WriteAllText(Path + "/" + directory + "/" + filename + "." + filetype, fileData);
        }
        if (mode == FileUpdateMode.Append)
        {
            File.AppendAllText(Path + "/" + directory + "/" + filename + "." + filetype, fileData);
        }
    }

}
