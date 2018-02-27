using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGoT.Models.House;
using Character = WebGoT.Models.Character;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Test.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/
        public ActionResult Choix()
        {
            List<IndexViewModel> list = new List<IndexViewModel>();

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:56063/");

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/house/GetAllHouses").Result;
            if (response.IsSuccessStatusCode)
            {
                list = response.Content.ReadAsAsync<List<IndexViewModel>>().Result;
            }

            return View(list);
        }
        public ActionResult SelectHouse()
        {
            return View();
        }
        //
        // GET: /Game/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Game/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Game/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Game/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Game/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Game/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Game/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
