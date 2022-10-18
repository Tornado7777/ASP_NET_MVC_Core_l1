using System;
using System.Collections.Generic;
using System.Text;

namespace ASP_NET_MVC_Core_l1_cons
{
    internal class Sample04Lesson3Builder
    {
        static void Main(string[] args)
        {
            MailMessage message = new MailMessageBuilder("b@b.com")
                .From("a@a.com")
                //.To("b@b.com")
                .Subject("Test message")
                .Body("lkhhkhkhkhj")
                .Build();
            MailMessageV2 message2 = new MailMessageV2()
                .From("aa@aa.com")
                .To("bb@bb.com")
                .Subject("Test message")
                .Body("lkh123hkhkhkhj");
        }
        
    }

    public class MailMessage
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    // В  паттерне Builder реализованном через fluent можно хранить и анализировать
    // состояние объекта и добовлять небходимую логику в методы
    public class MailMessageBuilder
    {
        private readonly MailMessage _mailMessage = new MailMessage();

        //для принуждения использования методов и заполнения полей можно использовать конструктор
        public MailMessageBuilder (string addressTo)
        {
            To(addressTo);
        }

        public MailMessage Build() 
        { 
            //доп.логика. Проверяем на наличие получателя почты
            if (string.IsNullOrEmpty(_mailMessage.To))
                throw new Exception("MailTo is empty.")
            //
            return _mailMessage; 
        }

        public MailMessageBuilder From(string address)
        {
            _mailMessage.From = address;
            return this;
        }

        public MailMessageBuilder To(string address)
        {
            _mailMessage.To = address;
            return this;
        }

        public MailMessageBuilder Subject(string subject)
        {
            _mailMessage.Subject = subject;
            return this;
        }
        public MailMessageBuilder Body(string body)
        {
            _mailMessage.Body = body;
            return this;
        }


    }

    // В  паттерне Builder реализованном через расширения нельзя оперерывать состоянием
    public class MailMessageV2
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
    public static class MailMessageBuilderExtensions
    {
        public static MailMessageV2 From(this MailMessageV2 mailMessage, string address)
        {
            mailMessage.From = address;
            return mailMessage;
        }

        public static MailMessageV2 To(this MailMessageV2 mailMessage, string address)
        {
            mailMessage.From = address;
            return mailMessage;
        }

        public static MailMessageV2 Subject(this MailMessageV2 mailMessage, string subject)
        {
            mailMessage.Subject = subject;
            return mailMessage;
        }

        public static MailMessageV2 Body(this MailMessageV2 mailMessage, string body)
        {
            mailMessage.Body = body;
            return mailMessage;
        }
    }
}
