using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProje.Globals;
using WebProje.Models;

namespace WebProje.Entity
{
    public class AdminEntity
    {
        SQL sql = new SQL();
        public List<Duyuru> DuyuruListesi()
        {
            sql.Open();
            var rd = sql.CmdReader("select * from duyurular");
            List<Duyuru> DuyuruListesi= new List<Duyuru>();
            while (rd.Read())
            {
                Duyuru duyuru = new Duyuru();
                duyuru.Id = rd.GetString("Id");
                duyuru.aciklama = rd.GetString("aciklama");
                DuyuruListesi.Add(duyuru);
            }
            sql.Close();
            return DuyuruListesi;
        } 
        public List<Izin> IzinListesi()
        {
            sql.Open();
            var rd = sql.CmdReader("select * from izinler");
            List<Izin> IzinListesi = new List<Izin>();
            while (rd.Read())
            {
                Izin izin = new Izin();
                izin.Id = rd.GetString("Id");
                izin.IzinAdı = rd.GetString("Izin");
                IzinListesi.Add(izin);
            }
            sql.Close();
            return IzinListesi;
        }
        public void DuyuruEkle(Duyuru duyuru)
        {
            sql.CmdWriter("insert into duyurular(aciklama) value('"+duyuru.aciklama+"')");
        }
        public void KullaniciIzinDegistir(string deger, string id)
        {
            sql.CmdWriter("update kullanici set Izin_Id = '" + deger + "' where Id='" + id + "';");
        }
    }
}