using System;
using System.Windows.Forms;

namespace CartBuilder
{
    public partial class MainForm : Form
    {
        CardBuilder builder;

        public MainForm()
        {
            builder = new CardBuilder();
            InitializeComponent();

            foreach (var card in builder.CardArray)
                cardList.Items.Add(card);

            FormClosing += (sender1, e1) =>
            {
                try
                {
                    builder.Save(cardList.Items);
                }
                catch
                {
                }
            };
        }

        private void cardList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cardList.SelectedItems.Count != 1)
            {
                textCardInfo.Lines = new string[0];
                return;
            }

            CardInfo info = (CardInfo)cardList.SelectedItem;
            string[] text =
            {
                "НАЗВАНИЕ:",
                "   " + info.Name,
                "КЛАСС КАРТЫ:",
                "   " + info.GetClassString(),
                "ОПИСАНИЕ:",
                "   " + info.Description,
                "АТАКА:",
                "   " + info.Attack,
                "ЗДОРОВЬЕ:",
                "   " + info.Health,
                "ЩИТЫ:",
                "   " + info.Shield,
                "БОЕВОЙ КЛИЧ:",
                "   " + info.GetBattleCry(),
                "ПРЕДСМЕРТНЫЙ ХРИП:",
                "   " + info.GetDeathRattle(),
                "АУРА:",
                "   " + info.GetAura(),
                "ПУТЬ К ИЗОБРАЖЕНИЮ:",
                "   " + info.ImagePath,
                "ЗНАЧЕНИЕ БОЕВОГО КЛИЧА:",
                "   " + info.valueBatterCry,
                "ЗНАЧЕНИЕ ПРЕДСМЕРТНОГО ХРИПА:",
                "   " + info.valueDeathRattle,
                "ЗНАЧЕНИЕ АУРЫ:",
                "   " + info.valueAura
            };

            textCardInfo.Lines = text;
        }

        private void saveAllButton_Click(object sender, EventArgs e)
        {
            try
            {
                builder.Save(cardList.Items);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!" + ex.Message);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (cardList.SelectedItems.Count == 0)
                return;

            for (int i = 0; i < cardList.SelectedItems.Count; i++)
                cardList.Items.Remove(cardList.SelectedItems[i]);
            textCardInfo.Lines = new string[0];
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddForm form = new AddForm();

            form.FormClosing += (sender1, e1) =>
            {
                CardInfo cardInfo = form.info;
                if (cardInfo.Name == null || cardInfo.Name == "")
                    return;
                cardList.Items.Add(cardInfo);
            };

            form.Show();
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            if (cardList.SelectedItems.Count != 1)
                return;
                                                                                                        
            AddForm form = new AddForm((CardInfo)cardList.SelectedItem);

            form.FormClosing += (sender1, e1) =>
            {
                CardInfo cardInfo = form.info;
                if (cardInfo.Name == null || cardInfo.Name == "")
                    return;
                cardList.Items.Remove(cardList.SelectedItem);
                cardList.Items.Add(cardInfo);
            };

            form.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                builder.Save(cardList.Items);
            }
            catch
            {
            }
        }
    }
}
