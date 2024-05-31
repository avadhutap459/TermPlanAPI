namespace SudLife_ProtectShield.APILayer.API.Model
{
    public class SDEBaseRequest
    {
        public string? APIKey { get; set; }

        public List<SDEBaseKeyValuePair>? formInputs { get; set; }
        public List<FundInput>? funds { get; set; }

        public List<InputOptions>? inputOptions { get; set; }
        public List<InputPW>? inputPartialWithdrawal { get; set; }


        public List<Rider>? riders { get; set; }
    }

    public class InputOptions
    {
        public int optionLevel { get; set; }
        public int optionId { get; set; }
        public string? optionValue { get; set; }

    }

    public class InputPW
    {
        public int PWYear { get; set; }
        public long PWAmount4Per { get; set; }
        public long PWAmount8Per { get; set; }
    }

    public class FundInput
    {
        public int fundId { get; set; }
        public decimal fundPercent { get; set; }
    }
    public class Rider
    {
        public int RiderId { get; set; }

        public List<FormInput>? formInputs { get; set; }

        public List<InputOptions>? inputOptions { get; set; }

    }
    public class FormInput
    {
        public string? key { get; set; }
        public string? value { get; set; }
    }
}
