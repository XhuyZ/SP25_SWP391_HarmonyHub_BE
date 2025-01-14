﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Google.Protobuf.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Service.Interfaces;
using Service.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly CloudinarySettings _cloudinarySetting;

        public CloudinaryService(IOptions<CloudinarySettings> cloudinarySetting)
        {
            _cloudinarySetting = cloudinarySetting.Value;
            var account = new CloudinaryDotNet.Account(
                        _cloudinarySetting.Cloud,
                        _cloudinarySetting.ApiKey,
                        _cloudinarySetting.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }


        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty or null");
            var uploadResult = new ImageUploadResult();

            await using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Quality(80).Crop("limit")
            };

            uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.Message);

            return uploadResult.SecureUrl.ToString();
        }
    }
}
