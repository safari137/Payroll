namespace Payroll.UI.Tools
{
    public class NumberValidation : Validation
    {
        public override bool IsValid(string input)
        {
            var inputCharArray = input.ToCharArray();

            foreach (var c in inputCharArray)
            {
                if ((c < '0') || (c > '9'))
                    return false;
            }

            return true;
        }
    }
}
