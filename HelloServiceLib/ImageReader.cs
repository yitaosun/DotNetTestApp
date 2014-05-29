using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace HelloWorldService.HelloServiceLib
{
    public class ImageReader
    {

    }
    public class DirImageLoader
    {

        public static List<MemoryStream> LoadAllImages(string dirPath)
        {
            List<MemoryStream> imagefiles = new List<MemoryStream>();
            if (Directory.Exists(dirPath))
            {
                string[] files = Directory.GetFiles(dirPath);
                foreach (string fileName in files)
                {
                    string filePath = fileName;
                    MemoryStream stream = LoadImageIntoMemory(filePath);
                    imagefiles.Add(stream);
                }
            }
            return imagefiles;
        }

        public static MemoryStream LoadImageIntoMemory(string filePath)
        {
            MemoryStream memStream = new MemoryStream();
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                memStream.SetLength(fileStream.Length);
                fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);
            }
            return memStream;
        }

    }

    //This will be loaded in dic with weakreferencee
    public class ImageData
    {
        public ImageData()
        {
            if (ConfigurationManager.AppSettings["ImageDirPath"] != null)
            {
                dirPath = ConfigurationManager.AppSettings["ImageDirPath"];
            }
            FilesLoaded();
        }

        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public List<MemoryStream> ImageFiles { get; set; }

        private string dirPath = "c:/temp/images";

        public long TotalSizeCaptured()
        {
            FilesLoaded();
            long totalMBytes = 0;
            foreach (Stream st in ImageFiles)
            {
                long bytes = st.Length;
                totalMBytes = (bytes / 8) / 1024;
            }
            _logger.Trace("Total size captured(mb)-" + totalMBytes);
            return totalMBytes;
        }
        
        public int FilesLoaded()
        {
            if (ImageFiles == null)
            {
                ImageFiles = DirImageLoader.LoadAllImages(dirPath);
            }

            return ImageFiles.Count;
        }

    }
}
