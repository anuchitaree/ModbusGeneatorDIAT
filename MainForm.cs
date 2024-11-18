using EasyModbus;
using ModbusGeneatorDIAT.Modules;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace ModbusGeneatorDIAT
{
    public partial class MainForm : Form
    {
        ModbusClient modbusClient = new ModbusClient();

        ModbusServer modbusServer;

        DispatcherTimer timerPollWrite = new DispatcherTimer();
        DispatcherTimer timerPollRead = new DispatcherTimer();
        private int energyacc0;
        private int energyacc1;
        private int energyacc2;
        private int energyacc3;
        private int prodacc;
        private int flowacc;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string[] header = new string[] { "InputRegAddr", "Updating value", "Description", "Update" };
            int[] width = new int[] { 100, 100, 150, 200 };

            InitDataGridView.Pattern_1(dgv, header, width);
        }

        private void TimerPollRead_Tick(object sender, EventArgs e)
        {
            int[] energyRead = { 0, 0, 0, 0 };
            int[] prodRead = { 0, 0, 0, 0 };
            int flowRead = 0;
            string[] decription = new string[] {
            "Energy MC1",
            "Energy MC2",
            "Energy MC3",
            "Energy MC4"};

            try
            {
                if (modbusClient.Connected == true)
                {

                    int[] vals = modbusClient.ReadInputRegisters(0, 18);

                    int b = 0;
                    for (int i = 0; i < 16; i += 4)
                    {
                        energyRead[b] = vals[0 + i] << 16 | (vals[1 + i] & 0x0000FFFF); //2147483647
                        prodRead[b] = vals[2 + i] << 16 | (vals[3 + i] & 0x0000FFFF); //2147483647
                        b++;
                    }
                    flowRead = vals[16] << 16 | (vals[17] & 0x0000FFFF); //2147483647

                    dgv.Rows.Clear();
                    var datetime = DateTime.Now.ToString();
                    if (true)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgv);
                        row.Cells[0].Value = "300 000";
                        row.Cells[1].Value = $"{energyRead[0]}";
                        row.Cells[2].Value = $"{decription[0]}";
                        row.Cells[3].Value = datetime;
                        dgv.Rows.Add(row);
                    }
                    if (true)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgv);
                        row.Cells[0].Value = "300 002";
                        row.Cells[1].Value = $"{prodRead[0]}";
                        row.Cells[2].Value = $"Production capture";
                        row.Cells[3].Value = datetime;
                        dgv.Rows.Add(row);
                    }
                    if (true)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgv);
                        row.Cells[0].Value = "300 004";
                        row.Cells[1].Value = $"{energyRead[1]}";
                        row.Cells[2].Value = $"{decription[1]}";
                        row.Cells[3].Value = datetime;
                        dgv.Rows.Add(row);
                    }
                    if (true)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgv);
                        row.Cells[0].Value = "300 006";
                        row.Cells[1].Value = $"{prodRead[0]}";
                        row.Cells[2].Value = $"Production capture";
                        row.Cells[3].Value = datetime;
                        dgv.Rows.Add(row);
                    }
                    if (true)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgv);
                        row.Cells[0].Value = "300 008";
                        row.Cells[1].Value = $"{energyRead[2]}";
                        row.Cells[2].Value = $"{decription[2]}";
                        row.Cells[3].Value = datetime;
                        dgv.Rows.Add(row);
                    }
                    if (true)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgv);
                        row.Cells[0].Value = "300 010";
                        row.Cells[1].Value = $"{prodRead[0]}";
                        row.Cells[2].Value = $"Production capture";
                        row.Cells[3].Value = datetime;
                        dgv.Rows.Add(row);
                    }
                    if (true)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgv);
                        row.Cells[0].Value = "300 012";
                        row.Cells[1].Value = $"{energyRead[3]}";
                        row.Cells[2].Value = $"{decription[3]}";
                        row.Cells[3].Value = datetime;
                        dgv.Rows.Add(row);

                    }
                    if (true)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgv);
                        row.Cells[0].Value = "300 014";
                        row.Cells[1].Value = $"{prodRead[0]}";
                        row.Cells[2].Value = $"Production capture";
                        row.Cells[3].Value = datetime;
                        dgv.Rows.Add(row);
                    }
                    if (true)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        row.CreateCells(dgv);
                        row.Cells[0].Value = "300 016";
                        row.Cells[1].Value = $"{flowRead}";
                        row.Cells[2].Value = $"Total air flow";
                        row.Cells[3].Value = datetime;
                        dgv.Rows.Add(row);
                    }

                }

            }
            catch (Exception ex)
            {

                ex.ToString();
            }
        }



        private void TimerPollWrite_Tick(object sender, EventArgs e)
        {
            try
            {
                int radenergy0;
                int radenergy1;
                int radenergy2;
                int radenergy3;
                int radflow;

                energyacc0 = energyacc0 > 999999999 ? 0 : energyacc0;
                energyacc1 = energyacc1 > 999999999 ? 0 : energyacc1;
                energyacc2 = energyacc2 > 999999999 ? 0 : energyacc2;
                energyacc3 = energyacc3 > 999999999 ? 0 : energyacc3;
                prodacc = prodacc > 999999999 ? 0 : prodacc;
                flowacc = flowacc > 999999999 ? 0 : flowacc;

                Random rnd = new Random();


                int whichday = (int)DateTime.Now.DayOfWeek;
                var datenow = DateTime.Now;
                var year = datenow.Year;
                var month = datenow.Month;
                var day = datenow.Day;

                bool daycondition = whichday >= 1 && whichday <= 5;
                var starttime1 = new DateTime(year, month, day, 7, 30, 0);
                var stoptime1 = new DateTime(year, month, day, 9, 30, 0);

                var starttime2 = new DateTime(year, month, day, 9, 40, 0);
                var stoptime2 = new DateTime(year, month, day, 11, 30, 0);

                var starttime3 = new DateTime(year, month, day, 12, 30, 0);
                var stoptime3 = new DateTime(year, month, day, 14, 20, 0);

                var starttime4 = new DateTime(year, month, day, 14, 30, 0);
                var stoptime4 = new DateTime(year, month, day, 16, 30, 0);

                bool timecondition = (starttime1 <= datenow && datenow <= stoptime1) ||
                    (starttime2 <= datenow && datenow <= stoptime2) ||
                    (starttime3 <= datenow && datenow <= stoptime3) ||
                    (starttime4 <= datenow && datenow <= stoptime4);
                if (daycondition && timecondition)
                {
                    radenergy0 = rnd.Next(5, 10);
                    radenergy1 = rnd.Next(5, 10);
                    radenergy2 = rnd.Next(5, 10);
                    radenergy3 = rnd.Next(5, 10);
                    radflow = rnd.Next(7, 10);
                    prodacc += 1;

                }
                else
                {
                    radenergy0 = rnd.Next(1, 3);
                    radenergy1 = rnd.Next(1, 3);
                    radenergy2 = rnd.Next(1, 3);
                    radenergy3 = rnd.Next(1, 3);
                    radflow = rnd.Next(1, 3);
                }

                energyacc0 = energyacc0 + radenergy0;
                energyacc1 = energyacc1 + radenergy1;
                energyacc2 = energyacc2 + radenergy2;
                energyacc3 = energyacc3 + radenergy3;
                flowacc = flowacc + radflow;



                byte[] bytes0 = BitConverter.GetBytes(energyacc0);
                short firstHalf0 = BitConverter.ToInt16(bytes0, 0);
                short secondHalf0 = BitConverter.ToInt16(bytes0, 2);

                byte[] bytes1 = BitConverter.GetBytes(energyacc1);
                short firstHalf1 = BitConverter.ToInt16(bytes1, 0);
                short secondHalf1 = BitConverter.ToInt16(bytes1, 2);

                byte[] bytes2 = BitConverter.GetBytes(energyacc2);
                short firstHalf2 = BitConverter.ToInt16(bytes2, 0);
                short secondHalf2 = BitConverter.ToInt16(bytes2, 2);

                byte[] bytes3 = BitConverter.GetBytes(energyacc3);
                short firstHalf3 = BitConverter.ToInt16(bytes3, 0);
                short secondHalf3 = BitConverter.ToInt16(bytes3, 2);

                byte[] bytes4 = BitConverter.GetBytes(prodacc);
                short firstHalf4 = BitConverter.ToInt16(bytes4, 0);
                short secondHalf4 = BitConverter.ToInt16(bytes4, 2);

                byte[] bytes5 = BitConverter.GetBytes(flowacc);
                short firstHalf5 = BitConverter.ToInt16(bytes5, 0);
                short secondHalf5 = BitConverter.ToInt16(bytes5, 2);

                ModbusServer.InputRegisters regs = modbusServer.inputRegisters;
                //----------------------------//
                regs[2] = firstHalf0;  /// FFFF
                regs[1] = secondHalf0;  /// 7FFF

                regs[4] = firstHalf4;
                regs[3] = secondHalf4;
                //----------------------------//
                regs[6] = firstHalf1;
                regs[5] = secondHalf1;

                regs[8] = firstHalf4;
                regs[7] = secondHalf4;
                //----------------------------//
                regs[10] = firstHalf2;
                regs[9] = secondHalf2;

                regs[12] = firstHalf4;
                regs[11] = secondHalf4;
                //----------------------------//
                regs[14] = firstHalf3;
                regs[13] = secondHalf3;

                regs[16] = firstHalf4;
                regs[15] = secondHalf4;
                //----------------------------//

                regs[18] = firstHalf5;
                regs[17] = secondHalf5;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnStart.Text == "Start")
                {
                    string ip = tbIP.Text.Trim();
                    int port = int.Parse(tbPort.Text.Trim());
                    double autoupdate = double.Parse(tbScanrate.Text);

                    modbusServer = new ModbusServer();
                    modbusServer.Listen();

                    Thread.Sleep(1000);

                    modbusClient.IPAddress = ip;
                    modbusClient.Port = port;
                    modbusClient.ConnectionTimeout = 10000;
                    modbusClient.Connect();


                    timerPollRead.Interval = TimeSpan.FromMilliseconds(autoupdate);
                    timerPollRead.Tick += TimerPollRead_Tick;

                    timerPollWrite.Interval = TimeSpan.FromMilliseconds(autoupdate);
                    timerPollWrite.Tick += TimerPollWrite_Tick;

                    timerPollWrite.Start();
                    timerPollRead.Start();

                    btnStart.Text = "Starting";
                    btnStart.BackColor = Color.GreenYellow;
                    tbIP.ReadOnly = true;
                    tbPort.ReadOnly = true;
                    tbScanrate.ReadOnly = true;
                }
                else
                {
                    modbusClient.Disconnect();
                    btnStart.Text = "Start";
                    btnStart.BackColor = SystemColors.Control;
                    timerPollWrite.Tick -= TimerPollWrite_Tick;
                    timerPollRead.Tick -= TimerPollRead_Tick;
                    tbIP.ReadOnly = false;
                    tbPort.ReadOnly = false;
                    tbScanrate.ReadOnly = false;
                }
                dgv.Rows.Clear();
            }
            catch (EasyModbus.Exceptions.ConnectionException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine(ex.Message);
                //lbStatus.Text = "Status : Connection timed out";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
