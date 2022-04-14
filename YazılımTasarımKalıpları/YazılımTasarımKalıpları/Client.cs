using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YazılımTasarımKalıpları
{
    public abstract class Client
    {
        public abstract void Connect(int x);
    }
    public class RequestClient : Client
    {
        public override void Connect(int x)
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    var newClient = ClientPool.Instance.AcquireObject();
                    if (newClient == null)
                    {
                        Console.Write(x + ".Kullanıcı bekletildi\n");

                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.Write(x + ".Kullanıcı erişim yaptı.\n");

                        Thread.Sleep(2000);
                    }
                    Thread.Sleep(2000);
                    if (newClient != null)
                    {
                        ClientPool.Instance.ReleaseObject(newClient);
                        Console.Write(x + ".Kullanıcı erişimi bıraktı.\n");

                        Thread.Sleep(2000);
                    }

                }

            });
            thread.Start();

        }

    }
}
