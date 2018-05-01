using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Automation.Helpersobjects
{
    public class AdsTxtValidator
    {
        string []_adsTxt;
        List<string> _domains;
        string _errors = string.Empty;

        public AdsTxtValidator(String url)
        {
            _adsTxt = GetTxtFile(url).Split(Environment.NewLine.ToCharArray());
            _domains = _adsTxt.ToList().Select(l => l.Split(',')[0]).ToList().Distinct().ToList();
            _domains.RemoveAll(d => d == string.Empty);
            _domains.RemoveAll(d => d.Contains("#COMMENT"));
        }

        public string Validate()
        {
            _errors += ValidateDomain();
            return _errors;
        }

        bool ValidateDomainFormat(string domain)
        {
            return domain.ToCharArray().All(c => char.IsLetter(c) || c == '.' || char.IsDigit(c)) && domain.ToCharArray().Where(c => c == '.').Count() == 1;
        }

        string ValidateDomain()
        {
            var domainErrors = string.Empty;
            _domains.ForEach(d => domainErrors += ValidateDomainFormat(d)? "" : $"<div><h3>format of {d} is incorrect</h3></div>");
            return domainErrors;
        }

        string GetTxtFile(string url)
        {
            var webRequest = WebRequest.Create(url);

            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var strContent = reader.ReadToEnd();
                return strContent;
            }
        }
    }
}