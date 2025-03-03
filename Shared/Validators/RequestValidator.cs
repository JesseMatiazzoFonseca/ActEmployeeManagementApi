namespace Shared.Validators
{
    public static class RequestValidator
    {
        public static bool CpfValidator(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;

            int[] multiplicadores1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
            int[] multiplicadores2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            if (!cpf.All(char.IsDigit))
                return false;

            if (cpf.Length != 11)
                return false;

            if (cpf.Distinct().Count() == 1)
                return false;

            for (int i = 0; i < 10; i++)
                if (cpf.Substring(0, 11) == new string(char.Parse(i.ToString()), 11))
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
