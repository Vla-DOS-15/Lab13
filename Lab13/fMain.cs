using System;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Lab13
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int buttonsSize = 9 * btnAdd.Width + 3 * tsSeparator1.Width + 30;
            btnExit.Margin = new Padding(Width - buttonsSize, 0, 0, 0);

            gvCar.AutoGenerateColumns = false;

            DataGridViewColumn column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Make",
                Name = "Make"
            };
            gvCar.Columns.Add(column);

            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Model",
                Name = "Model"
            };
            gvCar.Columns.Add(column);

            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Color",
                Name = "Color"
            };
            gvCar.Columns.Add(column);

            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "YearOfCreation",
                Name = "YearOfCreation"
            };
            gvCar.Columns.Add(column);

            column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                Name = "Price"
            };
            gvCar.Columns.Add(column);


            column = new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "KeyWorkingOrder",
                Name = "KeyWorkingOrder",
                Width = 60
            };
            gvCar.Columns.Add(column);

            EventArgs args = new EventArgs();
            OnResize(args);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Car processor = new Car();

            fCar ft = new fCar(processor);
            if (ft.ShowDialog() == DialogResult.OK)
            {
                bindSrcCar.Add(processor);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Car processor = (Car)bindSrcCar.List[bindSrcCar.Position];

            fCar fp = new fCar(processor);
            if (fp.ShowDialog() == DialogResult.OK)
            {
                bindSrcCar.List[bindSrcCar.Position] = processor;
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Видалити поточний запис?", 
                "Видалення запису", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning) == DialogResult.OK)
            {
                bindSrcCar.RemoveCurrent();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Очистити таблицю?\nВсі дані будуть втрачені",
                "Очищення даних", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                bindSrcCar.Clear();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Закрити застосунок?", 
                "Вихід з програми", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnSaveAsText_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти дані у текстовому форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            StreamWriter sw;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8);
                try
                {
                    foreach (Car processor in bindSrcCar.List)
                    {
                        sw.Write(processor.Make + "\t" + processor.Model + "\t" +
                        processor.Color + "\t" + processor.YearOfCreation+ "\t" + 
                        processor.Price + "\t" + processor.KeyWorkingOrder + "\t\n");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sw.Close();
                }
            }
        }

        private void btnSaveAsBinary_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "Файли даних (*.processors)|*.towns|All files (*.*)|*.*";
            saveFileDialog.Title = "Зберегти дані у бінарному форматі";
            saveFileDialog.InitialDirectory = Application.StartupPath;
            BinaryWriter bw;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bw = new BinaryWriter(saveFileDialog.OpenFile());
                try
                {
                    foreach (Car processor in bindSrcCar.List)
                    {
                        bw.Write(processor.Make);
                        bw.Write(processor.Model);
                        bw.Write(processor.Color);
                        bw.Write(processor.YearOfCreation);
                        bw.Write(processor.Price);
                        bw.Write(processor.KeyWorkingOrder);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message, 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    bw.Close();
                }
            }
        }

        private void btnOpenFromText_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Текстові файли (*.txt)|*.txt|All files (*.*) | *.* ";
            openFileDialog.Title = "Прочитати дані у текстовому форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;

            StreamReader sr;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcCar.Clear(); 
                sr = new StreamReader(openFileDialog.FileName, Encoding.UTF8);
                string s;
                try
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] split = s.Split('\t');
                        Car processor = new Car(split[0], split[1], split[2],
                        uint.Parse(split[3]), int.Parse(split[4]), bool.Parse(split[5]));
                        bindSrcCar.Add(processor);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message,
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    sr.Close();
                }
            }
        }

        private void btnOpenFromBinary_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Файли даних (*.towns)|*.towns|All files (*.*) | *.* ";
            openFileDialog.Title = "Прочитати дані у бінарному форматі";
            openFileDialog.InitialDirectory = Application.StartupPath;
            BinaryReader br;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                bindSrcCar.Clear();
                br = new BinaryReader(openFileDialog.OpenFile());
                try
                {
                    Car processor; 
                    while (br.BaseStream.Position < br.BaseStream.Length)
                    {
                        processor = new Car();
                        for (int i = 1; i <= 6; i++)
                        {
                            switch (i)
                            {
                                case 1: processor.Make = br.ReadString(); break;
                                case 2: processor.Model = br.ReadString(); break;
                                case 3: processor.Color= br.ReadString(); break;
                                case 4: processor.YearOfCreation= br.ReadUInt32(); break;
                                case 5: processor.Price = br.ReadInt32(); break;
                                case 6: processor.KeyWorkingOrder = br.ReadBoolean(); break;
                            }
                        }
                        bindSrcCar.Add(processor);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Сталась помилка: \n{0}", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    br.Close();
                }
            }
        }
    }
}
