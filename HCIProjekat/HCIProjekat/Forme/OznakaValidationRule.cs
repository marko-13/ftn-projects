using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HCIProjekat.Forme
{
    //VALIDATION RULE ZA OZNAKU
    public class OznakaValidationRule : ValidationRule
    {
        
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;

                if (s.Contains(" ")){
                    return new ValidationResult(false, "Greska: Razmaci nisu podrzani");
                }

                if (!Regex.Match((String)value, "^[^0-9]+$").Success)
                    {
                        
                        return new ValidationResult(false, "Greska: Oznaka ne sme sadrzati cifre");
                    }
                
                

                return new ValidationResult(true, null);

            }
            catch
            {
                return new ValidationResult(false, "Greska: Dogodila se nepoznata greska");
            }
        }
    }

    //VALIDATION RULE ZA OZNAKU
    public class OznakaValidationRulee : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;

                if (s.Contains(" "))
                {
                    return new ValidationResult(false, "ozn ne podrzava razmake");
                }
                foreach (char c in s)
                {
                    
                    if (Char.IsDigit(c))
                    {

                        return new ValidationResult(false, "ozn ne sme sadrzati cifre");
                    }
                }


                return new ValidationResult(true, null);

            }
            catch
            {
                return new ValidationResult(false, "Greska: Dogodila se nepoznata greska");
            }
        }
    }

    //VALIDATION RULE ZA IME
    public class ImeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                foreach (char c in s)
                {
                    if (Char.IsDigit(c))
                    {
                        return new ValidationResult(false, "Greska: Ime ne sme sadrzati cifre");
                    }
                }

                return new ValidationResult(true, null);

            }
            catch
            {
                return new ValidationResult(false, "Greska: Dogodila se nepoznata greska");
            }
        }
    }

    //VALIDATION RULE ZA PRIHOD
    public class PrihodValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
              
                var s = value as string;
                double n;
                if (!(double.TryParse(s, out n)))
                 
                {
                    
                    if (!string.IsNullOrEmpty(s))
                        {
                        return new ValidationResult(false, "Greska: Prihod mora biti broj");
                        }
                    

                }
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Greska: Dogodila se nepoznata greska");
            }
        }
    }

   
}
