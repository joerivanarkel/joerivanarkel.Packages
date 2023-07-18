using joerivanarkel.Core.Logger.Enum;
using joerivanarkel.Core.Logger.Exception;
using joerivanarkel.Core.Logger.Interfaces;

namespace joerivanarkel.Core.Logger.FileHandlers;

/// <summary>
/// This class handles reading files.
/// </summary>
public class FileReadHandler : BaseFileHandler, IFileReadHandler
{
    public async Task<string> ReadAllTextFromFile(string name, FileExtension extension, string location)
    {
        if (name == string.Empty) throw new FileHandlerException($"Name, ${name} is empty");
        if (location == string.Empty) throw new FileHandlerException($"Location, ${location} is empty");

        var nameAndExtension = name + GetFileExtension(extension);
        var targetFolder = Path.Combine(Environment.CurrentDirectory, location);
        string fileName = Path.Combine(targetFolder, nameAndExtension);

        return FileExists(name, extension, location)
            ? await File.ReadAllTextAsync(fileName)
            : throw new FileNotFoundException("File not found");
    }

    public bool FileExists(string name, FileExtension extension, string location)
    {
        if (name == string.Empty) throw new FileHandlerException($"Name, ${name} is empty");
        if (location == string.Empty) throw new FileHandlerException($"Location, ${location} is empty");

        name += GetFileExtension(extension);
        var targetFolder = Path.Combine(Environment.CurrentDirectory, location);
        string fileName = Path.Combine(targetFolder, name);

        return File.Exists(fileName);
    }
    public bool DirectoryExists(string location)
    {
        if (location == string.Empty) throw new FileHandlerException($"Location, ${location} is empty");

        var targetFolder = Path.Combine(Environment.CurrentDirectory, location);

        return Directory.Exists(targetFolder);
    }
}