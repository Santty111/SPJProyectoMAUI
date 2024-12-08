using System.Collections.ObjectModel;

namespace SPJProyectoMAUI;

public partial class SPJListaRegistros : ContentPage
{
    public ObservableCollection<VendedorYVehiculo> Registros { get; set; }

    public SPJListaRegistros()
    {
        InitializeComponent();

        // Crear una lista de datos (en memoria)
        Registros = new ObservableCollection<VendedorYVehiculo>();

        BindingContext = this;
    }

    public void AgregarRegistro(VendedorYVehiculo registro)
    {
        Registros.Add(registro);
    }
}

// Modelo para los datos
public class VendedorYVehiculo
{
    public string Nombre { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }
    public string Direccion { get; set; }
    public string Modelo { get; set; }
    public string Marca { get; set; }
    public string Año { get; set; }
    public string Precio { get; set; }
}
