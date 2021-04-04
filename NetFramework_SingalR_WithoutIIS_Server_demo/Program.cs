using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFramework_SingalR_WithoutIIS_Server_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Microsoft.Owin.Hosting.WebApp.Start<Startup>("http://localhost:8080"))
            {
                Console.WriteLine("Server running");
                Console.Read();
            }
        }

        class Startup
        {
            public void Configuration(IAppBuilder app)
            {
                //// 要讓網頁端能連線需要允許Cors,要安裝Microsoft.Owin.Cors
                app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
                app.MapSignalR();
            }
        }

        class MyHub : Microsoft.AspNet.SignalR.Hub
        {
            public void Send(string user, string message)
            {
                //// 取得目前connection ID
                var connectionId = this.Context.ConnectionId;

                //// 只傳遞給特定connection,Send是Client端的函式
                this.Clients.Client(connectionId).SayHello(user, message);

                //// 傳給所有Connection
                this.Clients.All.SayHello(user, message);
            }
        }
    }
}
