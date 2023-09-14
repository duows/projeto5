
using ReaLTaiizor.Forms;
namespace projeto4
{
    public partial class Principal : MaterialForm
    {
        bool isOpenAluno = false;
        bool isOpenProfessor = false;
        bool isOpenCurso = false;
        bool isOpenRelatorioAluno = false;
        bool isOpenRelatorioProfessor = false;
        bool isOpenRelatorioCurso = false;

        public Principal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cadastroDeAlunoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isOpenAluno)
            {
                FormAluno formAluno = new FormAluno();
                formAluno.MdiParent = this;
                isOpenAluno= true;
                formAluno.Show();
            }
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                e.Cancel = true;
            }
        }

        private void cadastroDeProfessorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProfessor formProfessor = new FormProfessor();
            formProfessor.MdiParent = this;
            formProfessor.Show();
        }

        private void cadastroCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCurso formCurso = new FormCurso();
            formCurso.MdiParent = this;
            formCurso.Show();
        }

        
        private void relat�riosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void alunosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formRelAluno = new FormRelatorioAluno();
            formRelAluno.MdiParent = this;
            formRelAluno.Show();
        }
    }
}