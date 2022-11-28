namespace Chronos.Domain.Utils
{
    public static class GenerateCustomHtmlString
    {
        public static string CreateEnvioTokenHtml(string nome, string token)
        {
            return "<html lang='en'>" +
                  "<head>" +
                    "<meta charset='UTF-8'/>" +
                    "<meta http-equiv='X-UA-Compatible' content='IE=edge'/>" +
                    "<meta name='viewport' content='width=device-width, initial-scale=1.0'/>" +
                    "<style>*{font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Oxygen,Ubuntu,Cantarell,'Open Sans','Helvetica Neue',sans-serif}body{padding:25px}.header{width:100%;text-align:center;margin-bottom:30px}.header-info{line-height:36px}.text,.text-bolder-info,.text-info,.text-quest-info{color:#595959;line-height:28px;font-size:14px}.text-info{margin-top:20px;margin-bottom:20px}.text-bolder-info{font-weight:500;margin-top:30px;margin-bottom:30px}.text-quest-info{margin-top:40px}.footer{font-size:14px} </style>" +
                  "</head>" +
                  "<body>" +
                    "<div class='header'>" +
                      "<img src='https://cdn.dribbble.com/users/715283/screenshots/4699059/logo-anim00.gif' width='300'/>" +
                    "</div>" +
                    $"<h2 class='header-info'>Olá {nome},</h2>" +

                    "<p class='text-info'>" +
                      $"Aqui está o token para configuração da sua nova conta Chronos! <b>{token}</b> " +
                      "</p>" +                   

                    
                    "<div class='footer'>" +
                      "<p class='text'>E-mail automático, favor não responder.</p>" +                     
                      "<p class='text'>Atenciosamente,</p>" +
                      "<p class='text'>Equipe Chronos.</p>" +
                    "</div>" +
                  "</body>" +
                "</html>";
        }

        public static string CreateEnvioResetarSenhaHtml(string nome, string codigo, string token)
        {
            return "<html lang='en'>" +
                  "<head>" +
                    "<meta charset='UTF-8'/>" +
                    "<meta http-equiv='X-UA-Compatible' content='IE=edge'/>" +
                    "<meta name='viewport' content='width=device-width, initial-scale=1.0'/>" +
                    "<style>*{font-family:-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Oxygen,Ubuntu,Cantarell,'Open Sans','Helvetica Neue',sans-serif}body{padding:25px}.header{width:100%;text-align:center;margin-bottom:30px}.header-info{line-height:36px}.text,.text-bolder-info,.text-info,.text-quest-info{color:#595959;line-height:28px;font-size:14px}.text-info{margin-top:20px;margin-bottom:20px}.text-bolder-info{font-weight:500;margin-top:30px;margin-bottom:30px}.text-quest-info{margin-top:40px}.footer{font-size:14px}</style>" +
                  "</head>" +
                  "<body>" +
                    "<div class='header'>" +
                      "<img src='https://cdn.dribbble.com/users/715283/screenshots/4699059/logo-anim00.gif' width='300'/>" +
                    "</div>" +
                    $"<h2 class='header-info'>Olá {nome},</h2>" +

                    "<p class='text-info'>" +
                      $"Aqui está o código: <b>{codigo}</b> e o token:  <b>{token}</b> para resetar sua senha!" +
                      "</p>" +


                    "<div class='footer'>" +
                      "<p class='text'>E-mail automático, favor não responder.</p>" +
                      "<p class='text'>Atenciosamente,</p>" +
                      "<p class='text'>Equipe Chronos.</p>" +
                    "</div>" +
                  "</body>" +
                "</html>";
        }
    }
}
