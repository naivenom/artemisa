using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;
using System.Net;
using System.Management;
using System.Net.Sockets;

namespace artemisa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label1.ForeColor = System.Drawing.Color.Red;
            label1.Text = "Parado";
            CheckForIllegalCrossThreadCalls = false;
        }
        Thread myThread = null;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void _scan(string subred)
        {
            Ping _ping;
            PingReply _respuesta;
            IPAddress _ip;
            IPHostEntry _host;

            progressBar1.Maximum = 254;
            progressBar1.Value = 0;
            OutPut.Items.Clear();

            for (int i = 1; i < 255; i++)
            {
                string _subred = "." + i.ToString();
                _ping = new Ping();
                _respuesta = _ping.Send(subred + _subred, 900);

                label1.ForeColor = System.Drawing.Color.Green;
                label1.Text = "Escaneando: " + subred + _subred;

                if (_respuesta.Status == IPStatus.Success)
                {
                    try
                    {
                        _ip = IPAddress.Parse(subred + _subred);
                        _host = Dns.GetHostEntry(_ip);

                        OutPut.Items.Add(new ListViewItem(new String[] { subred + _subred, _host.HostName, "Ok" }));
                    }
                    catch { OutPut.Items.Add(new ListViewItem(new String[] { subred + _subred, "Se desconoce", "Ok" })); }
                }
                progressBar1.Value += 1;
            }
            scan.Enabled = true;
            stop.Enabled = false;
            ipAddress.Enabled = true;
            label1.Text = "Realizado con exito!";
            int _contador = OutPut.Items.Count;
            MessageBox.Show("Escaneo realizado!\nEncontrados " + _contador.ToString() + " hosts.", "Hecho", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void _detallesWin(string _host)
        {
            string con = null;
            string[] _deteccionWin = { "Win32_ComputerSystem", "Win32_OperatingSystem" };
            string[] param = { "UserName", "Caption" };

            label1.ForeColor = System.Drawing.Color.Green;

            for (int i = 0; i <= _deteccionWin.Length - 1; i++)
            {
                label1.Text = "Obteniendo informacion.";
                try
                {
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("\\\\" + _host + "\\root\\CIMV2", "SELECT *FROM " + _deteccionWin[i]);
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        label1.Text = "Obteniendo informacion. .";

                        con += obj.GetPropertyValue(param[i]).ToString() + "\n";
                        if (i == _deteccionWin.Length - 1)
                        {
                            label1.Text = "Hecho!";
                            MessageBox.Show(con, "Host Windows: " + _host, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                        label1.Text = "Obteniendo informacion. . .";
                    }
                }
                catch (Exception excepcion) { MessageBox.Show("Error en consulta WMI.\n\n" + excepcion.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); break; }
            }
        }

        public void _detallesPuertos(string _host)
        {
            progressBar1.Maximum = 8080;
            progressBar1.Value = 0;
            for (int i = 1; i < 8081; i++)
            {
                string _puerto = i.ToString();
                IPEndPoint hostRemoto = new IPEndPoint(IPAddress.Parse(_host), i);
                label1.ForeColor = System.Drawing.Color.Green;
                label1.Text = "Escaneando puertos: " + _puerto;

                using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    try
                    {
                        sock.Connect(hostRemoto);
                        if (i == 80)
                        {
                            try
                            {
                                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://" + _host);
                                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                                string _respuesta = string.Empty;
                                string _server = string.Empty;
                                StringBuilder requestBuilder1 = new StringBuilder();
                                for (int j = 0; j < myHttpWebResponse.Headers.Count; ++j)
                                {
                                    requestBuilder1.Append(myHttpWebResponse.Headers.Keys[j] + " " + myHttpWebResponse.Headers[j] + "\n");
                                    if (myHttpWebResponse.Headers.Keys[j].StartsWith("Server"))
                                    {
                                        _server = myHttpWebResponse.Headers[j].Split(' ')[0].Replace("\r", string.Empty) + myHttpWebResponse.Headers[j].Split(' ')[1].Replace("\r", string.Empty);
                                    }
                                }
                                MessageBox.Show("Puerto: " + i + " abierto. "+"Version del servidor web y sistema operativo: " + _server);
                                OutPut.Items.Add(new ListViewItem(new String[] { i.ToString(), "","Abierto" }));
                                myHttpWebResponse.Close();
                            }
                            catch (Exception excepcion) { MessageBox.Show("Error en obtencion del sistema operativo.\n\n" + excepcion.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        else
                        {
                            OutPut.Items.Add(new ListViewItem(new String[] { i.ToString(), "","Abierto"}));
                        }
                    }
                    catch (Exception excepcion) { }

                }
                progressBar1.Value += 1;
            }
          
        }
 
        private void scan_Click(object sender, EventArgs e)
        {
            if (ipAddress.Text == string.Empty)
            {
                MessageBox.Show("No se ha introducido ninguna IP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (escanerDeRed.Checked)
                {
                    myThread = new Thread(() => _scan(ipAddress.Text));
                    myThread.Start();

                    if (myThread.IsAlive == true)
                    {
                        stop.Enabled = true;
                        scan.Enabled = false;
                        ipAddress.Enabled = false;
                    }
                }
                if (deteccion.Checked)
                {
                    Thread qThread = new Thread(() => _detallesWin(ipAddress.Text));
                    qThread.Start();

                    if (qThread.IsAlive == true)
                    {
                        stop.Enabled = true;
                        scan.Enabled = false;
                        ipAddress.Enabled = false;
                    }
                }
                if (deteccionPuertos.Checked)
                {
                    Thread LThread = new Thread(() => _detallesPuertos(ipAddress.Text));
                    LThread.Start();

                    if (LThread.IsAlive == true)
                    {
                        stop.Enabled = true;
                        scan.Enabled = false;
                        ipAddress.Enabled = false;
                    }
                }

            }
        }

        private void ipAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void stop_Click(object sender, EventArgs e)
        {
            myThread.Suspend();
            scan.Enabled = true;
            stop.Enabled = false;
            ipAddress.Enabled = true;
            label1.ForeColor = System.Drawing.Color.Red;
            label1.Text = "Parado";
        }

        private void OutPut_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void deteccionLinux_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
