using DOCSAN.APPLICATION.Messages.Common.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOCSAN.APPLICATION.Interfaces
{
    public interface IAwsS3Service
    {
        Task<FileUploadResponse> UploadFileAsync(IFormFile file,string folder, string imageName);

        Task<MultiFileUploadResponse> UploadMultiFilesAsync(List<IFormFile> files, string folder,string imageName);

        Task<FileUploadResponse> DeleteObjectFromS3Async(string completeFileName);
    }
}
