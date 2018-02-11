using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yumiki.Commons.Storage
{
    public class FileSystem
    {
        public static string GenerateFilePath(Guid id, string fileName)
        {
            string guid = id.ToString();

            return Path.Combine(guid[0].ToString(), guid[1].ToString(), guid[2].ToString(), $"{guid}-{fileName}");
        }

        /// <summary>
        /// Save file to file system
        /// </summary>
        /// <param name="id">To get 3 initial words to be folder name. E.g. \E\A\6\file.ext</param>
        /// <param name="path">Prefix of physical path. E.g. C:\Storage\Files</param>
        /// <param name="file">File to be saved in file system</param>
        public static void Save(Guid id, string path, string fileName, Stream readStream)
        {
            string guid = id.ToString();

            string folder1 = Path.Combine(path, guid[0].ToString());
            if (!Directory.Exists(folder1))
            {
                Directory.CreateDirectory(folder1);
            }

            string folder2 = Path.Combine(folder1, guid[1].ToString());
            if (!Directory.Exists(folder2))
            {
                Directory.CreateDirectory(folder2);
            }

            string folder3 = Path.Combine(folder2, guid[2].ToString());
            if (!Directory.Exists(folder3))
            {
                Directory.CreateDirectory(folder3);
            }

            string fullFileName = Path.Combine(folder3, $"{guid}-{fileName}");
            if (File.Exists(fullFileName))
            {
                File.Delete(fullFileName);
            }

            FileStream writeStream = new FileStream(fullFileName, FileMode.Create, FileAccess.Write);
            int length = 256;
            Byte[] buffer = new Byte[length];
            int bytesRead = readStream.Read(buffer, 0, length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, length);
            }
            readStream.Close();
            writeStream.Close();
        }

        /// <summary>
        /// Delete file from file system
        /// </summary>
        /// <param name="id">To get 3 initial words to be folder name. E.g. \E\A\6\file.ext</param>
        /// <param name="path">Prefix of physical path. E.g. C:\Storage\Files</param>
        /// <param name="fileName">File to be deleted in file system</param>
        public static void Delete(Guid id, string path, string fileName)
        {
            string guid = id.ToString();

            string folder1 = Path.Combine(path, guid[0].ToString());
            string folder2 = Path.Combine(folder1, guid[1].ToString());
            string folder3 = Path.Combine(folder2, guid[2].ToString());

            string fullFileName = Path.Combine(folder3, $"{guid}-{fileName}");
            if (File.Exists(fullFileName))
            {
                File.Delete(fullFileName);
            }

            if (Directory.Exists(folder3))
            {
                if (!Directory.GetFiles(folder3, "*", SearchOption.AllDirectories).Any())
                {
                    Directory.Delete(folder3, true);
                }
                else
                {
                    return;
                }
            }

            if (Directory.Exists(folder2))
            {
                if (!Directory.GetFiles(folder2, "*", SearchOption.AllDirectories).Any())
                {
                    Directory.Delete(folder2, true);
                }
                else
                {
                    return;
                }
            }


            if (Directory.Exists(folder1))
            {
                if (!Directory.GetFiles(folder1, "*", SearchOption.AllDirectories).Any())
                {
                    Directory.Delete(folder1, true);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
