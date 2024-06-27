namespace MatheusVSMP.Business.Core.Notificacoes
{
    public class Notificacao
    {
        public readonly string Message;

        protected Notificacao(string message)
        {
            Message = message;
        }

        public static Notificacao CreateNotificacao(string message) => new Notificacao(message);
    }
}
