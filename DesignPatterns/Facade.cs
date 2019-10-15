using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DesignPattern.DesignPatterns
{
    public class Facade : Pattern
    {

        #region SubSystems
        public class VideoCompression
        {
            public Stream CompressFile(Stream file)
            {
                return file;
            }
        }

        public class FileUploader
        {
            public string UploadToServer(Stream file)
            {
                // update stream file to server here
                return "UC6fBbrBB4dF7WznwLmfbOnQ";
            }
            public string GetURL(string id) => $"https://www.youtube.com/channel/{id}";
        }
        #endregion


        #region Facade
        public class VideoFacade
        {
            public string UploadVideoFile(Stream file)
            {
                var videoCompression = new VideoCompression();
                var compressedFile = videoCompression.CompressFile(file);
                var fileUploader = new FileUploader();
                var id = fileUploader.UploadToServer(compressedFile);
                return fileUploader.GetURL(id);
            }
        }
        #endregion

        /// <summary>
        /// Problem: When there're a lot of subsystems and before we can do anything it's depend on another object and many complex for do the task
        /// Solved: Use Facade pattern for keep all complex work into one place and execute only one method(God object)
        /// </summary>
        public override void Demo()
        {
            var videoFacade = new VideoFacade();
            var url = videoFacade.UploadVideoFile(null);
            Console.WriteLine($"Video url: {url}");
        }
    }
}
