## Asp.Net Core Web API İle Json Web Token Kullanımı

 * Yoken bazlı yetkilendirme aracıdır. Geleneksel cookie bazlı araçların modern halidir.
 * Birçok projede kullanılmakla beraber standart olma yolunda ilerlemektedir.
 * RFC 7519 standartlarında geliştirilmiştir

**Bir JWT yapısı 3 kısımdan oluşmaktadır.**

 * Header
 * Payload
 * Verify Signature
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
 - SingInCredentials : İlgili tokenın imzasını taşır.

