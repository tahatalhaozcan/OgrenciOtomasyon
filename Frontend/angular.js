var app = angular.module("by", ["ngRoute"]);
app.config(function ($routeProvider) {
    $routeProvider
        .when("/", {
            templateUrl: "index.html"
        })
        .when("/siparis", {
            templateUrl: "siparis.html",
            controller: "siparisCtrl"
        })
        .when("/giris", {
            templateUrl: "girisyap.html",
            controller: "girisCtrl"
        })
        .when("/kayit", {
        templateUrl: "kayit.html",
        controller: "kayitCtrl"
        })
        .when("/personel", {
            templateUrl: "personeller.html",
            controller: "personelCtrl"

        });

});
app.controller('rezervasyonCtrl', function ($scope, $http) {
    $http.get("http://localhost:54146/api/Ogrenci/OgrencileriGetir")
        .then(function (response) {
            $scope.ogrencilerigetir = response.data;
        });
    $http.get("http://localhost:56525/api/Urun/UrunleriGetir")
        .then(function (response) {
            $scope.urunlerigetir = response.data;
        });
    $http.get("http://localhost:56525/api/Personel/PersonelleriGetir")
        .then(function (response) {
            $scope.personellerigetir = response.data;
        });

    $scope.bilgigetir = function (oid) {
        $http.get("http://localhost:54146/api/Siparis/RezervasyonlariGoster?ogrenciID=" + oid)
            .then(function (response) {
                $scope.rdetay = response.data;
            });
    }
    $scope.siparissil = function (sid) {
        alert("Tamamlandı");
        $http.get("http://localhost:54146/api/Siparis/SiparisSil?siparisID=" + sid)
            .then(function (response) {
                $scope.rdetay = response.data;
            });
    }
    $scope.siparisver = function (mid, pid, uid, adet) {
        $scope.veri =
            {
                masaID: mid,
                personelID: pid,
                urunID: uid,
                adet: adet
            }
        $http.post("http://localhost:56525/api/Siparis/YeniSiparisEkle", $scope.veri)
            .then(function (response) {
                $scope.masaninsiparisi = response.data;
            });


    }
});
app.controller('girisCtrl', function ($scope, $http, $window) {
    $scope.girisyap = function (ka, sifre) {
        $http.get("http://localhost:56525/api/Personel/PersonelGiris?pka=" + ka + "&psifre=" + sifre)
            .then(function (response) {
                if (response.data != 0)
                {
                    if (response.data == 17)
                    {
                        $window.location.href = '/personeller.html';
                    }
                    else
                    {
                        $window.location.href = '/siparis.html';
                    }
                }
                    
                else
                {
                    
                    alert("Kullanıcı adı veya şifre yanlış");
                }
            });

    }
});
app.controller('kayitCtrl', function ($scope, $http, $window) {
    $http.get("http://localhost:56525/api/Personel/PersonelleriGetir")
        .then(function (response) {
            $scope.personeller = response.data;
        });
    $scope.personelekle = function (kayit) {
        $http.post("http://localhost:56525/api/Personel/YeniPersonel", kayit)
            .then(function (response) {
                $scope.personeller = response.data;
                $scope.kullanici = {};
                alert("Girişe yönlendiriliyorsunuz");
                $window.location.href = 'girisyap.html';
            });
    }
});
app.controller('personelCtrl', function ($scope, $http) {
    $http.get("http://localhost:56525/api/Personel/PersonelleriGetir")
        .then(function (response) {
            $scope.personeller = response.data;
        });
    $scope.personel = function (pid) {
        alert("Çıkış veriliyor");
        $http.get("http://localhost:56525/api/Personel/PersonelSil?personelID=" + pid)
            .then(function (response) {
                $scope.personeller = response.data;
            });
    }

});