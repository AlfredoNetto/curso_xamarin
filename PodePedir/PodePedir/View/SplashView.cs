using System;
using Xamarin.Forms;

namespace PodePedir.View
{
    public class SplashView : ContentPage
    {
        //Definição da imagem que irá receber a Logo do App no escopo geral da classe.
        Image splashImage;

        public SplashView()
        {
            //Remove a barra de navegação padrão da View.
            NavigationPage.SetHasNavigationBar(this, true);

            //Define um Layout absoluto para organizarmos a estrutura da tela de Splash.
            var sub = new AbsoluteLayout();

            //Aqui instânciamos a imagem declarada no escopo globlal da classe
            splashImage = new Image
            {
                Source = "logoapp.png", //Definimos aqui a imagem utilizada (No Android está na pasta Drawable em Resources, e no iOS em Resource)
                WidthRequest = 100, //Definimos a largura da imagem.
                HeightRequest = 100 //Definimos a altura da imagem.
            };

            //Aqui passamos as caracteristicas da imagem para atribui-las e serem interpretadas pelo Bound a baixo.
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            
            //Quando renderizado o elemento em tela, o Bound mantém uma ligação com o Source do objeto x atribuição.
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            //Adicionas a imagem no Layout absoluto.
            sub.Children.Add(splashImage);

            //Definimos o Background desta página como branco.
            this.BackgroundColor = Color.FromHex("#fff");

            //E por fim, incluimos o layout dentro da própria página (area de content = aréa de conteudo = visivel da pagina)
            this.Content = sub;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            await splashImage.ScaleTo(1, 2000); //Comeca a image om a escala padrão           
            await splashImage.ScaleTo(0.9, 2000, Easing.Linear); //Leva a imagem 0.1 para tras diminuindo na escala.
            await splashImage.FadeTo(0.4, 1500); //Inicia o efeito de FadeOut apagando a imagem gradativamente.
            await splashImage.ScaleTo(100, 1200, Easing.Linear); //Aumenta a imagem em sua escala dando um grande zoom.

            //Redireciona para a página principal da aplicação.
            Application.Current.MainPage = new NavigationPage(new MainView());
        }
    }
}
