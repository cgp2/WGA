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
            info = new CardInfo();

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
                info.Attack = int.Parse(attackBox.Text);
                info.Description = descriptionBox.Text;
                info.Health = int.Parse(hpBox.Text);
                info.Name = nameBox.Text;
                info.Shield = int.Parse(shieldBox.Text);
                info.ImagePath = ImagePathBox.Text;

                info.valueAura = int.Parse(ASettingsBox.Text);
                info.valueBatterCry = int.Parse(BCSettingsBox.Text);
                info.valueDeathRattle = int.Parse(DRSettingsBox.Text);

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
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Bitmap files (*.bmp)|*.bmp|Image files (*.jpg)|*.jpg";

            if (openFile.ShowDialog() == DialogResult.Cancel)
                return;
            ImagePathBox.Text = openFile.FileName;
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
