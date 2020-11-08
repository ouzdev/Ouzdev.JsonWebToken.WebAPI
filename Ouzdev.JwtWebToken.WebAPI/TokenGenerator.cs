using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Ouzdev.JwtWebToken.WebAPI
{
    public class TokenGenerator
    {
        public string CreateToken()
        {
            //Burada algoritma için bir anahtar (key) oluşturduk.
            //**Buradaki anahtar startup.cs içinde yapılan tanımlamadaki anahtar değer ile eşleşirse token çalışacaktır(ufak bir not)
            var bytes = Encoding.UTF8.GetBytes("ouzouzouz1");

            //Sonrasında bu oluşturduğumuz keyi Simetrik anahtar generate eden nesnenin yapıcı metoduna tanımladık.
            SymmetricSecurityKey key = new SymmetricSecurityKey(bytes);

            //Simetrik anahtar nesnesini SignInCredentials nesnesinin yapıcı metoduna argüma olarak verdik.
            //İkinci argüman olarak SecurityAlgorithms sınıfının HmacSha256 const değerini verdik. Jwt bu algoritmayı tavsiye eder.
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Issuer => Ureten (imzalayan)
            //Audience => Tuketen
            //notBefore => Hangi süreden önce geçerli değil ?. Şuan ki zamandan önce geçerli değil.
            //expires => Ne zamana kadar geçerli ? şuanki tarihten 2 dakika sonrasına kadar geçerli.
            //signinCredentials argümanına yukarıda oluştuduğum credentials nesnesini veriyorum.
            JwtSecurityToken token = new JwtSecurityToken(issuer: "http://localhost", audience: "http://localhost", notBefore: DateTime.Now, expires: DateTime.Now.AddMinutes(2), signingCredentials: credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            //Bu aşamada token oluşturulmuştur. Ve geriye JSON tipinde string bir değişken olarak return edilmiştir.
            return handler.WriteToken(token);
        }
    }
}
