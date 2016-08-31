using System;
using System.Linq;
using System.ServiceModel;

namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VirtualFileSystem" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select VirtualFileSystem.svc or VirtualFileSystem.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class VirtualFileSystem : IVirtualFileSystem
    {
        private readonly Folder _root;
        public VirtualFileSystem()
        {
            _root = new Folder("root");
        }

        private static void IsValidName(Folder parent, string name)
        {
            if (parent.list.Exists(element => element.Name == name))
            {

                throw new Exception("В одной папке одинаковые имена элементов не допустимы");
            }

            if (name.Contains("/"))
            {
                throw new Exception("Имя папки не должно содержать '/'");
            }
           
        }


        public Item GetItemByPath(string path)
        {
            if (path == null) return Root;

            var folders = path.Split('/').ToList();


            Item current = Root;

            for (var i = 0; i < folders.Count; i++)
            {

                if (!(current is Folder))
                {
                    if (i < folders.Count - 1) return null;
                    else return (File) current;
                }


                    current = (current as Folder).list.Find(element => element.Name == folders[i]);
            }

            return current;

        }


        public string CreateItem(string path, string name, bool type)
        {
            try
            {
                var parent = (Folder)GetItemByPath(path);
                IsValidName(parent, name);
                    
                Item file;
                if (type)
                {
                    file = new Folder(name) { Parent = parent };

                }

                else
                {
                    file = new File(name) { Parent = parent };
                }
                
                parent.list.Add(file);

            }                   

            catch (Exception ex)
            {
                return ex.Message;
            }

            return "элемент создан";
        }


        public string RemoveItem(string path)
        {
            try {
                var item = GetItemByPath(path);
                item.Parent.list.Remove(item);
                return "элемент удален";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }



        public string CopyItem(string source, string receiver)
        {
            try {

                Item item = GetItemByPath(source);
                Folder receiverFolder = (Folder)GetItemByPath(receiver);

                if (item is Folder)
                    if ((item as Folder).IsChild(receiverFolder))
                        return
                            "Папка, в которую следует поместить элементы, является дочерней для папки, в которой они находятся";
                    
                IsValidName(receiverFolder, item.Name);

                var copy = item.Copy();
                copy.Parent = receiverFolder;
                receiverFolder.list.Add(copy);
                return "элемент скопирован";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string MoveItem(string source, string receiver)
        {
            try {
                CopyItem(source, receiver);
                RemoveItem(source);
                return "элемент перемещен";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string ShowItemsInFolder(string path)
        {
            string folders = "";
            try {
                var folder = GetItemByPath(path) as Folder;

                return folder.list.Aggregate(folders, (current, item) => current + (item.Name + "  "));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Folder Root => _root;
    }
}
