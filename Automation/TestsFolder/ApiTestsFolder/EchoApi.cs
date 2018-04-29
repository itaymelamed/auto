using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Automation.TestsFolder.ApiTestsFolder
{
    [TestFixture]
    public class EchoApi
    {
        [Test]
        public void Test()
        {
            Console.WriteLine("");

            // Define const Key this should be private secret key  stored in some safe place
            string key = "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";

           // Create Security key  using private key above:
           // not that latest version of JWT using Microsoft namespace instead of System
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // Also note that securityKey length should be >256b
            // so you have to make sure that your private key has a proper length
            //
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            //  Finally create a Token
            var header = new JwtHeader(credentials);

            string json = "{ 'email': 'lior.s@minutemedia.com', 'username': 'lior', 'picture': 'https://s.gravatar.com/avatar/f2b523c65689b841a4dc8daf3cf3b55b?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fli.png', 'nickname': 'lior', 'name': 'lior.s@minutemedia.com', 'user_metadata': { 'name': 'Lior S', 'nick_name': 'Lior S' }, 'last_password_reset': '2018-01-18T13:11:30.420Z', 'app_metadata': {}, 'roles': [ 'echo:::admin', 'tags:::admin', 'editor:::admin', 'commercial:::admin', 'video:::admin' ], 'email_verified': true, 'user_id': 'auth0|59477f2ec879a57c9b5aa2a9', 'clientID': 'PKv66dyQ1jFMjoWz8EvCtyhfX2oRpHwN', 'identities': [ { 'user_id': '59477f2ec879a57c9b5aa2a9', 'provider': 'auth0', 'connection': 'Community', 'isSocial': false } ], 'updated_at': '2018-04-25T12:31:30.183Z', 'created_at': '2017-06-19T07:37:18.057Z', 'iss': 'https://minutemedia.auth0.com/', 'sub': 'auth0|59477f2ec879a57c9b5aa2a9', 'aud': 'PKv66dyQ1jFMjoWz8EvCtyhfX2oRpHwN', 'iat': 1524659492, 'exp': 1524695492, 'at_hash': '-tYA68Xsuip0oFOF1l9JGA' }".Replace(" ", "");
            JObject jObject = JObject.Parse(json);

            List<Claim> claims = new List<Claim>();
            jObject.Properties().ToList().ForEach(p => claims.Add(new Claim(p.Name, jObject[p.Name].ToString())));

            //Some PayLoad that contain information about the  customer
            var payload = new JwtPayload(claims);

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Token to String so you can use it in your client
            var tokenString = handler.WriteToken(secToken);

            Console.WriteLine(tokenString);
            Console.WriteLine("Consume Token");

            // And finally when  you received token from client
            // you can  either validate it or try to  read
            var token = handler.ReadJwtToken(tokenString);

            Console.WriteLine(token.Payload.First().Value);

            Console.ReadLine();
        }
    }
}