namespace SPJProyectoMAUI;

public partial class SPJVendedor : ContentPage
{
    public SPJVendedor()
    {
        InitializeComponent();
    }

    private async void OnRegistrarVendedorClicked(object sender, EventArgs e)
    {
        // Recopilar datos de las entradas
        string nombre = NombreEntry.Text;
        string correo = CorreoEntry.Text;
        string telefono = TelefonoEntry.Text;
        string direccion = DireccionEntry.Text;

        // Validar datos
        if (string.IsNullOrWhiteSpace(nombre) ||
            string.IsNullOrWhiteSpace(correo) ||
            string.IsNullOrWhiteSpace(telefono))
        {
            await DisplayAlert("Error", "Por favor completa todos los campos requeridos.", "OK");
            return;
        }

        // Registrar el vendedor (ejemplo: guardar en base de datos o enviar a API)
        bool registrado = RegistrarVendedor(nombre, correo, telefono, direccion);

        if (registrado)
        {
            await DisplayAlert("Éxito", "Vendedor registrado exitosamente.", "OK");
            // Opcional: Limpiar los campos
            LimpiarCampos();
        }
        else
        {
            await DisplayAlert("Error", "No se pudo registrar el vendedor. Intenta nuevamente.", "OK");
        }
    }

    private bool RegistrarVendedor(string nombre, string correo, string telefono, string direccion)
    {
        try
        {
            // Aquí podrías agregar la lógica para guardar en una base de datos o llamar a un servicio
            Console.WriteLine($"Registrando: {nombre}, {correo}, {telefono}, {direccion}");
            return true; // Suponemos éxito en esta implementación básica
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    private void LimpiarCampos()
    {
        NombreEntry.Text = string.Empty;
        CorreoEntry.Text = string.Empty;
        TelefonoEntry.Text = string.Empty;
        DireccionEntry.Text = string.Empty;
    }
}
