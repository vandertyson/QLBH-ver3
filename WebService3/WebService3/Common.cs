using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService3
{
    public class Common
    {
        public class ThangNam
        {
            public int thang { get; set; }
            public int nam { get; set; }
        }
        public static List<decimal> TachID(string input)
        {
            var dsID = input.Split(',').ToList();
            var listID = new List<decimal>();
            foreach (var item in dsID)
            {
                listID.Add(decimal.Parse(item));
            }
            return listID;
        }
        public static List<ThangNam> lay_cac_thang_tiep_theo(string bat_dau, int so_thang)
        {
            List<ThangNam> result = new List<ThangNam>();
            int thang = Convert.ToDateTime(bat_dau).Month;
            int nam = Convert.ToDateTime(bat_dau).Year;
            for (int i = 0; i < so_thang; i++)
            {
                thang += 1;
                if (thang % 13 == 0)
                {
                    thang = 1;
                    nam += 1;
                }
                ThangNam p = new ThangNam();
                p.thang = thang;
                p.nam = nam;
                result.Add(p);
            }
            return result;
        }
        public static string GenMa(string key, int num, string last)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                string lastMaHangHoa = key;
                for (int i = 0; i < num - 1; i++)
                {
                    lastMaHangHoa += "0";
                }
                lastMaHangHoa += "1";
                if (last != null)
                {
                    lastMaHangHoa = last;
                }
                while (char.IsLetter(lastMaHangHoa[0]))
                {
                    lastMaHangHoa = lastMaHangHoa.Substring(key.Length);
                }
                var iden = (long.Parse(lastMaHangHoa) + 1).ToString();
                string space = "";
                for (int i = 0; i < num - iden.Length; i++)
                {
                    space += "0";
                }
                string hangHoaMoi = key + space + iden;
                return hangHoaMoi;
            }
        }
    }
}