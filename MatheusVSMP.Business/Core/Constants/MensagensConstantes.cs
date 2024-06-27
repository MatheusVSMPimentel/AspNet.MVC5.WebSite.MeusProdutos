namespace MatheusVSMP.Business.Core.Constants
{
    public static class MensagensConstantes
    {

        public const string NotEmptyValidator = "O campo {PropertyName} precisa ser fornecido.";
        public const string LengthValidator = "O campo {PropertyName} precisa ter entre {MinLenght} a {MaxLenght} de letras.";
        public const string EqualLengthValidator = "O Campo {PropertyName} precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.";
        public const string InvalidPropertyValidator = "O {PropertyName} Fornecido é invalido.";
        public const string GreaterThenValidator = "O campo {PropertyName} deve ser maior que {ComparisonValue}.";
        public const string RequiredDataAnnotations = "O campo {0} é obrigatorio.";
        public const string StringLegthDataAnnotations = "O campo {0} precisa ter entre {2} a {1} de letras.";
        public const string RequiredStringLegthDataAnnotations = "O campo {0} precisa ter {1} de letras.";
    }
}
