## Asp.NET Core Web API Json Web Token

 * Token bazlı yetkilendirme aracıdır. Geleneksel cookie bazlı araçların modern halidir.
 * Birçok projede kullanılmakla beraber standart olma yolunda ilerlemektedir.
 * RFC 7519 standartlarında geliştirilmiştir

**Bir JWT yapısı 3 kısımdan oluşmaktadır.**

 * HEADER 
 * PAYLOAD
 * VERIFY
 ## Header
 
Json Web Token (JWT) Header algoritma ve token tipinden oluşmaktadır. 
	    Birinci satır  "**alg**"  şifreleme algoritmasını içermektedir. HS256 RFC 7519 tarafından önerilen şifreleme algoritmasıdır. İsteğe bağlı olarak farklı şifreleme algoritmaları da kullanılabilir.
	    İkinci satır "**typ**" tokenın tipidir. Burada "JWT" Json Web Token kullanacağımızı belirttik.
			
    {
    "alg":"HS256",
    "typ":"JWT"
    }



## Payload

Bu kısımda şifrelenmiş olan  ilgili veriler taşınır.

 

     {
    
     "sub": "1234567890",
    
     "name": "John Doe",
    
     "iat": 1516239022
    
    }
## Verify Signature

	

Bu kısımda şifrelenmiş olan Tokenı çözmemiz için gereken anahtarı içermektedir.


  ![enter image description here](https://i.ibb.co/xFpPt5X/Ekran-g-r-nt-s-2020-11-08-101352.png)

## Json Web Token Nasıl Çalışır ?
Sunucu tarafından ilgili veriler doğru ise JWT tarafından Web Token oluşturulup client tarafına gönderilir. Client eğer, sunucunun gönderdiği Web Tokena tanımlanmış yetkiler vb. gereksinimleri karşılıyorsa JWT "Verify Signature" ile anahtarı alarak Client'a erişimi açar.

## Json Web Token Nasıl Oluşturulur ?
Bir JWT oluşturmak için `JwtSecurityToken Handler` sınıfından bir nesne türetilir. Bu nesneyle beraber `JwtNesne.WriteToken()` metodunu kullanarak, bu metodun argümanına `JwtSecurityToken` sınıfından bir nesne türetip verdikten sonra Tokenı başarılı bir şekilde oluşturabiliriz.
Token oluştururken hangi özelliklere sahip olacağını belirlemek için `JwtSecurityToken` sınıfını kullanıyoruz. 

**JWT Token Özellikleri**

 - Issuer : Bu tokenı kim tarafından oluşturulduğunun bilgisini tutar.
 - Audience : Bu tokenı kimin kullanacağının bilgisini tutar.
 - Expires : Bu token ne kadar süre geçerli olacağının bilgisini tutar.
 - notBefore : Hangi süreden önce geçerli olmayacağının bilgisini tutar.
 - Claims : Tokenda rol bilgilerinin tutulacağı property.
 - SingInCredentials : İlgili tokenın şifrelenmiş simetrik anahtar bilgisini tutar.

## JWT Asp .Net Core Web API  Projesini Oluşturma
Örnek bir proje oluştururken "Configure Https" özelliğini devre dışı bırakıyoruz. Sonrasında geleneksel Web Api projesini oluşturuyoruz.

JWT doğrulama imzası kısmında 256 bit bir anahtara ihtiyacımız olacağı için .net Core tarafında bu anahtarı bir nesne yardımıyla oluşturacağız. Bu nesne bu anahtarı simetrik olarak oluşturacak.

**Startup.cs dosyasında gerekli konfigürasyonların yapılması** 

ConfigureService içerisine JWT servisi eklemek için `services.AddAuthentication()` kodunu ekliyorum. Sonrasında bu giriş işlemini gerçekleştirebilmek için bir kaç işlem daha yapmamız gerekiyor. 

Nuget Package Console kullanarak ilgili `Microsoft.AspNetCore.Authentication.JwtBearer`  paketini projeme ekliyorum. Gerekli paketi projeme kurduktan sonra işlemlere devam ediyorum.

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
    });
Yukarıda ki kod bloğunda `service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)` içerisine bir authentication şeması belirtmemiz gerekiyor. 

Şema nedir kısaca özetleyecek olursak bir giriş sisteminiz olduğunu ve öğrenci ve öğretmen olarak 2 farklı giriş yöntemi kullandığınızı varsayalım. Bu tarz durumlarda birden fazla authentication şeması kullanabiliriz. Biz bu projede sadece bir tane authentication yöntemi kullanacağımız için bir tane şema kullanacağız.
 
 Sonrasında `AddJwtBearer()` içinde bir delege kullanarak özelleştirmeleri yapıyoruz.
 
**Peki bu özelleştireceğimiz özellikler nelelerdir ?**

 - 
