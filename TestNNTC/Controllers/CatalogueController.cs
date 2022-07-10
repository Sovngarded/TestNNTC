using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestNNTC.DAL;
using TestNNTC.DAL.Entities;
using TestNNTC.DAL.ViewModel;

namespace TestNNTC.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly TestNNTCDbContext _db;

        public CatalogueController(TestNNTCDbContext db)
        {
            _db = db;
        }

        // Отправление каталога на вью
        public ActionResult Index()
        {
            var Catalogue = this._db.CatalogueList.Include(m => m.CatalogueProducts).Select(m => new CatalogueViewModel
            {
                CategoryName = m.CategoryName,
                CatalogueDataProducts = m.CatalogueProducts
               
            });
            return View(Catalogue);
        }


        //Создание обьекта каталога
        public ActionResult CreateCatalogueItem()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCatalogueItem(CatalogueDataEntity obj)
        {
            _db.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // Удаление предмета каталога
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCatalogueItem(int? id)
        {
            var obj = _db.CatalogueList.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.CatalogueList.Remove(obj);
            _db.SaveChanges();


            return RedirectToAction("Index");
        }


        // Обновление предмета каталога

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCatalogueItem(CatalogueDataEntity obj)
        {
            if (ModelState.IsValid)
            {
                _db.CatalogueList.Update(obj);
                _db.SaveChanges();
            }


            return RedirectToAction("Index");
        }




        // Показывает весь каталог
        public ActionResult DownLoadFile()
        {
            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);
            int totalSum = 0;
            int everySecondSum = 0;
            var firstIdx = 1;
            var takeEvery = 2;


            List<CatalogueDataEntity> objList = _db.CatalogueList.Include("CatalogueProducts").ToList();
            

            foreach (var catalogueName in objList)
            {
                tw.WriteLine(catalogueName.CategoryName);
                foreach(var catalogueitem in catalogueName.CatalogueProducts)
                {
                    tw.Write("Название: {0}; Описание: {1}; Цена: {2};" , catalogueitem.ProductName, catalogueitem.Description, catalogueitem.Cost);
                    tw.WriteLine();
                    totalSum += catalogueitem.Cost;
                }
                foreach(var catalogueCost in catalogueName.CatalogueProducts
                    .Skip(firstIdx)
                    .Where((elem, idx) => idx % takeEvery == 0))
                {
                    everySecondSum += catalogueCost.Cost;
                }
            }
            tw.WriteLine("Общая сумма: {0} , Сумма каждой второй категории: {1}",totalSum,everySecondSum);
            tw.Flush();
            tw.Close();

            return File(memoryStream.GetBuffer(), "text/plain", "file.txt");
        }


        //Показывает только 2 дочерних предмета каталог

       public ActionResult DownLoadEveryTwoFile()
        {
            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);
            int totalSum = 0;
            int everySecondSum = 0;
            var firstIdx = 1;
            var takeEvery = 2;

            List<CatalogueDataEntity> objList = _db.CatalogueList.Include("CatalogueProducts").ToList();
             foreach (var catalogueName in objList)
            {
                tw.WriteLine(catalogueName.CategoryName);
                foreach(var catalogueitem in catalogueName.CatalogueProducts.Take(2))
                {
                    tw.Write("Название: {0}; Описание: {1}; Цена: {2};" , catalogueitem.ProductName, catalogueitem.Description, catalogueitem.Cost);
                    tw.WriteLine();
                    totalSum += catalogueitem.Cost;
                }
                foreach(var catalogueCost in catalogueName.CatalogueProducts
                    .Take(2)
                    .Skip(firstIdx)
                    .Where((elem, idx) => idx % takeEvery == 0))
                { 
                    everySecondSum += catalogueCost.Cost;
                }
            }
            tw.WriteLine();
            tw.WriteLine("Общая сумма: {0} , Сумма каждой второй категории: {1}", totalSum, everySecondSum);
            tw.Flush();
            tw.Close();


            return File(memoryStream.GetBuffer(), "text/plain", "file.txt");
        }


    }
}
