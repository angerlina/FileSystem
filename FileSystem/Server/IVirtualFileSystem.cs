using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVirtualFileSystem" in both code and config file together.
    [ServiceContract]
    public interface IVirtualFileSystem
    {
        [OperationContract]
        string CreateItem(string path, string name, bool type);
        [OperationContract]
        string RemoveItem(string path);
        [OperationContract]
        string CopyItem(string source, string receiver);
        [OperationContract]
        string MoveItem(string source, string receiver);
        [OperationContract]
        string ShowItemsInFolder(string path);
    }
}
