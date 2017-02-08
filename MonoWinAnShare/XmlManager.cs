//using System;
//using System.Xml.Serialization;
//using System.IO;
//using System.Text;

//#if __ANDROID__
//    using Microsoft.Xna.Framework;
//#endif


//namespace MonoWinAnShare
//{
//    public class XmlManager<T>
//    {
//        public Type Type;

//        public XmlManager()
//        {
//            Type = typeof(T);
//        }
//#if !__ANDROID__ && !__IOS__
//        public T Load(string path)
//        {
//            T instance;
//            // convert string to stream
//            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(path));
//            //SystemIOFileNOTFOUND exception (if path is null go to title screen)
//            using (TextReader reader = new StreamReader(stream))
//            {
//                XmlSerializer xml = new XmlSerializer(Type);
//                instance = (T)xml.Deserialize(reader);
//            }
//            return instance;
//        }
//#endif
//#if __ANDROID__
//        public T Load(string path)
//        {
//            T instance;
//            var filePath = path;
//            using (TextReader reader = new StreamReader(TitleContainer.OpenStream(filePath)))
//            {
//                XmlSerializer xml = new XmlSerializer(Type);
//                instance = (T)xml.Deserialize(reader);
//            }
//            return instance;
//        }
//        //TODO: This still modified for android
//#endif
//#if !__ANDROID__
//        //public void Save(string path, object obj)
//        //{
//        //    using (TextWriter writer = new StreamWriter(path))
//        //    {
//        //        XmlSerializer xml = new XmlSerializer(Type);
//        //        xml.Serialize(writer, obj);
//        //    }
//        //}
//#endif
//    }
//}