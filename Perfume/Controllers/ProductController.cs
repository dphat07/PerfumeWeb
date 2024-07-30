using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Perfume.App_Start;
using Perfume.Models;
namespace Perfume.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        Perfume4Entities2 db = new Perfume4Entities2();
        public ActionResult DetailsAccount(string id)
        {
            TaiKhoan tk;

            tk = db.TaiKhoans.FirstOrDefault(t => t.tentaikhoan == id);
            if (tk == null)
                return HttpNotFound();
            return View(tk);
            //tbl_Deparment dept;
            //dept = db.tbl_Deparment.FirstOrDefault(d => d.DeptId == id);
            //if (dept == null)
            //    return HttpNotFound();
            //return View(dept);
        }
        public ActionResult EditsAccount(int id )
        {
            TaiKhoan tk;
            tk = db.TaiKhoans.FirstOrDefault(t => t.idTaiKhoan == id);
            if (tk == null)
                return HttpNotFound();
            return View(tk);
        }
        [HttpPost]
        public ActionResult EditsAccount(TaiKhoan tk)
        {
            if (ModelState.IsValid)
            {
                if (db.TaiKhoans.Any(t => t.tentaikhoan == tk.tentaikhoan))
                {
                    ViewBag.thongbao = "Tên đăng nhập đã tồn tại";
                    return View();
                }


                db.TaiKhoans.Attach(tk);
                db.Entry(tk).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                SessionConfig.SetUser(tk);
                var us = SessionConfig.GetUser();
                return RedirectToAction("Product", "Product");
            }
            return View(tk);
        }

        private static int kieuSapXep = -1;

        public List<sanPham> getListProduct()
        {
            List<sanPham> listProduct = Session["Product"] as List<sanPham>;
            if (listProduct == null)
            {
                listProduct = new List<sanPham>();
                listProduct = db.sanPhams.ToList();
                Session["Product"] = listProduct;
            }
            return listProduct;
        }

        public ActionResult Product()
        {
            var lstNuocHoa = getListProduct();
            return View(lstNuocHoa);
        }

        public ActionResult SapXep(int stt)
        {
            var lstNuocHoa = getListProduct();
            Session["Product"] = GTSapXep(stt, lstNuocHoa);
            return RedirectToAction("Product", "Product");
        }

        public List<sanPham> GTSapXep(int stt, List<sanPham> lstNuocHoa)
        {
            if (stt == 1)
            {
                lstNuocHoa = lstNuocHoa.OrderBy(nh => nh.tenSanPham).ToList();
                kieuSapXep = 1;
            }
            else if (stt == 2)
            {
                lstNuocHoa = lstNuocHoa.OrderByDescending(nh => nh.tenSanPham).ToList();
                kieuSapXep = 2;
            }
            else if (stt == 3)
            {
                lstNuocHoa = lstNuocHoa.OrderBy(nh => nh.gia).ToList();
                kieuSapXep = 3;
            }
            else
            {
                lstNuocHoa = lstNuocHoa.OrderByDescending(nh => nh.gia).ToList();
                kieuSapXep = 4;
            }
            return lstNuocHoa;
        }


        public List<int> getDsFilterThuongHieu()
        {
            List<int> list = Session["ThuongHieu"] as List<int>;
            if (list == null)
            {
                list = new List<int>();
                Session["ThuongHieu"] = list;
            }
            return list;
        }

        public List<int> getDsFilterGia()
        {
            List<int> list = Session["Gia"] as List<int>;
            if (list == null)
            {
                list = new List<int>();
                Session["Gia"] = list;
            }
            return list;
        }
        public List<int> getDsFilterLoai()
        {
            List<int> list = Session["Loai"] as List<int>;
            if (list == null)
            {
                list = new List<int>();
                Session["Loai"] = list;
            }
            return list;
        }

        public void addDsFilter(List<int> list, int i)
        {
            if (list.IndexOf(i) == -1)
            {
                list.Add(i);
            }
            else
            {
                list.Remove(i);
            }
        }

        public ActionResult FilterProduct(string name, int i)
        {
            bool flag = false;
            List<sanPham> lstNuocHoa = db.sanPhams.ToList();
            Session["Product"] = lstNuocHoa;
            if (kieuSapXep != -1)
                lstNuocHoa = GTSapXep(kieuSapXep, lstNuocHoa);
            List<sanPham> listSP = new List<sanPham>();
            List<sanPham> list = new List<sanPham>();
            switch (name)
            {
                case "TH":
                    {
                        addDsFilter(getDsFilterThuongHieu(), i);
                        break;
                    }
                case "GI":
                    {
                        addDsFilter(getDsFilterGia(), i);
                        break;
                    }
                case "LO":
                    {
                        addDsFilter(getDsFilterLoai(), i);
                        break;
                    }
            }
            foreach (int x in getDsFilterThuongHieu())
            {
                flag = true;
                list = lstNuocHoa.Where(t => t.idThuongHieu == x).ToList();
                foreach (sanPham y in list)
                {
                    if (listSP.Find(t => t.id == y.id) == null)
                    {
                        listSP.Add(y);
                    }
                }
            }
            if (getDsFilterGia().Count != 0 && getDsFilterThuongHieu().Count != 0)
            {
                lstNuocHoa = listSP;
                listSP = new List<sanPham>();
            }
            foreach (int x in getDsFilterGia())
            {
                flag = true;
                switch (x)
                {
                    case 1:
                        {
                            list = lstNuocHoa.Where(t => t.gia < 100000).ToList();
                            foreach (sanPham y in list)
                            {
                                if (listSP.Find(t => t.id == y.id) == null)
                                {
                                    listSP.Add(y);
                                }
                            }
                            break;
                        }
                    case 2:
                        {
                            list = lstNuocHoa.Where(t => t.gia >= 100000 && t.gia < 200000).ToList();
                            foreach (sanPham y in list)
                            {
                                if (listSP.Find(t => t.id == y.id) == null)
                                {
                                    listSP.Add(y);
                                }
                            }
                            break;
                        }
                    case 3:
                        {
                            list = lstNuocHoa.Where(t => t.gia >= 200000 && t.gia < 300000).ToList();
                            foreach (sanPham y in list)
                            {
                                if (listSP.Find(t => t.id == y.id) == null)
                                {
                                    listSP.Add(y);
                                }
                            }
                            break;
                        }
                    case 4:
                        {
                            list = lstNuocHoa.Where(t => t.gia >= 300000 && t.gia < 500000).ToList();
                            foreach (sanPham y in list)
                            {
                                if (listSP.Find(t => t.id == y.id) == null)
                                {
                                    listSP.Add(y);
                                }
                            }
                            break;
                        }
                    case 5:
                        {
                            list = lstNuocHoa.Where(t => t.gia >= 500000 && t.gia < 1000000).ToList();
                            foreach (sanPham y in list)
                            {
                                if (listSP.Find(t => t.id == y.id) == null)
                                {
                                    listSP.Add(y);
                                }
                            }
                            break;
                        }
                    case 6:
                        {
                            list = lstNuocHoa.Where(t => t.gia >= 1000000).ToList();
                            foreach (sanPham y in list)
                            {
                                if (listSP.Find(t => t.id == y.id) == null)
                                {
                                    listSP.Add(y);
                                }
                            }
                            break;
                        }
                }
            }
            if (getDsFilterLoai().Count != 0 && getDsFilterGia().Count != 0)
            {
                lstNuocHoa = listSP;
                listSP = new List<sanPham>();
            }
            foreach (int x in getDsFilterLoai())
            {
                flag = true;
                list = lstNuocHoa.Where(t => t.idLoai == x).ToList();
                foreach (sanPham y in list)
                {
                    if (listSP.Find(t => t.id == y.id) == null)
                    {
                        listSP.Add(y);
                    }
                }
            }

            if (listSP.Count == 0)
            {
                if (!flag)
                {
                    if (kieuSapXep != -1)
                        lstNuocHoa = GTSapXep(kieuSapXep, lstNuocHoa);
                    Session["Product"] = lstNuocHoa;
                }
                else
                {
                    Session["Product"] = listSP;
                }
            }
            else
            {
                if (kieuSapXep != -1)
                    listSP = GTSapXep(kieuSapXep, listSP);
                Session["Product"] = listSP;
            }
            return RedirectToAction("Product", "Product");
        }

        public ActionResult LoaiPartial()
        {
            ViewData["listLoaiChon"] = getDsFilterLoai();
            var dsLoai = db.Loais.ToList();
            return View(dsLoai);
        }

        public ActionResult ThuongHieuPartial()
        {
            ViewData["listThuongHieuChon"] = getDsFilterThuongHieu();
            var dsThuongHieu = db.thuongHieux.ToList();
            return View(dsThuongHieu);
        }
        public ActionResult GiaPartial()
        {
            ViewData["listGiaChon"] = getDsFilterGia();
            var dsThuongHieu = db.thuongHieux.ToList();
            return View(dsThuongHieu);
        }

        public ActionResult listChonPartial()
        {
            ViewData["listLoaiChon"] = getDsFilterLoai();
            ViewData["listThuongHieuChon"] = getDsFilterThuongHieu();
            ViewData["listGiaChon"] = getDsFilterGia();
            ViewData["DataLoai"] = db.Loais.ToList();
            ViewData["DataThuongHieu"] = db.thuongHieux.ToList();
            return View();
        }

        public ActionResult chonDetails(string name, int i)
        {
            getDsFilterGia().Clear();
            getDsFilterThuongHieu().Clear();
            getDsFilterLoai().Clear();
            return RedirectToAction("FilterProduct", "Product", new { name = name, i = i });
        }

    }
}