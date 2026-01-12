using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S7.Net;

namespace scadaServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PLCController : ControllerBase
    {
        static Plc myPLC;

        [HttpPost("connect")]
        public bool connect()
        {
            myPLC = new Plc(CpuType.S71500, "192.168.2.122", 0, 1);
            controllaConnessione(myPLC.IsConnected);
            return myPLC.IsConnected;
        }


        [HttpPost("disconnect")]
        public bool diconnect()
        {
            myPLC.Close();
            controllaConnessione(myPLC.IsConnected);
            return myPLC.IsConnected;
        }

        [HttpPost("startNastro")]
        public bool startNastro()
        {
            myPLC.Write("DB1.DBX1.1", true);
            return true;
        }

        private void controllaConnessione(bool isConnected)
        {
            if (isConnected)
                Console.WriteLine("-- Connected --");
            else
                Console.WriteLine("-- Disconnected --");
        }
    }
}
