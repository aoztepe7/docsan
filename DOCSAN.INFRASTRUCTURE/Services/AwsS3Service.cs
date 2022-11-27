using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using DOCSAN.APPLICATION.Exceptions;
using DOCSAN.APPLICATION.Interfaces;
using DOCSAN.APPLICATION.Messages.Common.Response;
using DOCSAN.SHARED.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;

namespace DOCSAN.INFRASTRUCTURE.Services
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly AwsConfig _awsConfig;
        private readonly AmazonS3Config _amazonConfig;

        public AwsS3Service(IOptions<AwsConfig> awsConfig)
        {
            _awsConfig = awsConfig.Value;
            _amazonConfig = new AmazonS3Config();
            _amazonConfig.ServiceURL = _awsConfig.ServiceUrl;
        }

        public async Task<FileUploadResponse> DeleteObjectFromS3Async(string completeFileName)
        {
            try
            {
                var replacedName = completeFileName.Replace(_awsConfig.FullImagePath, "");
                var request = new GetObjectRequest { BucketName = _awsConfig.BucketName, Key = replacedName };

                using (var client = new AmazonS3Client(_awsConfig.AccessKey, _awsConfig.SecretKey, _amazonConfig))
                {
                    var response = await client.GetObjectAsync(request);

                    if (response == null || response.HttpStatusCode != HttpStatusCode.OK)
                        return new FileUploadResponse() { Success = false, Url = String.Empty };

                    await client.DeleteObjectAsync(_awsConfig.BucketName, replacedName);
                }
                return new FileUploadResponse() { Success = true, Url = String.Empty };
            }
            catch (AmazonS3Exception e)
            {
                return new FileUploadResponse() { Success = true, Url = String.Empty };
            }
            catch (Exception e)
            {
                return new FileUploadResponse() { Success = true, Url = String.Empty };
            }
        }

        public async Task<FileUploadResponse> UploadFileAsync(IFormFile file, string folder, string imageName)
        {
            var folderCheckIsSucceed = await CheckAndCreateFolderIfNotExist(folder);
            if (!folderCheckIsSucceed)
                throw new InternalServerException();

            try
            {
                using (var client = new AmazonS3Client(_awsConfig.AccessKey, _awsConfig.SecretKey, _amazonConfig))
                {
                    using (var newMemoryStream = new MemoryStream())
                    {
                        file.CopyTo(newMemoryStream);

                        var completeFileName = folder + "/" +imageName+ "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var uploadRequest = new TransferUtilityUploadRequest
                        {
                            InputStream = newMemoryStream,
                            Key = completeFileName,
                            BucketName = _awsConfig.BucketName,
                            CannedACL = S3CannedACL.PublicRead
                        };

                        var fileTransferUtility = new TransferUtility(client);
                        await fileTransferUtility.UploadAsync(uploadRequest);
                        return new FileUploadResponse() { Success = true, Url = _awsConfig.FullImagePath + completeFileName };
                    }
                }
            }
            catch (Exception e)
            {
                throw new InternalServerException();
            }
        }

        public Task<MultiFileUploadResponse> UploadMultiFilesAsync(List<IFormFile> files, string folder, string imageName)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> CheckAndCreateFolderIfNotExist(string folderName)
        {
            try
            {
                var findFolderRequest = new ListObjectsV2Request();
                findFolderRequest.BucketName = _awsConfig.BucketName;
                findFolderRequest.Prefix = folderName;
                findFolderRequest.MaxKeys = 1;

                using (var client = new AmazonS3Client(_awsConfig.AccessKey, _awsConfig.SecretKey, _amazonConfig))
                {
                    ListObjectsV2Response findFolderResponse = await client.ListObjectsV2Async(findFolderRequest);
                    if (findFolderResponse.S3Objects.Any())
                        return true;
                    else
                    {
                        PutObjectRequest request = new PutObjectRequest()
                        {
                            BucketName = _awsConfig.BucketName,
                            Key = folderName+"/"
                        };

                        PutObjectResponse response = await client.PutObjectAsync(request);
                        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                            return true;
                        else
                            return false;
                    }
                }
            }
            catch (AmazonS3Exception e)
            {
                throw new InternalServerException();
            }
            catch (Exception e)
            {
                throw new InternalServerException();
            }
        }
    }
}
