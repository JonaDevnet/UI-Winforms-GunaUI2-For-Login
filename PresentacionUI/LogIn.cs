using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentacionUI
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
        }
        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnInicio_Click_1(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text;
            string clave = txtClave.Text;

            var result = Login(nombreUsuario, clave);
        }
        private async Task<string> Login(string nombreUsuario, string clave)
        {
            using (var client = new HttpClient())
            {
                // Crear un objeto anónimo para el usuario
                var usuario = new
                {
                    usuario = nombreUsuario,
                    pass = clave
                };

                // Serializar el objeto usuario a JSON
                var json = JsonConvert.SerializeObject(usuario);

                // Configurar la solicitud HTTP POST
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7178/login", content);

                if (response.IsSuccessStatusCode)
                {
                    // Retornar el mensaje de éxito recibido desde la API
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Retornar el error con el código de estado
                    return "Error: " + response.StatusCode.ToString();
                }
            }
        }

        
    }
}
