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
        public IActionResult btnMan()
        {
            if (!myPLC.IsConnected) { return Ok(new { success = false}); }
            myPLC.Write("DB1.DBX1.4", true);
            if (Convert.ToBoolean(myPLC.Read("DB1.DBX1.4")))
                return Ok(new { success = true, status = "man" });
            else return Ok(new { success = false, });
        }

        [HttpGet("startNastro")]
        public IActionResult startNastro()
        {
            if (!myPLC.IsConnected) { return Ok(new { success = false}); }
            myPLC.Write("DB4.DBW54", 0);

            int nCicli = Convert.ToInt32(myPLC.Read("DB4.DBW52"));
            int nCicli = Convert.ToInt32(myPLC.Read("DB4.DBW52"));
            if(nCicli<3)
                myPLC.Write("DB1.DBX1.1", true);

            bool success = Convert.ToBoolean(myPLC.Read("DB4.DBX56.0"));
            return Ok(new { success = success}); 
        }

        [HttpGet("stopNastro")]
        public IActionResult stopNastro()
        {
            if (!myPLC.IsConnected) { return Ok(new { success = false }); }
            myPLC.Write("DB1.DBX0.2", true);
            bool success = Convert.ToBoolean(myPLC.Read("DB4.DBX56.0"));
            return Ok(new { success = success });
        }


        [HttpGet("slittaCentraleSu")]
        public IActionResult slittaCentraleSu()
        {
            if (!myPLC.IsConnected) { return Ok(new { success = false }); }
            myPLC.Write("DB1.DBX1.0", true);
            bool success = Convert.ToBoolean(myPLC.Read("DB4.DBX56.1"));
            return Ok(new { success = success });
        }

        [HttpGet("slittaCentraleGiu")]
        public IActionResult slittaCentraleGiu()
        {
            if (!myPLC.IsConnected) { return Ok(new { success = false }); }
            myPLC.Write("DB1.DBX0.7", true);
            bool success = Convert.ToBoolean(myPLC.Read("DB4.DBX56.4"));
            return Ok(new { success = success });
        }

        [HttpGet("pistoneAvanti")]
        public IActionResult pistoneAvanti()
        {
            if (!myPLC.IsConnected) { return Ok(new { success = false }); }
            myPLC.Write("DB1.DBX0.6", true);
            int nCicli = Convert.ToInt32(myPLC.Read("DB4.DBW52"));
            if (nCicli == 3)
            {
                myPLC.Write("DB4.DBW54", 0);
                myPLC.Write("DB4.DBX50.4", true);
            }

            bool success = Convert.ToBoolean(myPLC.Read("DB4.DBX50.3"));

            return Ok(new { success = success });
        }

        [HttpGet("pistoneIndietro")]
        public IActionResult pistoneIndietro()
        {
            if (!myPLC.IsConnected) { return Ok(new { success = false }); }
            myPLC.Write("DB1.DBX0.5", true);
            myPLC.Write("DB4.DBX50.4", false);
            bool success = Convert.ToBoolean(myPLC.Read("DB4.DBX50.2"));
            return Ok(new { success = success });
        }

        [HttpGet("slittaLatSu")]
        public bool slittaLatSu()
        {
            if (!myPLC.IsConnected) { return false; }
            myPLC.Write("DB1.DBX0.4", true);
            return Convert.ToBoolean(myPLC.Read("DB4.DBX50.1"));
        }

        [HttpGet("slittaLatGiu")]
        public bool slittaLatGiu()
        {
            if (!myPLC.IsConnected) { return false; }
            myPLC.Write("DB1.DBX0.3", true);
            return Convert.ToBoolean(myPLC.Read("DB4.DBX50.1"));
        }

        [HttpGet("emergenza")]
        public void emergenza()
        {
            if (!myPLC.IsConnected) { return; }
            myPLC.Write("DB1.DBX1.6", false);
        }

        [HttpGet("annullaEmergenza")]
        public void annullaEmergenza()
        {
            if (!myPLC.IsConnected) { return; }
            myPLC.Write("DB1.DBX1.6", true);
        }

        [HttpGet("reset")]
        public IActionResult reset()
        {
            if (!myPLC.IsConnected) { return Ok(new { success = false }); }
            myPLC.Write("DB1.DBX0.1", true);
            return Ok(new { success = true, nPezzi = Convert.ToInt32(myPLC.Read("DB4.DBW54")) });
        }

        [HttpGet("aggiornaPezzi")]
        public IActionResult aggiornaPezzi()
        {
            if (!myPLC.IsConnected) { return  Ok(new { success = false }); }
            return Ok(new { success = true, nPezzi = Convert.ToInt32(myPLC.Read("DB4.DBW54")) });
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
