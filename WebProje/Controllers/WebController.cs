using Renci.SshNet.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebProje.Models;
using WebProje.Service;

namespace WebProje.Controllers
{
    public class WebController : Controller
    {
        Service.Service service = new Service.Service();
        // GET: Web

        public ActionResult Index()
        {
            if(Session["Izin_bilgileri"] == null) Session["Izin_bilgileri"] = "3";
            return View();
        }
        public ActionResult Login()
        {
            Session["Izin_Bilgileri"] = "3";
            Session["KullaniciAdi"] = null;
            Session["Sifre"] = null;
            Session["Checkbox"] = "0";
            if (Request.Cookies["Login"] != null)
            {
                HttpCookie Login = Request.Cookies["Login"];
                Session["KullaniciAdi"] = Login.Values["KullaniciAdi"];
                Session["Sifre"] = Login.Values["Sifre"];
                Session["Checkbox"] = "1";
            }
            return View();
        }
        public ActionResult AdminPaneli()
        {
            if (Session["Izin_bilgileri"] == null) Session["Izin_bilgileri"] = "3";
            return View();
        }
        public ActionResult Hakkimda()
        {
            if (Session["Izin_bilgileri"] == null) Session["Izin_bilgileri"] = "3";
            return View();
        }
        public ActionResult yazilim()
        {
            if (Session["Izin_bilgileri"] == null) Session["Izin_bilgileri"] = "3";
            return View();
        }
        public ActionResult kisisel()
        {
            if (Session["Izin_bilgileri"] == null) Session["Izin_bilgileri"] = "3";
            return View();
        }
        public ActionResult iletisim()
        {
            if (Session["Izin_bilgileri"] == null) Session["Izin_bilgileri"] = "3";
            return View();
        }
        public ActionResult egitim()
        {
            if (Session["Izin_bilgileri"] == null) Session["Izin_bilgileri"] = "3";
            return View();
        }
        public ActionResult amac()
        {
            if (Session["Izin_bilgileri"] == null) Session["Izin_bilgileri"] = "3";
            return View();
        }


        [HttpPost]
        public ActionResult DuyuruEkle(Duyuru duyuru)
        {
            service.DuyuruEkle(duyuru);
            return View("AdminPaneli");
        }
        public JsonResult IzinDegistir(string deger, string id)
        {
            service.KullaniciIzinDegistir(deger, id);
            return Json("");
        }
        public JsonResult Kayit(Kullanici kullanici) 
        {
            var data = service.KullaniciKontrol1(kullanici);
            if (data == "") return Json(service.KullaniciKayit(kullanici));
            else            return Json(data);
        }
        public JsonResult Giris(Kullanici kullanici, bool BeniHatirla)
        {
            var data = service.KullaniciGiris(kullanici);
            if (data.Id == null)
            {
                return Json("Bulunamadi");
            }
            else
            {
                Session["KullaniciId"] = data.Id;
                Session["Izin_bilgileri"] = data.Izin_bilgileri;
                if (BeniHatirla == true)
                {
                    HttpCookie Login= new HttpCookie("Login");
                    Login.Values.Add("KullaniciAdi", data.KullaniciAdi);
                    Login.Values.Add("Sifre", data.Sifre);
                    Login.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(Login);
                }
                else
                {
                    Response.Cookies["Login"].Expires = DateTime.Now.AddDays(-1);
                }
                return Json(data.Ad + " " + data.Soyad);
            }
        }
        public JsonResult SifremiUnuttum(Kullanici kullanici)
        {
            var data = service.KullaniciKontrol2(kullanici);
            if (data == "Yok") return Json("Bulunamadı");
            else
            {
                string path = Path.GetRandomFileName();
                path = path.Replace(".", "");
                service.YeniSifre(kullanici.KullaniciAdi, path);
                MailMessage mesaj = new MailMessage();
                mesaj.From = new MailAddress("yec.kisiselwebsitesi@yandex.com");
                mesaj.To.Add(kullanici.Email);
                mesaj.Subject = "YEC Yeni Şifre";
                mesaj.Body = "Yeni Şifreniz:" + path ;

                mesaj.IsBodyHtml = true; // giden mailin içeriği html olmasını istiyorsak true kalması lazım
                SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
                client.Credentials = new NetworkCredential("yec.kisiselwebsitesi@yandex.com", "123yec123");
                client.EnableSsl = true;
                client.Send(mesaj);
                return Json("");
            }

        }
        public JsonResult MesajGonder(MesajBilgileri mesajbilgileri)
        {
            MailMessage mesaj = new MailMessage();
            mesaj.From = new MailAddress("yec.kisiselwebsitesi@yandex.com");
            mesaj.To.Add("yec.kisiselwebsitesi@yandex.com");
            mesaj.Subject = "YEC'den Mesajınız Var.";
            mesaj.Body = "Adı:" + mesajbilgileri.adi + "\nSoyadı:" + mesajbilgileri.soyadi + "\nDoğum Tarihi:" + mesajbilgileri.dogumtarihi + "\nTelefon Numarası:" + mesajbilgileri.telno + "\nMesaj:" + mesajbilgileri.mesaj;
            mesaj.IsBodyHtml = true; // giden mailin içeriği html olmasını istiyorsak true kalması lazım
            SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
            client.Credentials = new NetworkCredential("yec.kisiselwebsitesi@yandex.com", "123yec123");
            client.EnableSsl = true;
            client.Send(mesaj);
            if (Session["Izin_bilgileri"] == "3")
            {
                service.MisafirMesaj(mesajbilgileri);
            }
            else
            {
                service.UyeMesaj(Session["KullaniciId"].ToString(), mesajbilgileri.mesaj);
            }
            return Json("");
        }
    }
}