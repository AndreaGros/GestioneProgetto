using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S7.Net;

namespace scadaServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PLCController : ControllerBase
    {
        static Plc myPLC = new Plc(CpuType.S71500, "192.168.2.122", 0, 1);

        [HttpGet("connect")]
        public bool connect()
        {
            myPLC.Open();
            controllaConnessione(myPLC.IsConnected);
            return myPLC.IsConnected;
        }


        [HttpGet("disconnect")]
        public bool disconnect()
        {
            myPLC.Close();
            controllaConnessione(myPLC.IsConnected);
            return myPLC.IsConnected;
        }

        [HttpGet("startNastro")]
        public bool startNastro()
        {
            if (!myPLC.IsConnected) { return false; }    
            myPLC.Write("DB1.DBX1.1", true);
            return true;
        }

        [HttpGet("slittaCentraleSu")]
        public bool slittaCentraleSu()
        {
            if (!myPLC.IsConnected) { return false; }
            myPLC.Write("DB1.DBX1.0", true);
            return true;
        }

        [HttpGet("slittaCentraleGiu")]
        public bool slittaCentraleGiu()
        {
            if (!myPLC.IsConnected) { return false; }
            myPLC.Write("DB1.DBX0.7", true);
            return true;
        }

        [HttpGet("pistoneAvanti")]
        public bool pistoneAvanti()
        {
            myPLC.Write("DB1.DBX0.6", true);
            return true;
        }

        [HttpGet("pistoneIndietro")]
        public bool pistoneIndietro()
        {
            myPLC.Write("DB1.DBX0.5", true);
            return true;
        }

        [HttpGet("slittaLatSu")]
        public bool slittaLatSu()
        {
            myPLC.Write("DB1.DBX0.4", true);
            return true;
        }

        [HttpGet("slittaLatGiu")]
        public bool slittaLatGiu()
        {
            myPLC.Write("DB1.DBX0.3", true);
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
