using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

namespace WebService3
{
    public class QuanLyTagHangHoa
    {
        #region Struct
        public class LoaiHangHoa
        {
            public decimal id { get; set; }
            public string ma_tag { get; set; }
            public string ten_tag { get; set; }
            public string link_anh { get; set; }
        }
        public class HangHoa
        {
            public string ma_hang_hoa { get; set; }
            public string ten_hang_hoa { get; set; }
        }
        public class Tag
        {
            public decimal id { get; set; }
            public string ten_tag { get; set; }
        }
        public class LoaiTag
        {
            public decimal id { get; set; }
            public string ma_loai_tag { get; set; }
            public string ten_loai_tag { get; set; }
            public List<Tag> ds_tag { get; set; }
        }
        #endregion
        #region function
        public static void ThemTagHangHoa(string ten_tag, string ten_loai_tag, string link_anh)
        {
            using (var scope = new TransactionScope())
            {
                try
                {
                    using (var context = new TKHTQuanLyBanHangEntities())
                    {
                        //them tag
                        var checkTag = context.GD_TAG.Where(s => s.TEN_TAG == ten_tag).FirstOrDefault();
                        if (checkTag == null)
                        {
                            var tag = new GD_TAG();
                            tag.TEN_TAG = ten_tag;
                            if (link_anh != null)
                            {
                                tag.LINK_ANH = link_anh;
                            }

                            context.GD_TAG.Add(tag);
                            context.SaveChanges();
                            //them tag chi tiet
                            if (ten_loai_tag != null)
                            {
                                var tagChiTiet = new GD_LOAI_TAG_CHI_TIET();
                                tagChiTiet.ID_LOAI_TAG = context.DM_LOAI_TAG.Where(s => s.TEN_LOAI_TAG == ten_loai_tag).First().ID;
                                tagChiTiet.ID_TAG = context.GD_TAG.Where(s => s.TEN_TAG == ten_tag).First().ID;
                                context.GD_LOAI_TAG_CHI_TIET.Add(tagChiTiet);
                                context.SaveChanges();
                            }

                        }
                    }
                    scope.Complete();
                }
                catch (Exception v_e)
                {
                    scope.Dispose();
                    throw v_e;
                }
                finally
                {

                }
            }

        }
        public static void GanTagHangHoa(string ten_hang_hoa, string ten_tag)
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var tagHangHoa = new GD_HANG_HOA_TAG();
                tagHangHoa.ID_HANG_HOA = context.DM_HANG_HOA.Where(s => s.TEN_HANG_HOA == ten_hang_hoa).First().ID;
                tagHangHoa.ID_TAG = context.GD_TAG.Where(s => s.TEN_TAG == ten_tag).First().ID;
                context.GD_HANG_HOA_TAG.Add(tagHangHoa);
                context.SaveChanges();
            }
        }
        public static List<string> LayDanhSachTag()
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var gd_tag = context.GD_TAG.ToList();
                var listTag = new List<string>();
                foreach (var item in gd_tag)
                {
                    listTag.Add(item.TEN_TAG);
                }
                return listTag;
            }
           
        }

        public static List<HangHoa> LayDanhSachHangHoa()
        {
            using (var context = new TKHTQuanLyBanHangEntities())
            {
                var hanghoa = context.DM_HANG_HOA.ToList();
                var list = new List<HangHoa>();
                foreach (var item in hanghoa)
                {
                    var temp = new HangHoa();
                    temp.ma_hang_hoa = item.MA_HANG_HOA;
                    temp.ten_hang_hoa = item.TEN_HANG_HOA;
                    list.Add(temp);
                }
                return list;
            }

        }
        #endregion
    }
}