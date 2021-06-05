using Microsoft.AspNetCore.Mvc;
using System;
using Lab3.Models;

namespace Lab3.Controllers
{
    public class QuizServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IActionResult Quiz()
        {
            QuizModel qModel = QuizModel.Instance;
            qModel.Reset();
            qModel.Start();


            return View(qModel);
        }

        [HttpPost]
        public IActionResult Quiz(QuizModel qModel, string action)
        {
            qModel = QuizModel.Instance;

            if (ModelState.IsValid)

            {
                if (Request.Form["Answer"] != "") qModel.UserAnswer = Int32.Parse(Request.Form["Answer"]);
                else qModel.UserAnswer = 0;
                qModel.Questions();



                if (action == "Next")
                {
                    QuizModel quModel = QuizModel.Instance;
                    quModel.Start();

                    return View(quModel);
                }
                return RedirectToAction("QuizResult");
            }
            else
            {

                QuizModel quModel = QuizModel.Instance;
                return View(quModel);
            }

        }
        public IActionResult QuizResult()
        {
            QuizModel qModel = QuizModel.Instance;
            ViewBag.Result = qModel.AllAnswers;
            ViewData["Всего"] = "" + qModel.Count;
            ViewData["Правильно"] = "" + qModel.CountOfRightAnswers;
            return View();
        }



    }
}