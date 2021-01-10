using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GumpStudio.Plugins;

namespace GumpStudio.Forms
{
    public partial class PluginManager
    {
        public List<BasePlugin> AvailablePlugins { get; set; }

        public List<BasePlugin> LoadedPlugins { get; set; }

        public DesignerForm MainForm { get; set; }

        public PluginInfo[] OrderList { get; set; }

        public PluginManager()
        {
            InitializeComponent();
        }

        private void CmdAdd_Click(object sender, EventArgs e)
        {
            PluginInfo pluginInfo = (PluginInfo)lstAvailable.Items[lstAvailable.SelectedIndex];
            lstAvailable.Items.RemoveAt(lstAvailable.SelectedIndex);
            lstLoaded.Items.Add(pluginInfo);
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void CmdMoveDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstLoaded.SelectedIndex;
            if (selectedIndex >= lstLoaded.Items.Count - 2)
            {
                return;
            }

            object objectValue = RuntimeHelpers.GetObjectValue(lstLoaded.SelectedItem);
            lstLoaded.Items.RemoveAt(selectedIndex);
            lstLoaded.Items.Insert(selectedIndex + 1, RuntimeHelpers.GetObjectValue(objectValue));
            lstLoaded.SelectedIndex = selectedIndex + 1;
        }

        private void CmdMoveUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstLoaded.SelectedIndex;
            if (selectedIndex <= 0)
            {
                return;
            }

            object objectValue = RuntimeHelpers.GetObjectValue(lstLoaded.SelectedItem);
            lstLoaded.Items.RemoveAt(selectedIndex);
            lstLoaded.Items.Insert(selectedIndex - 1, RuntimeHelpers.GetObjectValue(objectValue));
            lstLoaded.SelectedIndex = selectedIndex - 1;
        }

        private void CmdOK_Click(object sender, EventArgs e)
        {
            MessageBox.Show("You will need to restart the program for plugin changes to take affect.");
            PluginInfo[] pluginInfoArray = null;

            foreach (object obj in lstLoaded.Items)
            {
                PluginInfo pluginInfo = (PluginInfo)RuntimeHelpers.GetObjectValue(obj);
                if (pluginInfoArray == null)
                {
                    pluginInfoArray = new PluginInfo[1];
                }
                else
                {
                    var newArray = new PluginInfo[pluginInfoArray.Length + 1];
                    Array.Copy(pluginInfoArray, newArray, pluginInfoArray.Length);
                    pluginInfoArray = newArray;
                }
                pluginInfoArray[pluginInfoArray.Length - 1] = pluginInfo;
            }

            MainForm.PluginTypesToLoad = pluginInfoArray;
            MainForm.WritePluginsToLoad();
            DialogResult = DialogResult.OK;
        }

        private void CmdRemove_Click(object sender, EventArgs e)
        {
            PluginInfo pluginInfo = (PluginInfo)lstLoaded.Items[lstLoaded.SelectedIndex];
            lstLoaded.Items.RemoveAt(lstLoaded.SelectedIndex);
            lstAvailable.Items.Add(pluginInfo);
        }

        private void PluginManager_Load(object sender, EventArgs e)
        {
            lstLoaded.Items.Clear();
            lstAvailable.Items.Clear();

            if (OrderList != null)
            {
                foreach (PluginInfo pluginInfo in OrderList)
                {
                    bool flag = false;

                    foreach (BasePlugin basePlugin in AvailablePlugins)
                    {
                        if (basePlugin.GetPluginInfo().Equals(pluginInfo))
                        {
                            flag = true;
                        }
                    }

                    if (flag)
                    {
                        lstLoaded.Items.Add(pluginInfo);
                    }
                }
            }

            foreach (BasePlugin basePlugin in AvailablePlugins)
            {
                if (!basePlugin.IsLoaded)
                {
                    lstAvailable.Items.Add(basePlugin.GetPluginInfo());
                }
            }
        }

        private void Plugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            if (listBox.SelectedIndex == -1)
            {
                return;
            }

            PluginInfo selectedItem = (PluginInfo)listBox.SelectedItem;
            txtAuthor.Text = selectedItem.AuthorName;
            txtEmail.Text = selectedItem.AuthorEmail;
            txtVersion.Text = selectedItem.Version;
            txtDescription.Text = selectedItem.Description;

            cmdMoveUp.Enabled = lstLoaded.SelectedIndex > 0;
            cmdMoveDown.Enabled = lstLoaded.SelectedIndex < listBox.Items.Count - 1;
            cmdAdd.Enabled = lstAvailable.SelectedIndex != -1;
            cmdRemove.Enabled = lstLoaded.SelectedIndex != -1;
        }
    }
}
