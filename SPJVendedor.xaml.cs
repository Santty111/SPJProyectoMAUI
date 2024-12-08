using Microsoft.Maui.Controls;

namespace SPJProyectoMAUI;

public partial class SPJVendedor : ContentPage
{
    private string _imagePath; // Para guardar la ruta de la imagen
    private SPJListaRegistros listaRegistrosPage;

    public SPJVendedor()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Navegar a la pesta�a de la lista de registros
        listaRegistrosPage = Application.Current.MainPage.FindByName<SPJListaRegistros>("SPJListaRegistros");
    }

    private void AgregarRegistroALista(VendedorYVehiculo registro)
    {
        listaRegistrosPage?.AgregarRegistro(registro);
    }

    private async void OnRegistrarVendedorClicked(object sender, EventArgs e)
    {
        // Recopilar datos del vendedor
        string nombre = NombreEntry.Text;
        string correo = CorreoEntry.Text;
        string telefono = TelefonoEntry.Text;
        string direccion = DireccionEntry.Text;

        // Recopilar datos del veh�culo
        string modelo = ModeloEntry.Text;
        string marca = MarcaEntry.Text;
        string a�o = A�oEntry.Text;
        string precio = PrecioEntry.Text;

        // Validar datos
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) ||
            string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(modelo) ||
            string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(a�o) || string.IsNullOrWhiteSpace(precio))
        {
            await DisplayAlert("Error", "Por favor completa todos los campos requeridos.", "OK");
            return;
        }

        // Registrar datos (ejemplo: guardar en base de datos)
        bool registrado = RegistrarVendedorYVehiculo(nombre, correo, telefono, direccion, modelo, marca, a�o, precio, _imagePath);

        if (registrado)
        {
            await DisplayAlert("�xito", "Vendedor y veh�culo registrados exitosamente.", "OK");
            LimpiarCampos();
        }
        else
        {
            await DisplayAlert("Error", "No se pudo registrar. Intenta nuevamente.", "OK");
        }
    }

    private async void OnUploadImageClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Selecciona una imagen",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                _imagePath = result.FullPath;
                CarImage.Source = ImageSource.FromFile(_imagePath); // Mostrar la imagen seleccionada
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar la imagen: {ex.Message}", "OK");
        }
    }

    private bool RegistrarVendedorYVehiculo(string nombre, string correo, string telefono, string direccion,
                                            string modelo, string marca, string a�o, string precio, string imagePath)
    {
        try
        {
            // Aqu� podr�as guardar en la base de datos o llamar a un servicio
            Console.WriteLine($"Vendedor: {nombre}, {correo}, {telefono}, {direccion}");
            Console.WriteLine($"Veh�culo: {modelo}, {marca}, {a�o}, {precio}, Imagen: {imagePath}");
            return true; // Suponemos �xito
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    private void LimpiarCampos()
    {
        // Limpiar datos del vendedor
        NombreEntry.Text = string.Empty;
        CorreoEntry.Text = string.Empty;
        TelefonoEntry.Text = string.Empty;
        DireccionEntry.Text = string.Empty;

        // Limpiar datos del veh�culo
        ModeloEntry.Text = string.Empty;
        MarcaEntry.Text = string.Empty;
        A�oEntry.Text = string.Empty;
        PrecioEntry.Text = string.Empty;

        // Limpiar imagen
        CarImage.Source = null;
        _imagePath = null;
    }
}
