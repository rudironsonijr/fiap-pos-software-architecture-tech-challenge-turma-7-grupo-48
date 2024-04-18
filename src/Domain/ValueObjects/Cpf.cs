namespace Domain.ValueObjects;
public struct Cpf
{
    public Cpf(string number)
    {
        if(IsValidCpf(number) is false)
        {

        }

        Number = number;
    }
    public string Number { get; } 

    public string FormatedNumber 
    { 
        get => Convert.ToUInt64(Number).ToString(@"000\.000\.000\-00"); 
    }

    public static implicit operator Cpf(string number) => new Cpf(number);

    public static implicit operator string(Cpf number) => number.FormatedNumber;

    public static bool IsValidCpf(string number)
    {
        int[] multiplier1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplier2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        number = number.Trim().Replace(".", "").Replace("-", "");
        if (number.Length != 11)
            return false;

        for (int j = 0; j < 10; j++)
            if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == number)
                return false;

        string hasCpf = number.Substring(0, 9);
        int sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(hasCpf[i].ToString()) * multiplier1[i];

        int remainder = sum % 11;
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;

        string digit = remainder.ToString();
        hasCpf = hasCpf + digit;
        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += int.Parse(hasCpf[i].ToString()) * multiplier2[i];

        remainder = sum % 11;
        if (remainder < 2)
            remainder = 0;
        else
            remainder = 11 - remainder;

        digit = digit + remainder.ToString();

        return number.EndsWith(digit);
    }

}
