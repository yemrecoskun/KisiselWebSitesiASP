using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using MySql.Data;
using WebProje.Globals;
using WebProje.Models;

namespace WebProje.Entity
{
    public class KullaniciEntity
    {
        SQL sql = new SQL();
        public string KullaniciKontrol1(Kullanici kullanici)
        {
            sql.Open();
            var rd = sql.CmdReader("select * from kullanici where KullaniciAdi='" + kullanici.KullaniciAdi + "'");
            if (rd.Read())
            {
                sql.Close();
                return "Böyle bir Kullanıcı Adı bulunmaktadır! Lütfen Başka Kullanıcı Adı Deneyiniz";
            }
            rd.Close();
            rd = sql.CmdReader("select * from kullanici where Email='" + kullanici.Email + "'");
            if (rd.Read())
            {
                sql.Close();
                return "Email Kayıtlı! Başka Email deneyiniz";
            }
            sql.Close();
            return "";
        }
        public string KullaniciKontrol2(Kullanici kullanici)
        {
            sql.Open();
            var rd = sql.CmdReader("select * from kullanici where KullaniciAdi='" + kullanici.KullaniciAdi + "' and Email='" + kullanici.Email + "'");
            if (rd.Read())
            {
                sql.Close();
                return "Var";
            }
            sql.Close();
            return "Yok";
        }
        public Kullanici GetirKullaniciBilgileri(string id)
        {
            sql.Open();
            var rd = sql.CmdReader("select * from kullanici where Id='" + id + "'");
            Kullanici kullanici = new Kullanici();
            if (rd.Read())
            {
                kullanici.Id = rd.GetString("Id");
                kullanici.KullaniciAdi = rd.GetString("KullaniciAdi");
                kullanici.Ad = rd.GetString("Ad");
                kullanici.Email = rd.GetString("Email");
                kullanici.Soyad = rd.GetString("Soyad");
                kullanici.Izin_bilgileri = rd.GetString("Izin_Id");
            }
            sql.Close();
            return kullanici;
        }
        public string KullaniciKayit(Kullanici kullanici)
        {
            sql.CmdWriter("INSERT INTO kullanici(KullaniciAdi,Sifre,Ad,Soyad,Email,Izin_Id) VALUES('" + kullanici.KullaniciAdi + "', '" + kullanici.Sifre + "', '" + kullanici.Ad + "', '" + kullanici.Soyad + "', '" + kullanici.Email + "', '2')");
            return "Kullanıcı Kayıt Edildi.";
        }
        public Kullanici KullaniciGiris(Kullanici kullanici)
        {
            sql.Open();
            var rd = sql.CmdReader("select * from kullanici where KullaniciAdi='" + kullanici.KullaniciAdi + "' and Sifre='" + kullanici.Sifre + "'");
            if (rd.Read())
            {
                kullanici.Id = rd.GetString("Id");
                kullanici.Ad = rd.GetString("Ad");
                kullanici.Email = rd.GetString("Email");
                kullanici.Soyad = rd.GetString("Soyad");
                kullanici.Izin_bilgileri = rd.GetString("Izin_Id");
            }
            sql.Close();
            return kullanici;
        }
        public List<Kullanici> KullaniciListesi(string KullaniciAdi)
        {
            sql.Open();
            var rd = sql.CmdReader("select * from kullanici where KullaniciAdi != '" + KullaniciAdi + "'");
            List<Kullanici> KullaniciListesi = new List<Kullanici>();
            while (rd.Read())
            {
                Kullanici kullanici = new Kullanici();
                kullanici.Id = rd.GetString("Id");
                kullanici.KullaniciAdi = rd.GetString("KullaniciAdi");
                kullanici.Ad = rd.GetString("Ad");
                kullanici.Email = rd.GetString("Email");
                kullanici.Soyad = rd.GetString("Soyad");
                kullanici.Izin_bilgileri = rd.GetString("Izin_Id");
                KullaniciListesi.Add(kullanici);
            }
            sql.Close();
            return KullaniciListesi;
        }
        public void YeniSifre(string kullaniciadi, string yenisifre)
        {
            sql.CmdWriter("update kullanici set Sifre = '" + yenisifre + "' where KullaniciAdi='" + kullaniciadi + "';");
        }
        public void MisafirMesaj(MesajBilgileri mesaj)
        {
            sql.CmdWriter("INSERT INTO MisafirMesajlar(Adi,Soyadi,DogumTarihi,TelNo,Email,Mesaj) VALUES('" + mesaj.adi + "', '" + mesaj.soyadi + "', '" + mesaj.dogumtarihi + "', '" + mesaj.telno + "', '" + mesaj.email + "', '" + mesaj.mesaj + "')");
        }
        public void UyeMesaj(string id, string mesaj)
        {
            sql.CmdWriter("INSERT INTO UyeMesajlar(Kullanici_Id,Mesaj) VALUES('" + id + "', '" + mesaj + "')");
        }
        public List<UyeMesajBilgileri> UyeMesajListesi()
        {
            sql.Open();
            var rd = sql.CmdReader("select KullaniciAdi,Ad,Soyad,Email,Mesaj from kullanici kul inner join UyeMesajlar uye on kul.Id = uye.Kullanici_Id");
            List<UyeMesajBilgileri> UyeMesajlarListesi = new List<UyeMesajBilgileri>();
            while (rd.Read())
            {
                UyeMesajBilgileri uyemesaj = new UyeMesajBilgileri();
                uyemesaj.KullaniciAdi = rd.GetString("KullaniciAdi");
                uyemesaj.Ad = rd.GetString("Ad");
                uyemesaj.Soyad = rd.GetString("Soyad");
                uyemesaj.Email = rd.GetString("Email");
                uyemesaj.Mesaj = rd.GetString("Mesaj");
                UyeMesajlarListesi.Add(uyemesaj);
            }
            sql.Close();
            return UyeMesajlarListesi;
        }
        public List<MesajBilgileri> MisafirMesajListesi()
        {
            sql.Open();
            var rd = sql.CmdReader("select * from MisafirMesajlar");
            List<MesajBilgileri> MesajlarListesi = new List<MesajBilgileri>();
            while (rd.Read())
            {
                MesajBilgileri mesaj = new MesajBilgileri();
                mesaj.adi = rd.GetString("Adi");
                mesaj.soyadi = rd.GetString("Soyadi");
                mesaj.dogumtarihi = rd.GetString("DogumTarihi");
                mesaj.telno = rd.GetString("TelNo");
                mesaj.email = rd.GetString("Email");
                mesaj.mesaj = rd.GetString("Mesaj");
                MesajlarListesi.Add(mesaj);
            }
            sql.Close();
            return MesajlarListesi;
        }
    }
}