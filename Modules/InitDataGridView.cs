using ModbusGeneatorDIAT.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModbusGeneatorDIAT.Modules
{
    public class InitDataGridView
    {
        public static void Pattern_1(DataGridView Dgv, string[] header, int[] width)
        {
            int len = header.Length;
            Dgv.ColumnCount = len;
            for (int i = 0; i < len; i++)
            {
                Dgv.Columns[i].Name = header[i];
                Dgv.Columns[i].Width = width[i];
                Dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                Dgv.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Dgv.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            Dgv.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Dgv.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            //Dgv.Columns[header[0]].Visible = false;


            Dgv.RowHeadersWidth = 4;
            Dgv.DefaultCellStyle.Font = new Font("Tahoma", 9);
            Dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9);
            Dgv.RowTemplate.Height = 30;

            Dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            Dgv.AllowUserToResizeRows = false;
            Dgv.AllowUserToResizeColumns = false;

        }

        public static void InitiSettings(DataGridView Dgv)
        {
            Dgv.Rows.Clear();

            int unit = 1;
            int addr = 0;
            for (int i = 0; i < 104; i++)
            {
                string clientAddress = $"{300001 + addr * 2}".Insert(3, " ");
                string serverAddress = $"{300001 + i * 2}".Insert(3, " ");
                Dgv.Rows.Add($"{i + 1}", "", $"#{unit}", clientAddress, 0, 999999999, 0, 999999999, serverAddress);

                if ((i + 1) % 8 == 0)
                {
                    unit += 1;
                    addr = 0;
                }
                else
                    addr += 1;
                //unit = ((i+1) % 8 == 0) ? unit+=1: unit;

            }

        }


        //public static void Settings(DataGridView Dgv, List<Setting> settings)
        //{
        //    Dgv.Rows.Clear();
        //    foreach (Setting setting in settings)
        //    {

        //        Dgv.Rows.Add(setting.No, "", setting.InputRegAddr, setting.InputL,
        //            setting.InputH, setting.OutputL, setting.OutputH, setting.ServerSideInputRegAddr);
        //    }

        //}


    }
}
