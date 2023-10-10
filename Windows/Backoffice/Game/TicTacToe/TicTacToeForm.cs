using Conexion.Canal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backoffice.Game.TicTacToe
{
    public partial class TicTacToeForm : Form
    {
        public Cliente ClienteCanal { get; set; }
        public string IdJuego { get; set; }
        public string IdJugador { get; set; }
        public string IdJugador1 { get; set; }
        public string IdJugador2 { get; set; }
        public string NombreJugado1 { get; set; }
        public string NombreJugado2 { get; set; }
        public string Turno { get; set; }

        public delegate void ListenerSelectedItem(string idJuego, string idJugador, int posicion);

        public ListenerSelectedItem OnSelectedItem;

        public TicTacToeForm()
        {
            IdJuego = Guid.NewGuid().ToString();
            InitializeComponent();
        }

        private void TicTacToeForm_Load(object sender, EventArgs e)
        {
            lblJugador1.Text = NombreJugado1;
            lblJugador2.Text = NombreJugado2;
        }

        public void InWrite(string jugador, int posicion)
        {
            switch (posicion)
            {
                case 1:
                    button1.Text = GetIndetificador(jugador);
                    break;
                case 2:
                    button2.Text = GetIndetificador(jugador);
                    break;
                case 3:
                    button3.Text = GetIndetificador(jugador);
                    break;
                case 4:
                    button4.Text = GetIndetificador(jugador);
                    break;
                case 5:
                    button5.Text = GetIndetificador(jugador);
                    break;
                case 6:
                    button6.Text = GetIndetificador(jugador);
                    break;
                case 7:
                    button7.Text = GetIndetificador(jugador);
                    break;
                case 8:
                    button8.Text = GetIndetificador(jugador);
                    break;
                case 9:
                    button9.Text = GetIndetificador(jugador);
                    break;
            }
            ValidaGanador();
            CambiarTurno();
        }

        private string GetIndetificador(string jugador)
        {
            if (jugador == IdJugador1)
                return "X";
            return "O";
        }

        public int GetPosicion(string nameButton) 
        {
            return  int.Parse(nameButton.Replace("button", string.Empty));
        }

        private void CambiarTurno()
        {
            if (Turno == IdJugador1)
                Turno = IdJugador2;
            else
                Turno = IdJugador1;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (IdJugador != Turno)
            {
                MessageBox.Show("Esperando que el otro jugador eliga", "Esperando turno", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                Button button = (Button)sender; 
                if (button.Text == string.Empty)
                {

                    InWrite(Turno, GetPosicion(button.Name));
                    if (OnSelectedItem != null)
                        OnSelectedItem(IdJuego, IdJugador, GetPosicion(button.Name));
                }
                else
                {
                    MessageBox.Show("Esperando que el otro jugador eliga", "Esperando turno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        private void ValidaGanador()
        {
            bool uno = ValidaGanador("XXX");
            if (uno)
            {
                MessageBox.Show("El jugador uno a ganado");
                Close();
            }
            
            bool dos = ValidaGanador("OOO");
            if (dos)
            {
                MessageBox.Show("El jugador dos a ganado");
                Close();
            }

            if (!ExistsEspacio())
            {
                MessageBox.Show("Se ha generado un empate");
                Close();
            }
        }

        private bool ValidaGanador(string comibacion)
        {
            // Horizontal
            if (button1.Text + button2.Text + button3.Text == comibacion)
                return true;
            if (button4.Text + button5.Text + button6.Text == comibacion)
                return true;
            if (button7.Text + button8.Text + button9.Text == comibacion)
                return true;
            // Vertical
            if (button1.Text + button4.Text + button7.Text == comibacion)
                return true;
            if (button2.Text + button5.Text + button8.Text == comibacion)
                return true;
            if (button3.Text + button6.Text + button9.Text == comibacion)
                return true;
            // Cruzado
            if (button1.Text + button5.Text + button9.Text == comibacion)
                return true;
            if (button7.Text + button5.Text + button3.Text == comibacion)
                return true;
            return false;
        }

        private bool ExistsEspacio()
        {
            return
            !(
                button1.Text == string.Empty &&
                button2.Text == string.Empty &&
                button3.Text == string.Empty &&
                button4.Text == string.Empty &&
                button5.Text == string.Empty &&
                button6.Text == string.Empty &&
                button7.Text == string.Empty &&
                button8.Text == string.Empty &&
                button9.Text == string.Empty
            );
        }

    }
}
