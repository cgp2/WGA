using System;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace CartBuilder
{
    public partial class AddForm : Form
    {
        const string SkillsFileName = "Skills.xml";
        public CardInfo info;

        public AddForm()
        {
            InitializeComponent();
            info = new CardInfo
            {
                CardID = Guid.NewGuid()
            };

            Skill[] SkillsArray;
            if (!File.Exists(SkillsFileName))
            {
                SkillsArray = new Skill[0];
                return;
            }

            using (FileStream fs = new FileStream(SkillsFileName, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Skill[]));
                SkillsArray = (Skill[])formatter.Deserialize(fs);
            }

            foreach (var skill in SkillsArray)
                SkillsCheckBox.Items.Add(skill);
        }

        public AddForm(CardInfo newInfo)
        {
            InitializeComponent();
            info = newInfo;

            if (info.CardID == Guid.Empty)
                info.CardID = Guid.NewGuid();

            attackBox.Text = info.Attack.ToString();
            descriptionBox.Text = info.Description;
            hpBox.Text = info.Health.ToString();
            nameBox.Text = info.Name;
            shieldBox.Text = info.Shield.ToString();
            ImagePathBox.Text = info.ImagePath;

            BCSettingsBox.Text = info.valueBatterCry.ToString();
            DRSettingsBox.Text = info.valueDeathRattle.ToString();
            ASettingsBox.Text = info.valueAura.ToString();

            switch (info.CardClass)
            {
                case CardInfo.Class.People:
                    ClassCardBox.Text = "People";
                    break;
                case CardInfo.Class.Insect:
                    ClassCardBox.Text = "Insect";
                    break;
                default:
                    ClassCardBox.Text = "Mechanism";
                    break;
            }

            Skill[] SkillsArray;
            if (!File.Exists(SkillsFileName))
            {
                SkillsArray = new Skill[0];
                return;
            }

            using (FileStream fs = new FileStream(SkillsFileName, FileMode.OpenOrCreate))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Skill[]));
                SkillsArray = (Skill[])formatter.Deserialize(fs);
            }

            foreach (var skill in SkillsArray)
                SkillsCheckBox.Items.Add(skill);

            for (int i = 0; i < SkillsCheckBox.Items.Count; i++)
            {
                foreach (var bc in info.BattleCryName)
                    if (((Skill)SkillsCheckBox.Items[i]).Name == bc)
                        SkillsCheckBox.SetItemChecked(i, true);

                foreach (var dr in info.DeathRattleName)
                    if (((Skill)SkillsCheckBox.Items[i]).Name == dr)
                        SkillsCheckBox.SetItemChecked(i, true);

                foreach (var au in info.AuraName)
                    if (((Skill)SkillsCheckBox.Items[i]).Name == au)
                        SkillsCheckBox.SetItemChecked(i, true);
            }
        }

        private void attackBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void hpBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void shieldBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (attackBox.Text == "" || hpBox.Text == "" || shieldBox.Text == "" || ClassCardBox.Text == "" ||
                    nameBox.Text == "" || descriptionBox.Text == "")
                MessageBox.Show("Введите все поля плз, оч надо :)");
            else
            {
                string path = "";

                try
                {
                    path = Path.Combine(
                        Path.GetFileNameWithoutExtension(Path.GetDirectoryName(ImagePathBox.Text)),
                        Path.GetFileNameWithoutExtension(ImagePathBox.Text));
                }
                catch { }

                info = new CardInfo
                {
                    Attack = int.Parse(attackBox.Text),
                    Description = descriptionBox.Text,
                    Health = int.Parse(hpBox.Text),
                    Name = nameBox.Text,
                    Shield = int.Parse(shieldBox.Text),
                    ImagePath = path,
                    valueAura = int.Parse(ASettingsBox.Text),
                    valueBatterCry = int.Parse(BCSettingsBox.Text),
                    valueDeathRattle = int.Parse(DRSettingsBox.Text),
                    CardID = info.CardID
                };

                switch (ClassCardBox.Text)
                {
                    case "People":
                        info.CardClass = CardInfo.Class.People;
                        break;
                    case "Insect":
                        info.CardClass = CardInfo.Class.Insect;
                        break;
                    default:
                        info.CardClass = CardInfo.Class.Mechanism;
                        break;
                }

                foreach (var skill in SkillsCheckBox.CheckedItems)
                {
                    if (((Skill)skill).Type == "DeathRattle")
                        info.AddDeathRattle(((Skill)skill).Name);

                    if (((Skill)skill).Type == "BattleCry")
                        info.AddBattleCry(((Skill)skill).Name);

                    if (((Skill)skill).Type == "Aura")
                        info.AddAura(((Skill)skill).Name);
                }

                Close();
            }
        }

        private void ImagePathButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "Bitmap files (*.bmp)|*.bmp|Image files (*.jpg)|*.jpg|PNG files (*.png)|*.png"
            };

            if (openFile.ShowDialog() == DialogResult.Cancel)
                return;

            try
            {
                ImagePathBox.Text = Path.Combine(
                        Path.GetFileNameWithoutExtension(Path.GetDirectoryName(openFile.FileName)),
                        Path.GetFileNameWithoutExtension(openFile.FileName));
            }
            catch
            {
                ImagePathBox.Text = "";
            }
        }

        private void BCSettingsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void DRSettingsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }

        private void ASettingsBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
            {
                e.Handled = true;
            }
        }
    }
}
