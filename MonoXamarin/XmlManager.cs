using System;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Xna.Framework;

namespace MonoXamarin
{
    public class XmlManager<T>
    {
        public Type Type;

        public XmlManager()
        {
            Type = typeof(T);
        }
#if WINDOWS
        public T Load(string path)
        {
            T instance;
            //SystemIOFileNOTFOUND exception (if path is null go to title screen)
            using (TextReader reader = new StreamReader(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }
#endif
        //Modified for android
#if __ANDROID__
        public T Load(string path)
        {
            T instance;
            var filePath = path;
            using (TextReader reader = new StreamReader(TitleContainer.OpenStream(filePath)))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                instance = (T)xml.Deserialize(reader);
            }
            return instance;
        }
        //TODO: This still modified for android
#endif
        public void Save(string path, object obj)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                XmlSerializer xml = new XmlSerializer(Type);
                xml.Serialize(writer, obj);
            }
        }
    }
}