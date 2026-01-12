using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//
using S7.Net;

namespace SCADA_CSharp
{
    public partial class scada : System.Web.UI.Page
    {
        static Plc myPLC;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    Timer1.Enabled = false;
                    myPLC = new Plc(CpuType.S71500, "192.168.2.100", 0, 1); //"192.168.2.100"
                    controllaConnessione(myPLC.IsConnected);
                }
                catch (Exception ex)
                {
                    lblMessaggio.Text = ex.Message;
                }
            }
        }

        protected void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                myPLC.Open();
                Timer1.Enabled = true;
                controllaConnessione(myPLC.IsConnected);
            }
            catch (Exception ex)
            {
                lblMessaggio.Text = ex.Message;
            }
        }

        protected void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                myPLC.Close();
                Timer1.Enabled = false;
                controllaConnessione(myPLC.IsConnected);
            }
            catch (Exception ex)
            {
                lblMessaggio.Text = ex.Message;
            }
        }
        private void controllaConnessione(bool isConnected)
        {
            if (isConnected)
            {
                lblConnect.BackColor = System.Drawing.Color.Green;
                lblConnect.Text = "  --connected--  ";
            }
            else
            {
                lblConnect.BackColor = System.Drawing.Color.Red;
                lblConnect.Text = "  --disconnected--  ";
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            bool[] led = new bool[8];
            for (int i = 0; i < 8; i++)
            {
                led[i] = (bool)myPLC.Read("DB5.DBX4." + i.ToString());
                coloraLed(led[i], i);
            }

        }

        private void coloraLed(bool led, int i)
        {
            if (i == 0)
                if (led)
                    btnLed0.BackColor = System.Drawing.Color.Red;
                else
                    btnLed0.BackColor = System.Drawing.Color.Gray;
            if (i == 1)
                if (led)
                    btnLed1.BackColor = System.Drawing.Color.Red;
                else
                    btnLed1.BackColor = System.Drawing.Color.Gray;
            if (i == 2)
                if (led)
                    btnLed2.BackColor = System.Drawing.Color.Red;
                else
                    btnLed2.BackColor = System.Drawing.Color.Gray;
            if (i == 3)
                if (led)
                    btnLed3.BackColor = System.Drawing.Color.Red;
                else
                    btnLed3.BackColor = System.Drawing.Color.Gray;
            if (i == 4)
                if (led)
                    btnLed4.BackColor = System.Drawing.Color.Red;
                else
                    btnLed4.BackColor = System.Drawing.Color.Gray;
            if (i == 5)
                if (led)
                    btnLed5.BackColor = System.Drawing.Color.Red;
                else
                    btnLed5.BackColor = System.Drawing.Color.Gray;
            if (i == 6)
                if (led)
                    btnLed6.BackColor = System.Drawing.Color.Red;
                else
                    btnLed6.BackColor = System.Drawing.Color.Gray;
            if (i == 7)
                if (led)
                    btnLed7.BackColor = System.Drawing.Color.Red;
                else
                    btnLed7.BackColor = System.Drawing.Color.Gray;
        }

        protected void btnAuto_Click(object sender, EventArgs e)
        {
            try
            {
                myPLC.Write("DB3.DBX0.1", true);
            }
            catch (Exception ex)
            {
                lblMessaggio.Text = ex.Message;
            }
        }

        protected void btnMan_Click(object sender, EventArgs e)
        {
            try
            {
                myPLC.Write("DB3.DBX0.2", true);
            }
            catch (Exception ex)
            {
                lblMessaggio.Text = ex.Message;
            }
        }

        protected void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                myPLC.Write("DB3.DBX0.0", true);
            }
            catch (Exception ex)
            {
                lblMessaggio.Text = ex.Message;
            }
        }

        protected void btnAttivaEmergenza_Click(object sender, EventArgs e)
        {
            try
            {
                myPLC.Write("DB3.DBX0.4", true);
            }
            catch (Exception ex)
            {
                lblMessaggio.Text = ex.Message;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                myPLC.Write("DB3.DBX0.3", true);
            }
            catch (Exception ex)
            {
                lblMessaggio.Text = ex.Message;
            }
        }
    }
}