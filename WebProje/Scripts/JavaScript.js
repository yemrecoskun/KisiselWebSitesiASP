function Kayit() {
    var KullaniciAdi = $("#KullaniciAdi").val();
    var Sifre = $("#Sifre").val();
    var Sifre1 = $("#Sifre1").val();
    var Ad = $("#Ad").val();
    var Soyad = $("#Soyad").val();
    var email = $("#email").val();
    if (KullaniciAdi != "" && Sifre != "" && Ad != "" && Soyad != "" && email != "") {

        if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email)) {

            if (Sifre == Sifre1) {
                $.ajax({
                    type: 'POST',
                    url: '/Web/Kayit',
                    data: { KullaniciAdi: KullaniciAdi, Sifre: Sifre, Ad: Ad, Soyad: Soyad, email: email },
                    success: function (data) {
                        $('#Uyari').text(data);
                        document.getElementById('Uyari').style.display = 'block';
                    }
                })
            }
            else {
                $('#Uyari').text('Hata! Şifreler Aynı Değil');
                document.getElementById('Uyari').style.display = 'block';
            }
        }
        else {
            $('#Uyari').text('Hata! Lütfen Doğru Email Giriniz');;
            document.getElementById('Uyari').style.display = 'block';
        }
    }
    else {
        $('#Uyari').text('Lütfen Boşlukları doldurunuz');;
        document.getElementById('Uyari').style.display = 'block';
    }
}
function Giris() {
    var KullaniciAdi = $("#KullaniciAdiGiris").val();
    var Sifre = $("#SifreGiris").val();
    var BeniHatirla = document.getElementById("hatirla").checked;
    if (KullaniciAdi != "" && Sifre != "") {
        $.ajax({
            type: 'POST',
            url: '/Web/Giris',
            data: { KullaniciAdi: KullaniciAdi, Sifre: Sifre, BeniHatirla: BeniHatirla },
            success: function (data) {
                if (data == "Bulunamadi") {
                    $('#UyariGiris').text('Böyle Bir Kullanıcı Bulunamadı. Tekrar Deneyiniz');
                    document.getElementById('UyariGiris').style.display = 'block';
                }
                else {
                    document.getElementById('UyariGiris').style.display = 'none';
                    window.location.href = "/Web/Index";
                }
            }
        })
    }
    else {
        $('#UyariGiris').text('Lütfen Boşlukları doldurunuz');
        document.getElementById('UyariGiris').style.display = 'block';
    }
}
function SifremiUnuttum() {
    var KullaniciAdi = $("#KullaniciAdiUnuttum").val();
    var Email = $("#EmailUnuttum").val();
    if (KullaniciAdi != "" && Sifre != "") {
        $.ajax({
            type: 'POST',
            url: '/Web/SifremiUnuttum',
            data: { KullaniciAdi: KullaniciAdi, Email: Email },
            success: function (data) {
                if (data == "Bulunamadı") {
                    $('#UyariUnuttum').text('Böyle bir kayıtlı email veya kullanıcı adı bulunamadı');
                    document.getElementById('UyariUnuttum').style.display = 'block';
                }
                else {
                    alert("Yeni şifreniz email adresinize gönderilmiştir.");
                    document.getElementById('UyariUnuttum').style.display = 'none';
                }
            }
        })
    }
    else {
        $('#UyariUnuttum').text('Lütfen Boşlukları doldurunuz');
        document.getElementById('UyariUnuttum').style.display = 'block';
    }
}

function degistir(id) {
    var deger = $("select#asd").val();
    alert(deger);
    $.ajax({
        url: '/Web/IzinDegistir',
        type: 'POST',
        data: { deger: deger, id: id },
        success: function () {
            alert("İzin Değişti")
        }
    })
}