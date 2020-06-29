using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProje.Entity;
using WebProje.Models;

namespace WebProje.Service
{
    public class Service
    {
        KullaniciEntity kullanicientity = new KullaniciEntity();
        AdminEntity adminentity = new AdminEntity();
        public string KullaniciKontrol1(Kullanici kullanici)
        {
            return kullanicientity.KullaniciKontrol1(kullanici);
        }
        public string KullaniciKontrol2(Kullanici kullanici)
        {
            return kullanicientity.KullaniciKontrol2(kullanici);
        }
        public string KullaniciKayit(Kullanici kullanici)
        {
            return kullanicientity.KullaniciKayit(kullanici);
        }
        public Kullanici KullaniciGiris(Kullanici kullanici)
        {
            return kullanicientity.KullaniciGiris(kullanici);
        }
        public List<Kullanici> KullaniciListesi(string KullaniciAdi)
        {
            return kullanicientity.KullaniciListesi(KullaniciAdi);
        }
        public List<Duyuru> DuyuruListesi()
        {
            return adminentity.DuyuruListesi();
        }
        public List<Izin> IzinListesi()
        {
            return adminentity.IzinListesi();
        }
        public void DuyuruEkle(Duyuru duyuru)
        {
            adminentity.DuyuruEkle(duyuru);
        }
        public void KullaniciIzinDegistir(string deger, string id)
        {
            adminentity.KullaniciIzinDegistir(deger, id);
        }
        public void YeniSifre(string kullaniciadi, string yenisifre)
        {
            kullanicientity.YeniSifre(kullaniciadi, yenisifre);
        }
        public Kullanici GetirKullaniciBilgileri(string id)
        {
            return kullanicientity.GetirKullaniciBilgileri(id);
        }
        public void MisafirMesaj(MesajBilgileri mesaj)
        {
            kullanicientity.MisafirMesaj(mesaj);
        }
        public void UyeMesaj(string id, string mesaj)
        {
            kullanicientity.UyeMesaj(id, mesaj);
        }
        public List<UyeMesajBilgileri> UyeMesajListesi()
        {
            return kullanicientity.UyeMesajListesi();
        }
        public List<MesajBilgileri> MisafirMesajListesi()
        {
            return kullanicientity.MisafirMesajListesi();
        }
    }
}