using System.Text.Json;
using Microsoft.Maui.Controls;
using System.Linq;
using System.IO;

namespace SPJProyectoMAUI;

public partial class SPJVendedor : ContentPage
{
    private string _imagePath; // Para guardar la ruta de la imagen seleccionada
    private const string FileName = "datos_registrados.json"; // Archivo para guardar datos
    private List<Dictionary<string, string>> _registros; // Lista de registros cargados

    public SPJVendedor()
    {
        InitializeComponent();
        CargarDatosDesdeArchivo(); // Cargar datos al iniciar

        // Vincular validación al cambiar texto
        TelefonoEntry.TextChanged += OnTelefonoTextChanged;
        PrecioEntry.TextChanged += OnPrecioTextChanged;
        AñoEntry.TextChanged += OnAñoTextChanged; // Agregar validación para el año
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

        // Crear el objeto con los datos
        var vendedor = new Dictionary<string, string>
        {
            { "Nombre", nombre },
            { "Correo", correo },
            { "Telefono", telefono },
            { "Direccion", direccion },
            { "Modelo", modelo },
            { "Marca", marca },
            { "Año", año },
            { "Precio", precio },
            { "Imagen", _imagePath }
        };

        // Guardar los datos en el archivo
        _registros.Add(vendedor);
        GuardarDatosEnArchivo();

        await DisplayAlert("Éxito", "Vendedor y vehículo registrados exitosamente.", "OK");
        MostrarRegistrosEnPantalla();
        LimpiarCampos();
    }

    private void MostrarRegistrosEnPantalla()
    {
        DatosRegistradosStack.Children.Clear();

        foreach (var registro in _registros)
        {
            var stack = new StackLayout
            {
                Padding = 10,
                BackgroundColor = Colors.LightGray,
                Margin = new Thickness(0, 5)
            };

            var datosVendedor = new Label
            {
                Text = $"Nombre: {registro["Nombre"]}\nCorreo: {registro["Correo"]}\nTeléfono: {registro["Telefono"]}\nDirección: {registro["Direccion"]}",
                FontSize = 14
            };

            var datosVehiculo = new Label
            {
                Text = $"Modelo: {registro["Modelo"]}\nMarca: {registro["Marca"]}\nAño: {registro["Año"]}\nPrecio: ${registro["Precio"]}",
                FontSize = 14
            };

            stack.Children.Add(datosVendedor);
            stack.Children.Add(datosVehiculo);

            // Si hay una ruta de imagen válida, agregar la imagen
            if (!string.IsNullOrWhiteSpace(registro["Imagen"]))
            {
                var imagenVehiculo = new Image
                {
                    Source = ImageSource.FromFile(Path.Combine(FileSystem.AppDataDirectory, registro["Imagen"])),
                    HeightRequest = 200,
                    Aspect = Aspect.AspectFit
                };

                stack.Children.Add(imagenVehiculo);
            }

            // Botón para eliminar el registro individualmente
            var eliminarButton = new Button
            {
                Text = "Eliminar",
                BackgroundColor = Colors.Red,
                TextColor = Colors.White
            };
            eliminarButton.Clicked += (s, e) => EliminarRegistro(registro);

            stack.Children.Add(eliminarButton);
            DatosRegistradosStack.Children.Add(stack);
        }
    }

    private void EliminarRegistro(Dictionary<string, string> registro)
    {
        _registros.Remove(registro);
        GuardarDatosEnArchivo();
        MostrarRegistrosEnPantalla();
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
                string newFileName = Path.GetFileName(result.FullPath);
                string destinationPath = Path.Combine(FileSystem.AppDataDirectory, newFileName);

                // Copiar la imagen al directorio interno
                File.Copy(result.FullPath, destinationPath, true);

                _imagePath = newFileName; // Guardar la ruta relativa
                CarImage.Source = ImageSource.FromFile(destinationPath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar la imagen: {ex.Message}", "OK");
        }
    }

    private void GuardarDatosEnArchivo()
    {
        try
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);
            string json = JsonSerializer.Serialize(_registros, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar datos: {ex.Message}");
        }
    }

    private void CargarDatosDesdeArchivo()
    {
        try
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, FileName);
            if (File.Exists(filePath))
            {
                string contenido = File.ReadAllText(filePath);
                _registros = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(contenido) ?? new List<Dictionary<string, string>>();
                MostrarRegistrosEnPantalla();
            }
            else
            {
                _registros = new List<Dictionary<string, string>>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar datos: {ex.Message}");
            _registros = new List<Dictionary<string, string>>();
        }
    }

    private void OnTelefonoTextChanged(object sender, TextChangedEventArgs e)
    {
        TelefonoEntry.Text = FiltrarSoloNumeros(e.NewTextValue);
    }

    private void OnPrecioTextChanged(object sender, TextChangedEventArgs e)
    {
        PrecioEntry.Text = FiltrarSoloNumeros(e.NewTextValue);
    }

    private void OnAñoTextChanged(object sender, TextChangedEventArgs e)
    {
        AñoEntry.Text = FiltrarSoloNumeros(e.NewTextValue); // Validación para el año
    }

    private string FiltrarSoloNumeros(string input)
    {
        return new string(input.Where(char.IsDigit).ToArray());
    }

    private async void OnEliminarTodoClicked(object sender, EventArgs e)
    {
        bool confirmacion = await DisplayAlert("Confirmación", "¿Seguro que deseas eliminar todos los registros?", "Sí", "No");
        if (confirmacion)
        {
            _registros.Clear();
            GuardarDatosEnArchivo();
            MostrarRegistrosEnPantalla();
        }
    }
}
