using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using IrregularVerbs.Models;

namespace IrregularVerbs.Services;

public class CacheService : AppDataService
{
    private const string CacheFolderName = "Cache";
    private const string TermPrioritiesFileName = "term_priorities.bin";

    private TermPriorities _termPriorities;
    
    private async Task LoadPriorityCacheAsync()
    {
        string fullFileName = Path.Combine(AppDirectoryInfo.FullName, CacheFolderName, TermPrioritiesFileName);

        if (!File.Exists(fullFileName))
        {
            _termPriorities = new TermPriorities();
        }
        else
        {
            try
            {
                byte[] bytes = await File.ReadAllBytesAsync(fullFileName);
                _termPriorities = Deserialize<TermPriorities>(bytes);
            }
            catch (Exception ex)
            {
                _termPriorities = new TermPriorities();
                File.Delete(fullFileName);
            }
        }
    }
    
    private T Deserialize<T>(byte[] param)
    {
        using (MemoryStream ms = new MemoryStream(param))
        {
            return (T)new BinaryFormatter().Deserialize(ms);
        }
    }

}