using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace ZhiliBiliUBabusi3VeselihGuysa
{
    internal class HelperFunctions
    {
        public static void SaveResults(string email, int timeLeft, bool math)
        {
            if (email == "" || email == null)
                return;
            if(Interaction.MsgBox("Kas sa tahad sinu tulemused salvestada?", MsgBoxStyle.YesNo, Title: "Tulemused") == MsgBoxResult.Yes)
            {
                ApplicationContext.SaveRes(email, timeLeft, math);
            }
        }
    }
}
