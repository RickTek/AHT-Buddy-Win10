using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace AHT_Buddy
{
    static class XmlIO
    {
        public static async Task<T> ReadObjectFromXMLFileAsync<T>(string filename) //Load XML
        {
            T objectfromXml = default(T);
            var serializer = new XmlSerializer(typeof(T));
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(filename);
            Stream stream = await file.OpenStreamForReadAsync();
            objectfromXml = (T)serializer.Deserialize(stream);
            stream.Dispose();
            return objectfromXml;
        }
        public static async Task SaveObjectToXml<T>(T objectToSave, string filename) //Save XML
        {
            var serializer = new XmlSerializer(typeof(T));
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            Stream stream = await file.OpenStreamForWriteAsync();

            using (stream)
            {
                serializer.Serialize(stream, objectToSave);
            }
        }

    }
}
