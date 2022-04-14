using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace YazılımTasarımKalıpları
{
    class Program
    {
        static void Main(string[] args)
        {
            var clients = new List<RequestClient>();
            for (int i = 0; i < 15; i++)
            {
                clients.Add(new RequestClient());
            }
            for (int i = 0; i < 15; i++)
            {
                clients[i].Connect(i+1);
            }
            
        }
    }
}
