namespace Payroll.Domain
{
    static class Extensions
    {
        public static decimal Roundv2(this decimal dec)
        {
            return decimal.Round(dec, 2);
        }

        public static string ToCurrency(this decimal dec)
        {
            return dec.ToString("C2");
        }
    }
}
