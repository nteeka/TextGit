using Lab03_NguyenTuongKhang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab03_NguyenTuongKhang.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var all_sach = from tt in data.Saches select tt;
            return View(all_sach);
        }
        public ActionResult Detail(int id)
        {
            var D_sach = data.Saches.Where(m => m.masach == id).First();
            return View(D_sach);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Sach sach)
        {
            var ten = collection["tensach"];
           
            var hinh = collection["hinh"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't Empty";
            }
            else
            {
                
                sach.tensach = ten;
               
                sach.hinh = hinh;
                data.Saches.InsertOnSubmit(sach);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Edit(int? id)
        {
            var E_category = data.Saches.First(m => m.masach == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var sach = data.Saches.First(m => m.masach == id);
            var E_tenloai = collection["tensach"];
            sach.maloai = id;
            if (string.IsNullOrEmpty(E_tenloai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                sach.tensach = E_tenloai;
                UpdateModel(sach);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_sach = data.Saches.First(m => m.masach == id);
            return View(D_sach);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_sach = data.Saches.Where(m => m.masach == id).First();
            data.Saches.DeleteOnSubmit(D_sach);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}
