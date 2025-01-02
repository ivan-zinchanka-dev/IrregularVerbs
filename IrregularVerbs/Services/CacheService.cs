using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using IrregularVerbs.Models;

namespace IrregularVerbs.Services;

public class CacheService : AppDataService
{
    private const string CacheFolderName = "Cache";
    private const string TermPrioritiesFileName = "term_priorities.bin";

    private DirectoryInfo _cacheDirectoryInfo;
    public TermPriorities TermPriorities { get; private set; }

    public CacheService()
    {
        CheckCacheFolder();
    }
    
    public async Task InitializeAsync()
    {
        await LoadPriorityCacheAsync();
    }
    
    private void CheckCacheFolder()
    {
        string path = Path.Combine(AppDirectoryInfo.FullName, CacheFolderName);
        _cacheDirectoryInfo = new DirectoryInfo(path);
        
        if (!_cacheDirectoryInfo.Exists)
        {
            _cacheDirectoryInfo.Create();
        }
    }
    
    private async Task LoadPriorityCacheAsync()
    {
        string fullFileName = Path.Combine(_cacheDirectoryInfo.FullName, TermPrioritiesFileName);

        if (!File.Exists(fullFileName))
        {
            TermPriorities = new TermPriorities();
        }
        else
        {
            try
            {
                byte[] bytes = await File.ReadAllBytesAsync(fullFileName);
                TermPriorities = (TermPriorities)Deserialize(bytes);
            }
            catch (Exception ex)
            {
                TermPriorities = new TermPriorities();
                File.Delete(fullFileName);
            }
        }

        TermPriorities.OnChanged += SavePriorityCacheAsync;
    }

    private async void SavePriorityCacheAsync()
    {
        string fullFileName = Path.Combine(_cacheDirectoryInfo.FullName, TermPrioritiesFileName);
        
        byte[] bytes = Serialize(TermPriorities);
        await File.WriteAllBytesAsync(fullFileName, bytes);
    }

    private byte[] Serialize(object source)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            new BinaryFormatter().Serialize(stream, source);
            return stream.ToArray();
        }
    }
    
    private object Deserialize(byte[] source)
    {
        using (MemoryStream stream = new MemoryStream(source))
        {
            return new BinaryFormatter().Deserialize(stream);
        }
    }
    
}