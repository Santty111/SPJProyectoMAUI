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

        // Navegar a la pestaña de la lista de registros
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

        // Recopilar datos del vehículo
        string modelo = ModeloEntry.Text;
        string marca = MarcaEntry.Text;
        string año = AñoEntry.Text;
        string precio = PrecioEntry.Text;

        // Validar datos
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) ||
            string.IsNullOrWhiteSpace(telefono) || string.IsNullOrWhiteSpace(modelo) ||
            string.IsNullOrWhiteSpace(marca) || string.IsNullOrWhiteSpace(año) || string.IsNullOrWhiteSpace(precio))
        {
            await DisplayAlert("Error", "Por favor completa todos los campos requeridos.", "OK");
            return;
        }

        // Registrar datos (ejemplo: guardar en base de datos)
        bool registrado = RegistrarVendedorYVehiculo(nombre, correo, telefono, direccion, modelo, marca, año, precio, _imagePath);

        if (registrado)
        {
            await DisplayAlert("Éxito", "Vendedor y vehículo registrados exitosamente.", "OK");
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
                                            string modelo, string marca, string año, string precio, string imagePath)
    {
        try
        {
            // Aquí podrías guardar en la base de datos o llamar a un servicio
            Console.WriteLine($"Vendedor: {nombre}, {correo}, {telefono}, {direccion}");
            Console.WriteLine($"Vehículo: {modelo}, {marca}, {año}, {precio}, Imagen: {imagePath}");
            return true; // Suponemos éxito
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

        // Limpiar datos del vehículo
        ModeloEntry.Text = string.Empty;
        MarcaEntry.Text = string.Empty;
        AñoEntry.Text = string.Empty;
        PrecioEntry.Text = string.Empty;

        // Limpiar imagen
        CarImage.Source = null;
        _imagePath = null;
    }
}
