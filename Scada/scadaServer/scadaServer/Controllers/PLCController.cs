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

        [HttpGet("btnMan")]
        public bool btnMan()
        {
            if (!myPLC.IsConnected) { return false; }
            myPLC.Write("DB1.DBX1.4", true);
            return Convert.ToBoolean(myPLC.Read("DB1.DBX1.4"));
        }

        [HttpGet("startNastro")]
        public IActionResult startNastro()
        {
            if (!myPLC.IsConnected) { return Ok(new { success = false}); }
            myPLC.Write("DB1.DBX1.1", true);
            bool success = Convert.ToBoolean(myPLC.Read("DB4.DBX56.0"));
            int nPezzi = Convert.ToInt32(myPLC.Read("DB4.DBW54"));
            return Ok(new { success = success, nPezzi = nPezzi }); 
        }

        [HttpGet("slittaCentraleSu")]
        public bool slittaCentraleSu()
        {
            if (!myPLC.IsConnected) { return false; }
            myPLC.Write("DB1.DBX1.0", true);
            return (bool)myPLC.Read("DB4.DBX56.1");
        }

        [HttpGet("slittaCentraleGiu")]
        public bool slittaCentraleGiu()
        {
            if (!myPLC.IsConnected) { return false; }
            myPLC.Write("DB1.DBX0.7", true);
            return (bool)myPLC.Read("DB4.DBX56.4");
        }

        [HttpGet("pistoneAvanti")]
        public bool pistoneAvanti()
        {
            if (!myPLC.IsConnected) { return false; }
            myPLC.Write("DB1.DBX0.6", true);
            return (bool)myPLC.Read("DB4.DBX50.3");
        }

        [HttpGet("pistoneIndietro")]
        public bool pistoneIndietro()
        {
            if (!myPLC.IsConnected) { return false; }
            myPLC.Write("DB1.DBX0.5", true);
            return (bool)myPLC.Read("DB4.DBX50.2");
        }

        [HttpGet("slittaLatSu")]
        public bool slittaLatSu()
        {
            if (!myPLC.IsConnected) { return false; }
            myPLC.Write("DB1.DBX0.4", true);
            return (bool)myPLC.Read("DB4.DBX50.1");
        }

        [HttpGet("slittaLatGiu")]
        public bool slittaLatGiu()
        {
            if (!myPLC.IsConnected) { return false; }
            myPLC.Write("DB1.DBX0.3", true);
            return (bool)myPLC.Read("DB4.DBX50.1");
        }

        [HttpGet("emergenza")]
        public void emergenza()
        {
            if (!myPLC.IsConnected) { return; }
            myPLC.Write("DB1.DBX1.6", false);
        }

        [HttpGet("reset")]
        public void reset()
        {
            if (!myPLC.IsConnected) { return; }
            myPLC.Write("DB1.DBX0.1", true);
        }

        private void controllaConnessione(bool isConnected)
        {
            if (isConnected)
                Console.WriteLine("-- Connected --");
            else
                Console.WriteLine("-- Disconnected --");
        }
        [HttpGet("aggiornaPezzi")]

        private IActionResult aggiornaPezzi()
        {
            return Ok(new { nPezzi = Convert.ToInt32(myPLC.Read("DB4.DBW54")) });
        }
    }
}
