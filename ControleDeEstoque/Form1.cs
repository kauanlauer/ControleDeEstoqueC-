using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ControleDeEstoque
{
    public partial class Form1 : Form
    {
        private List<Produto> produtos;

        public Form1()
        {
            InitializeComponent();
            produtos = new List<Produto>();
            AtualizarDataGridView();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (ValidarEntrada())
            {
                var novoProduto = new Produto
                {
                    Id = produtos.Count > 0 ? produtos.Max(p => p.Id) + 1 : 1,
                    Nome = textBox1.Text, // Usando textBox1 como txtNome
                    Quantidade = int.Parse(txtQuantidade.Text),
                    Preco = decimal.Parse(txtPreco.Text)
                };

                produtos.Add(novoProduto);
                LimparCampos();
                AtualizarDataGridView();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewProdutos.SelectedRows.Count > 0 && ValidarEntrada())
            {
                int id = Convert.ToInt32(dataGridViewProdutos.SelectedRows[0].Cells[0].Value);
                var produto = produtos.FirstOrDefault(p => p.Id == id);

                if (produto != null)
                {
                    produto.Nome = textBox1.Text; // Usando textBox1 como txtNome
                    produto.Quantidade = int.Parse(txtQuantidade.Text);
                    produto.Preco = decimal.Parse(txtPreco.Text);

                    LimparCampos();
                    AtualizarDataGridView();
                }
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (dataGridViewProdutos.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridViewProdutos.SelectedRows[0].Cells[0].Value);
                var produto = produtos.FirstOrDefault(p => p.Id == id);

                if (produto != null)
                {
                    produtos.Remove(produto);
                    LimparCampos();
                    AtualizarDataGridView();
                }
            }
        }

        private void dataGridViewProdutos_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewProdutos.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridViewProdutos.SelectedRows[0].Cells[0].Value);
                var produto = produtos.FirstOrDefault(p => p.Id == id);

                if (produto != null)
                {
                    textBox1.Text = produto.Nome; // Usando textBox1 como txtNome
                    txtQuantidade.Text = produto.Quantidade.ToString();
                    txtPreco.Text = produto.Preco.ToString("F2");
                }
            }
        }

        private void AtualizarDataGridView()
        {
            dataGridViewProdutos.DataSource = null; // Limpa a fonte de dados
            dataGridViewProdutos.DataSource = produtos; // Define a nova fonte de dados
        }

        private void LimparCampos()
        {
            textBox1.Clear(); // Usando textBox1 como txtNome
            txtQuantidade.Clear();
            txtPreco.Clear();
            dataGridViewProdutos.ClearSelection();
        }

        private bool ValidarEntrada()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                !int.TryParse(txtQuantidade.Text, out _) ||
                !decimal.TryParse(txtPreco.Text, out _))
            {
                MessageBox.Show("Por favor, preencha todos os campos corretamente.");
                return false;
            }
            return true;
        }
    }

    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}