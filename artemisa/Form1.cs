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
using System.IO;

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
            progressBar1.Maximum = 182;
            progressBar1.Value = 0;
            OutPut.Items.Clear();
            int[] _puertosConocidos = { 1,5,7,9,11,13,17,18,19,20,21,22,23,25,37,39,42,43,49,50,53,63,66,67,68,69,70,79,80,88,95,101,107,109,110,111,113,115,117,119,
                                123,135,137,138,139,143,161,162,174,177,178,179,194,199,201,202,204,206,209,210,213,220,245,347,363,369,370,372,389,427,434,
                                435,443,444,445,465,500,512,513,514,515,520,587,591,631,666,690,993,995,1080,1337,1352,1433,1434,1494,1512,1521,1701,1720,1723,1761,
                                1863,1935,2049,2082,2083,2086,2427,3025,3030,3074,3128,3306,3389,3396,3690,4299,4662,4672,4899,5000,5060,5190,5222,5223,5269,5432,5517,5631,5632,
                                5400,5500,5600,5700,5800,5900,6000,6112,6129,6346,6347,6348,6349,6350,6355,6667,6881,6969,7100,8000,8080,8118,9009,9898,10000,11845,19226,12345,15477,
                                15888,19012,22802,25565,26412,29380,31337,32768,32769,36264,36681,36867,38204,41901,44929,45003,49539,59393,60036,61910,62005,62119,62253,63378 };
            for (int i = 1; i < 183; i++)
            {
                string _puerto = i.ToString();
                IPEndPoint hostRemoto = new IPEndPoint(IPAddress.Parse(_host), _puertosConocidos[i]);
                label1.ForeColor = Color.Green;
                label1.Text = "Escaneando puertos: " + _puertosConocidos[i];

                using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                //Console.Read();
                {
                    try
                    {
                        sock.Connect(hostRemoto);

                        if (_puertosConocidos[i] == 80)
                            
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
                                OutPut.Items.Add(new ListViewItem(new String[] { _puertosConocidos[i].ToString(), _server , "Abierto" }));
                                myHttpWebResponse.Close();
                            }
                            catch (Exception excepcion) { MessageBox.Show("Error en obtencion del sistema operativo.\n\n" + excepcion.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        else if (_puertosConocidos[i] == 22 || _puertosConocidos[i] == 21)
                        {

                            
                            using (TcpClient client = new TcpClient(_host, _puertosConocidos[i]))
                            {
                                using(Stream stream = client.GetStream())
                                {
                                    using (StreamReader lectura = new StreamReader(stream))
                                    {
                                        while(true)
                                        {
                                            string _version = lectura.ReadLine();
                                            OutPut.Items.Add(new ListViewItem(new String[] { _puertosConocidos[i].ToString(), _version, "Abierto" }));
                                            lectura.Close();
                                            stream.Close();
                                            client.Close();
                                            if (_puertosConocidos[i] == 21)
                                            {
                                                Thread ftpThread = new Thread(() => _ftpanonymousHabilitado(_host));
                                                ftpThread.Start();
                                            }
                                                
                                        }
                                    }
                                }
                            }

                        }
                        else if (_puertosConocidos[i] == 139)
                        {
                            OutPut.Items.Add(new ListViewItem(new String[] { _puertosConocidos[i].ToString(), "NETBIOS Session Service", "Abierto" })); 
                        }
                        else
                        {
                            OutPut.Items.Add(new ListViewItem(new String[] { _puertosConocidos[i].ToString(), "","Abierto"}));
                        }

                    }
                    catch (Exception excepcion) { }

                }
                progressBar1.Value += 1;
            }
            label1.Text = "Realizado con exito!";
            int _contador = OutPut.Items.Count;
            MessageBox.Show("Escaneo realizado!\nEncontrados " + _contador.ToString() + " puertos.", "Hecho", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public static void _ftpanonymousHabilitado(string _host)
        {
            
            var port = 21;
            var userName = "anonymous";
            var password = "anonymous";
            

            var tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(_host, port);
                Flush(tcpClient);

                var response = TransmitCommand(tcpClient, "user " + userName);
                if (response.IndexOf("331", StringComparison.OrdinalIgnoreCase) < 0)
                    throw new Exception(string.Format("Error \"{0}\" mientras se envia user \"{1}\".", response, userName));

                response = TransmitCommand(tcpClient, "pass " + password);
                MessageBox.Show("Anonymous habilitado: " + response , "FTP", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (response.IndexOf("230", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    throw new Exception(string.Format("Error \"{0}\" mientras se envia pass.", response));
                }
                
            }
            finally
            {
                if (tcpClient.Connected)
                    tcpClient.Close();
            }
        }

        private static string TransmitCommand(TcpClient tcpClient, string cmd)
        {
            var networkStream = tcpClient.GetStream();
            if (!networkStream.CanWrite || !networkStream.CanRead)
                return string.Empty;

            var sendBytes = Encoding.ASCII.GetBytes(cmd + "\r\n");
            networkStream.Write(sendBytes, 0, sendBytes.Length);

            var streamReader = new StreamReader(networkStream);
            return streamReader.ReadLine();
        }

        private static string Flush(TcpClient tcpClient)
        {
            try
            {
                var networkStream = tcpClient.GetStream();
                if (!networkStream.CanWrite || !networkStream.CanRead)
                    return string.Empty;

                var receiveBytes = new byte[tcpClient.ReceiveBufferSize];
                networkStream.ReadTimeout = 10000;
                networkStream.Read(receiveBytes, 0, tcpClient.ReceiveBufferSize);

                return Encoding.ASCII.GetString(receiveBytes);
            }
            catch
            {
                // Ignora las excepciones irrelevantes
            }

            return string.Empty;
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
