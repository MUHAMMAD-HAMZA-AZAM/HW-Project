using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.Services
{
  public interface IAWSS3FileService
  {
    Task<bool> UploadFile(string uploadFileName);
    Task<List<string>> FilesList();
    Task<AwsImage> GetFile(string key);
    Task<bool> UpdateFile(string uploadFileName, string key);
    Task<bool> DeleteFile(string key);
  }
  public class AWSS3FileService : IAWSS3FileService
  {
    private readonly IAWSS3BucketHelper _AWSS3BucketHelper;

    public AWSS3FileService(IAWSS3BucketHelper AWSS3BucketHelper)
    {
      this._AWSS3BucketHelper = AWSS3BucketHelper;
    }
    public async Task<bool> UploadFile(string uploadFileName)
    {
      try
      {
        var path = Path.Combine("Files", uploadFileName.ToString() + ".png");
        using (FileStream fsSource = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
          string fileExtension = Path.GetExtension(path);
          string fileName = string.Empty;
          fileName = $"{DateTime.Now.Ticks}{fileExtension}";
          return await _AWSS3BucketHelper.UploadFile(fsSource, fileName);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public async Task<List<string>> FilesList()
    {
      try
      {
        ListVersionsResponse listVersions = await _AWSS3BucketHelper.FilesList();
        return listVersions.Versions.Select(c => c.Key).ToList();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public async Task<AwsImage> GetFile(string key)
    {
      AwsImage awsImage = new AwsImage();
      try
      {
        awsImage = await _AWSS3BucketHelper.GetFile(key);
        if (awsImage == null)
        {
          return null;
        }
        else
        {
          return awsImage;
        }
      }
      catch (Exception ex)
      {
            return null;
      }
    }
    public async Task<bool> UpdateFile(string uploadFileName, string key)
    {
      try
      {
        var path = Path.Combine("Files", uploadFileName.ToString() + ".png");
        using (FileStream fsSource = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
          return await _AWSS3BucketHelper.UploadFile(fsSource, key);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    public async Task<bool> DeleteFile(string key)
    {
      try
      {
        return await _AWSS3BucketHelper.DeleteFile(key);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
