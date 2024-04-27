using Demo.DataAccess.Models;
using System.Net;
using System.Net.Mail;

namespace Demo.Presentaion.Helpers
{
	public class EmailSettings
	{
		public static void Send(Email email) {

			var send = new SmtpClient(host: "smtp.gmail.com",port: 587 );
			send.EnableSsl = true;
			send.Credentials = new NetworkCredential("omaryassin1522002@gmail.com", "bxgjmacdpckkbxyi");
			send.Send("omaryassin1522002@gmail.com", email.To, email.Subject, email.Body);
		
		
		}

	}
}
