namespace Suma5834163
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        private int _editResultadoId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listview.ItemsSource = await _dbService.GetResultados());
        }

        

        private async void SumarBtn_Clicked(object sender, EventArgs e)
        {
            if (_editResultadoId == 0)
            {
                int totSuma, n1, n2;
                n1 = Convert.ToInt32(Entryprimernumero.Text);
                n2 = Convert.ToInt32(Entrysegundonumero.Text);

                totSuma = n1 + n2;
                labelresultado.Text = totSuma.ToString();

                //agrega cliente
                await _dbService.Create(new Resultado
                {
                    Numero1 = Entryprimernumero.Text,
                    Numero2 = Entrysegundonumero.Text,
                    
                    Suma = labelresultado.Text
                }); ;
            }
            else
            {
                int totSuma, n1, n2;
                n1 = Convert.ToInt32(Entryprimernumero.Text);
                n2 = Convert.ToInt32(Entrysegundonumero.Text);

                totSuma = n1 + n2;
                labelresultado.Text = totSuma.ToString();

                //edita cliente
                await _dbService.Update(new Resultado
                {
                    Id= _editResultadoId,
                    Numero1=Entryprimernumero.Text,
                    Numero2=Entrysegundonumero.Text,
                    Suma=labelresultado.Text
                });
                _editResultadoId = 0;
            }
            Entryprimernumero.Text = string.Empty;
            Entrysegundonumero.Text = string.Empty;
            labelresultado.Text = string.Empty;

            listview.ItemsSource = await _dbService.GetResultados();
        }

        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var resultado = (Resultado)e.Item;
            var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

            switch (action)
            {
                case "Edit":
                    _editResultadoId = resultado.Id;
                    Entryprimernumero.Text = resultado.Numero1;
                    Entrysegundonumero.Text = resultado.Numero2;
                    labelresultado.Text = resultado.Suma;
                    break;

                case "Delete":
                    await _dbService.Delete(resultado);
                    listview.ItemsSource = await _dbService.GetResultados();
                    break;
            }
        }
    }

}
