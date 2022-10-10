using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;

namespace ZhiliBiliUBabusi3VeselihGuysa
{
    public partial class Leaderboard : Form
    {
        string _name;
        ListViewItemCollection lvic;
        private ListViewColumnSorter lvwColumnSorter;
        private ListView lv;
        public Leaderboard(string Name)
        {
            lvwColumnSorter = new ListViewColumnSorter();
            _name = Name;
            CheckBox check = new CheckBox();
            ComboBox cb = new ComboBox();
            check.Text = "Personal?";
            check.CheckedChanged += (object s, EventArgs e) => fillListView(cb.GetItemText(cb.SelectedItem), check.Checked);
            cb.DataSource = new List<string>() { "Math game", "Match game"};
            cb.DropDownStyle = ComboBoxStyle.DropDownList;
            cb.SelectedValueChanged += (object s, EventArgs e) => fillListView(cb.GetItemText(cb.SelectedItem), check.Checked);
            cb.SelectedItem = "Math game";
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
            tlp.Dock = DockStyle.Fill;

             lv = new ListView();
            lv.ListViewItemSorter = lvwColumnSorter;
            lv.ColumnClick += Lv_ColumnClick;
            lv.Dock = DockStyle.Fill;
            lv.View = View.Details;
            lv.Columns.Add("Name", 140, HorizontalAlignment.Left);
            lv.Columns.Add("Score", 130, HorizontalAlignment.Left);
            lvic = new ListViewItemCollection(lv);
            fillListView(cb.GetItemText(cb.SelectedItem), check.Checked);
            tlp.Controls.Add(check, 0, 0);
            tlp.Controls.Add(cb, 0, 0);
            tlp.Controls.Add(lv, 0, 1);
            tlp.SetRowSpan(lv, 2);
            tlp.SetColumnSpan(lv, 2);
            Controls.Add(tlp);
        }

        private void Lv_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            lv.Sort();
        }

        private void fillListView(string chosen, bool personal)
        {
            lvic.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {

                if (chosen == "Match game") {
                    var query = from game in db.Set<MatchScore>()
                                join user in db.Set<User>()
                                    on game.PlayerId equals user.Id
                                select new { user, game };
                    foreach (var item in query.ToList())
                    {
                        var n = new ListViewItem() { Text = item.user.Name };
                        n.SubItems.Add(item.game.Time.ToString());
                        if (personal && _name != item.user.Name)
                            continue;
                        lvic.Add(n);
                    }
                } 
                if (chosen == "Math game") {
                    var query = from game in db.Set<MathScore>()
                            join user in db.Set<User>()
                                on game.PlayerId equals user.Id
                            select new { user, game };
                    foreach (var item in query.ToList())
                    {
                        var n = new ListViewItem() { Text = item.user.Name };
                        n.SubItems.Add(item.game.Time.ToString());
                        if (personal && _name != item.user.Name)
                            continue;
                        lvic.Add(n);
                    }
                }   
            };
        }
    }
}
