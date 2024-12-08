using Microsoft.Maui.Controls;

namespace SPJProyectoMAUI;

public partial class SPJVendedor : ContentPage
{
    private string _imagePath; // Para guardar la ruta de la imagen

    public SPJVendedor()
    {
        InitializeComponent();
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

        // Registrar datos
        bool registrado = RegistrarVendedorYVehiculo(nombre, correo, telefono, direccion, modelo, marca, año, precio, _imagePath);

        if (registrado)
        {
            await DisplayAlert("Éxito", "Vendedor y vehículo registrados exitosamente.", "OK");
            MostrarDatosEnPantalla(nombre, correo, telefono, direccion, modelo, marca, año, precio);
            LimpiarCampos();
        }
        else
        {
            await DisplayAlert("Error", "No se pudo registrar. Intenta nuevamente.", "OK");
        }
    }

    private bool RegistrarVendedorYVehiculo(string nombre, string correo, string telefono, string direccion,
        string modelo, string marca, string año, string precio, string imagePath)
    {
        try
        {
            Console.WriteLine($"Nombre: {nombre}");
            Console.WriteLine($"Correo: {correo}");
            Console.WriteLine($"Teléfono: {telefono}");
            Console.WriteLine($"Dirección: {direccion}");
            Console.WriteLine($"Modelo: {modelo}");
            Console.WriteLine($"Marca: {marca}");
            Console.WriteLine($"Año: {año}");
            Console.WriteLine($"Precio: {precio}");
            Console.WriteLine($"Ruta Imagen: {imagePath}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al registrar: {ex.Message}");
            return false;
        }
    }

    private void MostrarDatosEnPantalla(string nombre, string correo, string telefono, string direccion,
                                        string modelo, string marca, string año, string precio)
    {
        DatosRegistradosStack.Children.Clear();

        var datosVendedor = new Label
        {
            Text = $"Nombre: {nombre}\nCorreo: {correo}\nTeléfono: {telefono}\nDirección: {direccion}",
            FontSize = 14
        };

        var datosVehiculo = new Label
        {
            Text = $"Modelo: {modelo}\nMarca: {marca}\nAño: {año}\nPrecio: ${precio}",
            FontSize = 14
        };

        DatosRegistradosStack.Children.Add(datosVendedor);
        DatosRegistradosStack.Children.Add(datosVehiculo);

        // Si hay una ruta de imagen válida, agregar la imagen
        if (!string.IsNullOrWhiteSpace(_imagePath))
        {
            var imagenVehiculo = new Image
            {
                Source = ImageSource.FromFile(_imagePath),
                HeightRequest = 200, // Ajusta el tamaño según tus necesidades
                Aspect = Aspect.AspectFit
            };

            DatosRegistradosStack.Children.Add(imagenVehiculo);
        }
    }

    private void LimpiarCampos()
    {
        NombreEntry.Text = string.Empty;
        CorreoEntry.Text = string.Empty;
        TelefonoEntry.Text = string.Empty;
        DireccionEntry.Text = string.Empty;
        ModeloEntry.Text = string.Empty;
        MarcaEntry.Text = string.Empty;
        AñoEntry.Text = string.Empty;
        PrecioEntry.Text = string.Empty;
        CarImage.Source = null;
        _imagePath = null;
    }

    private async void OnUploadImageClicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync();
            if (result != null)
            {
                _imagePath = result.FullPath;
                CarImage.Source = ImageSource.FromFile(_imagePath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar la imagen: {ex.Message}", "OK");
        }
    }
}
