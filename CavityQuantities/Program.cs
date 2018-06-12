using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CavityQuantities.Models;
using System.Diagnostics;

namespace CavityQuantities
{
    class Program
    {
        private static M2K_DATA_WHSEEntities _M2KDb = new M2K_DATA_WHSEEntities();
        private static List<IM_IM> _ims;
        private static List<IM_IM> _ims4;
        private static List<IM_IM> _ims5;
        private static List<IM_IM> _ims6;

        static void Main(string[] args)
        {
            BuildFamilyLists();

            ReadExcel();
        }

        private static void BuildFamilyLists()
        {
            _ims = _M2KDb.IM_IM.Where(i => i.Fac_Code != "02" && (i.Inventory_Type == "CT" || i.Inventory_Type == "CL")).ToList();
        }

        private static void ReadExcel()
        {
            string fileName = @"e:\Source\Repos\CavityQuantities\CavityQuantities\InputFiles\Models-Series.xlsx";
            using (var wb = new XLWorkbook(fileName))
            using (var ws = wb.Worksheet(1))
            {
                int rowLocation = 0;

                // 5 letter model codes.
                foreach (IXLRow row in ws.RowsUsed())
                {
                    rowLocation++;
                    Debug.WriteLine($"Processing row {rowLocation}");

                    if (rowLocation == 1) { continue; }  // Skip header row.

                    string model = row.Cell(1).GetValue<string>();

                    if (model.Length != 5) { continue; } // Only 5 letter model codes.

                    int qty = GetSalesQuantity(model);

                    ws.Cell(rowLocation, 4).SetValue<int>(qty);
                }

                rowLocation = 0;

                // 4 letter model codes.
                foreach (IXLRow row in ws.RowsUsed())
                {
                    rowLocation++;
                    Debug.WriteLine($"Processing row {rowLocation}");

                    if (rowLocation == 1) { continue; }  // Skip header row.

                    string model = row.Cell(1).GetValue<string>();

                    if (model.Length != 4) { continue; } // Only 4 letter model codes.

                    int qty = GetSalesQuantity(model);

                    ws.Cell(rowLocation, 4).SetValue<int>(qty);
                }

                wb.Save();
            }

            Process.Start(fileName);

        }

        private static int GetSalesQuantity(string model)
        {
            List<string> partNbrs = new List<string>();


            switch (model.Length)
            {
                case 4:
                    partNbrs = _ims.Where(i => i.Family4 == model).Select(i => i.Part_Number).Distinct().ToList();
                    break;

                case 5:
                    partNbrs = _ims.Where(i => i.Family5 == model).Select(i => i.Part_Number).Distinct().ToList();
                    break;

                default:
                    throw new NotSupportedException($"Model length of  '{model.Length}' is not supported.");

            }

            DateTime dateStart = new DateTime(2017, 6, 1);
            DateTime dateEnd = new DateTime(2018, 5, 31);

            int? quantity = _M2KDb.vSA_Sales.Where(s => partNbrs.Contains(s.Part_Nbr) && s.Inv_So_Date >= dateStart && s.Inv_So_Date <= dateEnd).Sum(s => s.Quantity);

            // Remove part numbers from the IM list, so we don't double-count.
            _ims.RemoveAll(i => partNbrs.Contains(i.Part_Number));

            if(quantity.HasValue)
            {
                return quantity.Value;
            }
            else
            {
                return 0;
            }
        }
    }
}
