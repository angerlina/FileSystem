using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.VfsService;

namespace Client
{
    class Program
    {
   
        static void Main()
        {
            using (VfsService.VirtualFileSystemClient driver = new VirtualFileSystemClient())
            {
                

                string arg = null;
   
                while (arg != "exit")
                {
                    arg = Console.ReadLine();

                    
              
                    var arr = arg.Split(' ');


                    var response = "";
                    
                    try {
                        switch (arr[0])
                        {
                            case "md":
                                
                                response = arr.Length == 2 ? driver.CreateItem(null, arr[1], true) : driver.CreateItem(arr[1], arr[2], true);
        
                                break;

                            case "mf":
                                response = arr.Length == 2 ? driver.CreateItem(null, arr[1], false) : driver.CreateItem(arr[1], arr[2], false);

                                break;
                            case "remove":
                                response = driver.RemoveItem(arr[1]);
                                break;
                            case "move":
                                response = arr.Length == 1 ? driver.MoveItem(arr[1], null) : driver.MoveItem(arr[1], arr[2]);
                                break;
                            case "copy":
                                response = arr.Length == 1 ? driver.CopyItem(arr[1], null) : driver.CopyItem(arr[1], arr[2]);
                                break;
                            case "show":
                                response = driver.ShowItemsInFolder(arr.Length == 1 ? null : arr[1]);

                                break;
                            default:
                                response = "Неверная команда!";
                                break;

                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        response = "Неверные параметры!";
                    }
                    catch (Exception ex)
                    {
                        response = ex.Message;
                    }

                    Console.WriteLine(response);
                    
                   
                }

                Console.ReadKey();
            }
            
        }
    }
}
