using Jobs.Offer.Wbesite.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
          
            return View(db.Categories.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var LoginInfo = new NetworkCredential("saharnasr690@gmail.com", "password");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("saharnasr690@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "إسم المرسل:" + contact.Name + "<br>" +
                          "بريد المرسل:" + contact.Email + "<br>" +
                          "عنوان الرسالة:" + contact.Subject +"<br>"+
                          "محتوى الرسالة: <b>" + contact.Message+"</b>" ;

            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = LoginInfo;
            smtpClient.Send(mail);


            return RedirectToAction("Index");
        }


        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if (job==null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId; //store job id in session
            return View(job);
        }

        [Authorize]
        public ActionResult Apply()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];  // get job id  stored in session
            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == a.UserId).ToList();

            if (check.Count<1)
            {
                var job = new ApplyForJob();

                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "تم التقدم للوظيفة بنجاح";
            }
            else
            {
                ViewBag.Result = "المعذرة ، لقد تقدمت لهذه الوظيفة من قبل . قم بإختيار وظيفة أخرى";
            }
            

            return View();
        }

        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(a => a.UserId == a.UserId);
            return View(jobs.ToList());
        }


        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);

        }


        public ActionResult Edit(int id)
        {

            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }


        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }

        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {

            var MyJob = db.ApplyForJobs.Find(job.Id);
            db.ApplyForJobs.Remove(MyJob);
            db.SaveChanges();
            return RedirectToAction("GetJobsByUser");




        }
        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();

            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserID
                       select app;

            var grouped = from j in Jobs
                          group j by j.job.JobTitle
                         into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };

            return View(grouped.ToList());
        }


        public ActionResult Search()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)||
                                            a.JobContent.Contains(searchName)||
                                            a.Category.CategoryName.Contains(searchName)||
                                            a.Category.CategoryDescription.Contains(searchName)).ToList();
            return View(result);
        }

    }
}