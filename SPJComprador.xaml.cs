using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace SPJProyectoMAUI
{
    public partial class SPJComprador : ContentPage
    {
        private List<Dictionary<string, string>> _catalogo;

        public SPJComprador()
        {
            InitializeComponent();
            CargarCatalogoPredeterminado();
            MostrarCatalogoEnPantalla();
        }

        private void CargarCatalogoPredeterminado()
        {
            _catalogo = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    { "Modelo", "Model S" },
                    { "Marca", "Tesla" },
                    { "Año", "2020" },
                    { "Precio", "80000" },
                    { "Imagen", "tesla_model_s.jpeg" },
                    { "Nombre", "John Doe" },
                    { "Correo", "johndoe@example.com" },
                    { "Telefono", "123456789" },
                    { "Direccion", "123 Tesla St, Electric City" }
                },
                new Dictionary<string, string>
                {
                    { "Modelo", "Mustang" },
                    { "Marca", "Ford" },
                    { "Año", "2019" },
                    { "Precio", "50000" },
                    { "Imagen", "ford_mustang.jpeg" },
                    { "Nombre", "Jane Smith" },
                    { "Correo", "janesmith@example.com" },
                    { "Telefono", "987654321" },
                    { "Direccion", "456 Mustang Ave, Car Town" }
                },
                new Dictionary<string, string>
                {
                    { "Modelo", "Civic" },
                    { "Marca", "Honda" },
                    { "Año", "2018" },
                    { "Precio", "20000" },
                    { "Imagen", "honda_civic.jpeg" },
                    { "Nombre", "Carlos Ruiz" },
                    { "Correo", "carlosruiz@example.com" },
                    { "Telefono", "112233445" },
                    { "Direccion", "789 Civic Rd, Honda City" }
                }
            };
        }

        private void MostrarCatalogoEnPantalla()
        {
            foreach (var vehiculo in _catalogo)
            {
                var stack = new StackLayout
                {
                    Padding = 10,
                    BackgroundColor = Colors.LightGray,
                    Margin = new Thickness(0, 5)
                };

                // Datos del vehículo
                var datosVehiculo = new StackLayout
                {
                    Padding = 5,
                    BackgroundColor = Colors.LightGreen
                };
                datosVehiculo.Children.Add(new Label { Text = "Datos del Vehículo", FontSize = 16, FontAttributes = FontAttributes.Bold });
                datosVehiculo.Children.Add(new Label { Text = $"Modelo: {vehiculo["Modelo"]}", FontSize = 14 });
                datosVehiculo.Children.Add(new Label { Text = $"Marca: {vehiculo["Marca"]}", FontSize = 14 });
                datosVehiculo.Children.Add(new Label { Text = $"Año: {vehiculo["Año"]}", FontSize = 14 });
                datosVehiculo.Children.Add(new Label { Text = $"Precio: ${vehiculo["Precio"]}", FontSize = 14 });

                // Título para los datos del propietario
                var datosPropietario = new StackLayout
                {
                    Padding = 5,
                    BackgroundColor = Colors.LightBlue
                };
                datosPropietario.Children.Add(new Label { Text = "Datos del Propietario", FontSize = 16, FontAttributes = FontAttributes.Bold });
                datosPropietario.Children.Add(new Label { Text = $"Nombre: {vehiculo["Nombre"]}", FontSize = 14 });
                datosPropietario.Children.Add(new Label { Text = $"Correo: {vehiculo["Correo"]}", FontSize = 14 });
                datosPropietario.Children.Add(new Label { Text = $"Teléfono: {vehiculo["Telefono"]}", FontSize = 14 });
                datosPropietario.Children.Add(new Label { Text = $"Dirección: {vehiculo["Direccion"]}", FontSize = 14 });

                // Sección para hacer una oferta de precio
                var ofertaStack = new StackLayout
                {
                    Padding = 5,
                    BackgroundColor = Colors.LightCoral
                };
                ofertaStack.Children.Add(new Label { Text = "Haz una oferta", FontSize = 16, FontAttributes = FontAttributes.Bold });

                var ofertaEntry = new Entry
                {
                    Placeholder = "Introduce tu oferta de precio",
                    Keyboard = Keyboard.Numeric
                };
                ofertaStack.Children.Add(ofertaEntry);

                // Botón para enviar la oferta
                var enviarOfertaButton = new Button
                {
                    Text = "Enviar Oferta",
                    BackgroundColor = Colors.Green,
                    TextColor = Colors.White
                };
                enviarOfertaButton.Clicked += (sender, e) =>
                {
                    var ofertaPrecio = ofertaEntry.Text;

                    // Validación de la oferta introducida
                    if (!string.IsNullOrWhiteSpace(ofertaPrecio) && decimal.TryParse(ofertaPrecio, out decimal oferta))
                    {
                        // Mostrar mensaje de éxito
                        DisplayAlert("Oferta Enviada", $"Tu oferta de ${ofertaPrecio} ha sido enviada con éxito.", "OK");
                    }
                    else
                    {
                        // Si no es válido, mostrar mensaje de error
                        DisplayAlert("Error", "Por favor, introduce una oferta válida.", "OK");
                    }
                };

                ofertaStack.Children.Add(enviarOfertaButton);

                // Sección de imagen
                if (!string.IsNullOrWhiteSpace(vehiculo["Imagen"]))
                {
                    var imagenVehiculo = new Image
                    {
                        Source = ImageSource.FromFile(vehiculo["Imagen"]), // Nombre del archivo de imagen
                        HeightRequest = 200,
                        Aspect = Aspect.AspectFit
                    };

                    var imagenStack = new StackLayout
                    {
                        Padding = 5,
                        BackgroundColor = Colors.LightYellow
                    };
                    imagenStack.Children.Add(new Label { Text = "Imagen", FontSize = 16, FontAttributes = FontAttributes.Bold });
                    imagenStack.Children.Add(imagenVehiculo);
                    stack.Children.Add(imagenStack);
                }

                // Añadir todos los elementos a la vista
                stack.Children.Add(datosVehiculo);
                stack.Children.Add(datosPropietario);
                stack.Children.Add(ofertaStack); // Sección de oferta
                CatalogoStack.Children.Add(stack);
            }
        }
    }
}
