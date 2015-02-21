using GalaxyConquest.Tactics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GalaxyConquest
{
    public partial class StarShop : Form
    {
        public StarShop()
        {
            InitializeComponent();
        }

        string[] names = { "Ship Assaulter", "Ship Scout", "Colonysator Ship", "Fregat" };
        int[] costs = { 200, 100, 1000, 5000 };

        double totalPrice;
        int selectedType;       //0 - assaulter, 1 - scout, 2 - colonysator, 3 - fregat, 4,5,6 ...

        private void removeButton_Click(object sender, EventArgs e)
        {
            shipListBox.Items.Remove(shipListBox.SelectedItem);
            UpdateTotalPrice();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            shipListBox.Items.Add(names[selectedType]);

            UpdateTotalPrice();
        }

        private void buyButton_Click(object sender, EventArgs e)
        {
            if (shipListBox.Items.Count == 0) return;

            Fleet fl;
            int selectedFleet = SelectBox.Show(this, Form1.Game.Player.fleets.ToArray(), "Выберите флот");

            if (selectedFleet == -1) return;

            if (selectedFleet == Form1.Game.Player.fleets.Count)
            {
                int selectedStar = SelectBox.Show(this, Form1.Game.Player.stars.ToArray(), "Выберите звездную систему");
                fl = new Fleet(Form1.Game.Player, Form1.Game.Player.stars[selectedStar]);
                Form1.Game.Player.fleets.Add(fl);
            }
            else
                fl = Form1.Game.Player.fleets[selectedFleet];

            for (int i = 0; i < shipListBox.Items.Count; i++)
            {
                if (shipListBox.Items[i].Equals(names[0]))
                    fl.ships.Add(new ShipAssaulter(1, new WpnHeavyLaser()));
                else if (shipListBox.Items[i].Equals(names[1]))
                    fl.ships.Add(new ShipScout(1, new wpnLightLaser()));
                else if (shipListBox.Items[i].Equals(names[2]))
                    continue;   //Пока таких нету кораблей
                else if (shipListBox.Items[i].Equals(names[3]))
                    continue;   //Пока таких нету кораблей
            }
            Form1.Game.Player.credit -= totalPrice;

            Dispose();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (((PictureBox)sender) == assaultPicture)
                selectedType = 0;
            else if (((PictureBox)sender) == scoutPicture)
                selectedType = 1;
            else if (((PictureBox)sender) == colonysatorPicture)
                selectedType = 2;
            else if (((PictureBox)sender) == fregatPicture)
                selectedType = 3;

            switch (selectedType)
            {
                case 0:
                    {
                        descriptionText.Text = "Ship Assaulter.\nЦена: " + costs[selectedType] + " $\nТяжелый корабль, обладающий большим запасом прочности и хорошей огневой мощью, но при этом не так подвижен, как Scout.";
                        shipLargeImage.Image = assaultPicture.Image;
                    }
                    break;
                case 1:
                    {
                        descriptionText.Text = "Ship Scout.\nЦена: " + costs[selectedType] + " $\nЛёгкий корабль-разведчик. Имеет слабое вооружение и плохое бронирование, однако очень подвижен, что позволяет выжить на поле боя.";
                        shipLargeImage.Image = scoutPicture.Image;
                    }
                    break;
                case 2:
                    descriptionText.Text = "Colonysator Ship.\nЦена: " + costs[selectedType] + " $\nКорабль-колонист. Основная задача - перевозка людей. Отлично бронирован, имеет достаточный запас прочности, чтобы выжить в самых тяжелых условиях, однако не имеет вооружения.";
                    shipLargeImage.Image = colonysatorPicture.Image;
                    break;
                case 3:
                    descriptionText.Text = "Fregat.\nЦена: " + costs[selectedType] + " $\nКосмический фрегат имеет отличное вооружение как для ближнего, так и для дальнего боя. Это даёт ему возможность расправиться в одиночку с несколькими противниками, кроме этого имеет отличное бронирование. Недостатком является плохая подвижность.";
                    shipLargeImage.Image = fregatPicture.Image;
                    break;
                case 4:
                    break;
                case 5:
                    break;
                default:
                    return;
            }
        }

        void UpdateTotalPrice()
        {
            totalPrice = 0;
            for (int i = 0; i < shipListBox.Items.Count; i++)
            {
                if (shipListBox.Items[i].Equals(names[0]))
                    totalPrice += costs[0];
                else if (shipListBox.Items[i].Equals(names[1]))
                    totalPrice += costs[1];
                else if (shipListBox.Items[i].Equals(names[2]))
                    continue; //totalPrice += costs[2];
                else if (shipListBox.Items[i].Equals(names[3]))
                    continue; //totalPrice += costs[3];
            }
            priceLabel.Text = totalPrice + " $";
            if (Form1.Game.Player.credit < totalPrice)
            {
                priceLabel.ForeColor = Color.Red;
                buyButton.Enabled = false;
            }
            else
            {
                priceLabel.ForeColor = Color.Green;
                buyButton.Enabled = true;
            }
        }
    }

    /// <summary>
    /// Отображает окно со списком и возвращает индекс выбранного элемента 
    /// </summary>
    public static class SelectBox
    {
        /// <summary>
        /// Отображает перед другм окном окно со списком, состоящим из элементов типа SpaceObject и возвращает индекс выбранного объекта
        /// </summary>
        /// <param name="owner">Экземпляр класса Form, которому будет принадлежать данное диалоговое окно</param>
        /// <param name="objects">Массив объектов, которые будут представлены в окне</param>
        /// <param name="capture">Текст заголовка окна</param>
        public static int Show(Form owner, SpaceObject[] objects, string capture)
        {
            Form form = new Form();
            ListBox listBox1 = new ListBox();
            Button okButton = new Button();
            Button cancelButton = new Button();

            // listBox1
            listBox1.FormattingEnabled = true;
            listBox1.Location = new System.Drawing.Point(12, 12);
            listBox1.Size = new System.Drawing.Size(182, 199);
            // okButton
            okButton.Location = new System.Drawing.Point(22, 217);
            okButton.Size = new System.Drawing.Size(80, 23);
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            // cancelButton
            cancelButton.Location = new System.Drawing.Point(106, 216);
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "Cancel";
            cancelButton.DialogResult = DialogResult.Cancel;
            //Form
            form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            form.ClientSize = new System.Drawing.Size(205, 251);
            form.Controls.Add(cancelButton);
            form.Controls.Add(okButton);
            form.Controls.Add(listBox1);
            form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowInTaskbar = false;
            form.Text = capture;

            if (objects is Fleet[])
            {
                for (int i = 0; i < objects.Length; i++)
                    listBox1.Items.Add(objects[i].name + "\r\n(" + ((Fleet)objects[i]).s1.name + ")");
                listBox1.Items.Add("   <Сформировать новый флот>");
            }
            else
                for (int i = 0; i < objects.Length; i++)
                    listBox1.Items.Add(objects[i].name);

            if (form.ShowDialog(owner) == DialogResult.OK)
                return listBox1.SelectedIndex;
            else
                return -1;
        }
    }
}
