namespace MatheusVSMP.Business.Core.Constants
{
    public static class MensagensConstantes
    {

        public const string NotEmptyValidator = "O campo {PropertyName} precisa ser fornecido.";
        public const string LengthValidator = "O campo {PropertyName} precisa ter entre {MinLenght} até {MaxLenght} de letras.";
        public const string EqualLengthValidator = "O Campo {PropertyName} Precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.";
        public const string InvalidPropertyValidator = "O {PropertyName} Fornecido é invalido";
        public const string GreaterThenValidator = "O campo {PropertyName} deve ser maio que {ComparisonValue}.";
    }
}
