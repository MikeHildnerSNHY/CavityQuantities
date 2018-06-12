using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CavityQuantities.Models
{
    partial class IM_IM
    {
        public string Family4
        {
            get
            {
                //Debug.WriteLine($"Part_Number: {Part_Number}");

                if (Family == null || Family.Length < 4)
                {
                    if(Long_Item_Nbr == null || Long_Item_Nbr.Length < 4)
                    {
                        return Long_Item_Nbr;
                    }

                    return Long_Item_Nbr.Substring(0, 4);
                }
                else
                {
                    if (Family.Length < 4)
                    {
                        return Family;
                    }

                    return Family.Substring(0, 4);
                }
            }
        }

        public string PartNbr
        {
            get { return Part_Number; }
        }

        public string Family5
        {
            get
            {
                //Debug.WriteLine($"Part_Number: {Part_Number}");

                if (Family == null || Family.Length < 5)
                {
                    if (Long_Item_Nbr == null || Long_Item_Nbr.Length < 5)
                    {
                        return Long_Item_Nbr;
                    }

                    return Long_Item_Nbr.Substring(0, 5);
                }
                else
                {
                    if (Family.Length < 5)
                    {
                        return Family;
                    }

                    return Family.Substring(0, 5);
                }
            }
        }
    }
}
