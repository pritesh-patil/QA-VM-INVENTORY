
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using VMware.Vim;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.Net;
using NetConnection;
using Approva.QA.Common;
using System.ComponentModel;
using System.Threading;
using System.Xml;
using System.Diagnostics;
using System.ServiceProcess;

namespace vCenter_Inventory
{
    public partial class Inventory : Form
    {
        List<EntityViewBase> poweredOnvmlist = new List<EntityViewBase>();
        List<EntityViewBase> poweredOffvmlist = new List<EntityViewBase>();
        List<String> PowerOnvmNameList = new List<string>();
        List<EntityViewBase> hostlist = new List<EntityViewBase>();
        List<EntityViewBase> clusterlist = new List<EntityViewBase>();
        Dictionary<String, VirtualMachineSnapshotTree[]> vmSnaps = new Dictionary<String, VirtualMachineSnapshotTree[]>();
        Dictionary<String, String> installInformation = new Dictionary<String, String>();
        String searchVer = null;
        System.Threading.Tasks.Task[] taskArray = null;
        XmlDocument _appConfig = null;
        //Create a new VIM client object that will be used to connect to vCenter's SDK service
        VimClient Client = new VimClient();
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        static AutoResetEvent autoReset = new AutoResetEvent(false);
        String _genericUID = null;
        String _genricPWD = null;
        String _IRCVersion = null;
        String _CMVersion = null;
        String[] _productsInstalled = null;



        public Inventory()
        {
            InitializeComponent();

            InitializeBackgroundWorker();

        }
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            clearConnections();

            progressBar1.Visible = false;
            btnConnect.Enabled = false;
            lbHost.Items.Clear();

            String _hostIP = null;
            String _userID = null;
            String _password = null;

            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Config.xml"))
            {
                _appConfig = new XmlDocument();
                try
                {
                    _appConfig.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Config.xml");
                }
                catch (Exception xm)
                {
                    MessageBox.Show(xm.Message.ToString());
                }

                _hostIP = _appConfig.SelectSingleNode("settings/vmhost").InnerText;
                _userID = _appConfig.SelectSingleNode("settings/vmhostuserid").InnerText;
                _password = _appConfig.SelectSingleNode("settings/vmhostpassword").InnerText;
                _genericUID = _appConfig.SelectSingleNode("settings/vmcommonuserid").InnerText;
                _genricPWD = _appConfig.SelectSingleNode("settings/vmcommonpassword").InnerText;

                // Connect to the VMware vCenter SDK service  
                // Client.Connect("https://" + tbVCS.Text + "/sdk");
                Client.Connect(@"https://" + _hostIP + "/sdk");
                Client.Login(_userID, _password);

                // Get a list of Windows VM's
                NameValueCollection Onfilter = new NameValueCollection();
                NameValueCollection OFFfilter = new NameValueCollection();
                Onfilter.Add("Runtime.PowerState", "PoweredOn");
                //   filter.Add("Config.GuestFullName", "Windows");
                poweredOnvmlist = Client.FindEntityViews(typeof(VirtualMachine), null, Onfilter, null);
                OFFfilter.Add("Runtime.PowerState", "PoweredOff");
                poweredOffvmlist = Client.FindEntityViews(typeof(VirtualMachine), null, OFFfilter, null);
                listVM.Sorted = true;
                listBoxPowerOff.Sorted = true;

                foreach (VirtualMachine vm in poweredOnvmlist)
                {
                    listVM.Items.Add(vm.Name);
                    PowerOnvmNameList.Add(vm.Name);
                    if (vm.Snapshot != null)
                        vmSnaps.Add(vm.Name, vm.Snapshot.RootSnapshotList);

                }
                foreach (VirtualMachine vm in poweredOffvmlist)
                {
                    listBoxPowerOff.Items.Add(vm.Name);
                    if (vm.Snapshot != null)
                        vmSnaps.Add(vm.Name, vm.Snapshot.RootSnapshotList);
                }

                btnConnect.Enabled = true;

                //Get a list of ESXi hosts
                // hostlist = Client.FindEntityViews(typeof(HostSystem), null, null, null);
                //Populate the Host names into the Host ListBox
                // foreach (HostSystem vmhost in hostlist)
                //{
                //   lbHost.Items.Add(vmhost.Name);
                //}
            }
            else
            {
                MessageBox.Show("Config XML is not present");
            }
        }

        private void listVM_SelectedIndexChanged(object sender, EventArgs e)
        {
            progressBar1.Hide();
            label2.Size = new System.Drawing.Size(110, 13);
            label2.Text = "VM Name Description";
            labelInstalledProducts.Location = new System.Drawing.Point(245, 110);
            labelInstalledProducts.Size = new System.Drawing.Size(91, 13);
            lbHost.Height = 43;
            labelInstalledProducts.Text = "Installed Prodcuts";
            listBoxProductsInstalled.Location = new System.Drawing.Point(248, 126);
            listBoxProductsInstalled.Size = new System.Drawing.Size(446, 264);
            lbHost.Items.Clear();
            listBoxProductsInstalled.Items.Clear();
            listBoxProductsInstalled.ForeColor = System.Drawing.SystemColors.WindowText;
            buttonSearch.Enabled = true;
            _IRCVersion = null;
            _CMVersion = null;
            _productsInstalled = null;

            String vmNameSelectd = null;
            try
            {
                vmNameSelectd = listVM.SelectedItem.ToString();
            }
            catch (Exception)
            { }

            listBoxPowerOff.ClearSelected();
            buttonPowerOn.Enabled = false;
            buttonPowerOff.Enabled = true;
            if (!string.IsNullOrEmpty(vmNameSelectd))
            {
                getSnapshotInformation(vmNameSelectd);

                String currentProductsInstalled = readRemoteRegistry(vmNameSelectd);
                String[] productsInstall = null;
                try
                {
                    productsInstall = currentProductsInstalled.Split(':');

                }
                catch (NullReferenceException)
                {
                    listBoxProductsInstalled.Items.Clear();
                    listBoxProductsInstalled.Items.Add("IRC/CM is not installed on Selected VM");
                }

                try
                {
                    if (productsInstall.Length > 0)
                    {

                        listBoxProductsInstalled.Items.Add("----------------------------------------------------------------");
                        listBoxProductsInstalled.Items.Add("IRC Product Version :" + Convert.ToString(_IRCVersion));
                        if (!string.IsNullOrEmpty(Convert.ToString(_CMVersion)))
                            listBoxProductsInstalled.Items.Add("CM Product Version :" + Convert.ToString(_CMVersion));
                        else
                            listBoxProductsInstalled.Items.Add("CM Product Version: NOT INSTALLED");
                        listBoxProductsInstalled.Items.Add("----------------------------------------------------------------");
                        String[] componenets = _productsInstalled[0].Split(',');
                        for (int i = 0; i < componenets.Length; i++)
                            if (!string.IsNullOrEmpty(componenets[i]))
                                listBoxProductsInstalled.Items.Add(i + 1 + ":" + componenets[i]);
                        listBoxProductsInstalled.Items.Add("----------------------------------------------------------------");
                    }
                }
                catch (NullReferenceException)
                {
                    //MessageBox.Show("Error while retrieving values");
                }
            }
        }

        private String readRemoteRegistry(String vmNameSelectd)
        {
            String ServerName = vmNameSelectd.ToString();
            var registry_key = @"SOFTWARE\Approva";
            RegistryKey key = null;
            NetworkConnection netConn = null;
            _IRCVersion = null;
            _productsInstalled = null;
            _CMVersion = null;
            String versionNum = null;

            if (ServerName.Contains("INPULAB"))
                // netConn = new NetworkConnection(@"\\" + ServerName + @"\admin$", new NetworkCredential(@"administrator", @"approva1@", "inpulab.int"));
                return null;
            else
            {
                lock (_genericUID)
                {
                    netConn = new NetworkConnection(@"\\" + ServerName + @"\admin$", new NetworkCredential(_genericUID, _genricPWD, ServerName));
                }
            }

            if (netConn.success)
            {
                lock (ServerName)
                {
                    if (startRemoteRegistryService(vmNameSelectd))
                    {
                        using (RegistryKey remoteHklm = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ServerName))
                        {
                            if (remoteHklm != null)
                            {
                                try
                                {
                                    System.Threading.Thread.Sleep(100);
                                    key = remoteHklm.OpenSubKey(registry_key);
                                    if (key != null)
                                    {
                                        versionNum = Convert.ToString(key.OpenSubKey("BizRights").GetValue("VersionNum"));
                                        _IRCVersion = versionNum;
                                        _productsInstalled = (string[])(key.OpenSubKey("BizRights").GetValue(@"Products"));
                                        _CMVersion = Convert.ToString(key.OpenSubKey("CertificationManager").GetValue(@"Version"));
                                    }
                                    else
                                        return null;
                                }
                                catch (Exception e)
                                {
                                    Logger.WriteError("Exception while reading Registry for VM :" + ServerName + " Details:" + e.StackTrace);
                                }
                            }
                        }
                        netConn.Dispose();
                        return versionNum;
                    }
                    else
                        return null;
                }
            }
            return null;
        }

        private void listBoxPowerOff_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelInstalledProducts.Text = "Installed Prodcuts";
            buttonPowerOff.Enabled = false;
            buttonPowerOn.Enabled = true;
            lbHost.Items.Clear();
            listVM.ClearSelected();
            listBoxProductsInstalled.Items.Clear();
            buttonSearch.Enabled = true;
            String vmSelected = null;
            try
            {
                vmSelected = listBoxPowerOff.SelectedItem.ToString();
            }
            catch (Exception)
            {
            }

            if (!String.IsNullOrEmpty(vmSelected))
                getSnapshotInformation(vmSelected);
        }

        private void getSnapshotInformation(String SelectedVMName)
        {
            VirtualMachineSnapshotTree[] snapshotsInformation = null;
            if (!String.IsNullOrEmpty(SelectedVMName))
            {
                snapshotsInformation = vmSnaps[SelectedVMName];
                int length = snapshotsInformation.Length;

                for (int i = 0; i < length; i++)
                {
                    lbHost.Items.Add(snapshotsInformation[i].Name);
                }
            }
        }

        private void buttonPowerOff_Click(object sender, EventArgs e)
        {
            String vmNameSelectd = null;
            try
            {
                vmNameSelectd = listVM.SelectedItem.ToString();
            }
            catch (Exception)
            {
                return;
            }
            DialogResult dr = MessageBox.Show("Do you want to Shut Down VM: " + vmNameSelectd, "Shut Down Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

            if (dr == DialogResult.Yes)
            {
                // create filter by VM name
                NameValueCollection filter = new NameValueCollection();
                filter.Add("name", vmNameSelectd);
                // get the VirtualMachine view object
                VirtualMachine vm = (VirtualMachine)Client.FindEntityView(typeof(VirtualMachine), null, filter, null);
                if (vm != null)
                {
                    try
                    {
                        vm.ShutdownGuest();
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message);
                    }
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            String searchVersion = textBoxSearch.Text.TrimEnd().TrimStart().ToLower();
            if (!string.IsNullOrEmpty(searchVersion))
            {
                progressBar1.Visible = true;
                //System.Threading.Thread.Sleep(500);
                progressBar1.Value = 30;
                buttonSearch.Enabled = false;
                textBoxSearch.ReadOnly = true;
                listVM.Enabled = false;
                listBoxPowerOff.Enabled = false;
                listBoxProductsInstalled.Items.Clear();
                lbHost.Items.Clear();
                installInformation.Clear();
                listBoxProductsInstalled.Items.Clear();

                searchVer = searchVersion;
                labelInstalledProducts.Text = "VM with IRC v" + searchVersion + " installed:";
                this.listBoxProductsInstalled.ForeColor = System.Drawing.SystemColors.MenuHighlight;
                listBoxProductsInstalled.Enabled = false;

                watch.Start();
                backgroundWorker1.RunWorkerAsync();
                autoReset.WaitOne();

                String temp = null;
                listBoxProductsInstalled.Enabled = true;
                Thread.Sleep(1000);

                foreach (String key in installInformation.Keys)
                {
                    try
                    {
                        temp = installInformation[key];
                        Logger.WriteMessage("key:" + key + "-" + temp);
                    }
                    catch (Exception)
                    { }
                    if (temp.ToString().Contains(searchVer))
                    {
                        listBoxProductsInstalled.Items.Add(key);
                    }
                    temp = null;
                }

                listBoxProductsInstalled.Items.Add("----------------------------------------------------------------");
                listBoxProductsInstalled.Items.Add("Total Time Taken:" + watch.Elapsed);
                if (listBoxProductsInstalled.Items.Count < 3)
                    listBoxProductsInstalled.Items.Add("There is no vm with IRC v" + searchVersion + " installed");
                listBoxProductsInstalled.Items.Add("----------------------------------------------------------------");
                textBoxSearch.ReadOnly = false;
                listVM.Enabled = true;
                listBoxPowerOff.Enabled = true;
                buttonSearch.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please Enter Valid IRC Version Number", "IRC Version", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void getRegDetails(object vmName)
        {

            String vName = vmName.ToString();
            Logger.WriteMessage(vName);
            String currentProductsInstalled = null;


            currentProductsInstalled = readRemoteRegistry(vName);
            // Logger.WriteMessage("Current Products Installed on " + vName + " : " + currentProductsInstalled);       
            try
            {
                if (!String.IsNullOrEmpty(currentProductsInstalled))
                {
                    installInformation.Add(vName, currentProductsInstalled);
                }
            }
            catch (ArgumentException a)
            {
                // MessageBox.Show(vmName + " is already present");
                Logger.WriteError("Exception at Adding Value to installInformation: " + vName + ":" + a.StackTrace);
            }
            catch (NullReferenceException n)
            {
                Logger.WriteError(n.StackTrace);
            }
            catch (Exception g)
            {
                Logger.WriteMessage(g.StackTrace);
            }


        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSearch_Click(this, new EventArgs());
            }
        }

        private void listBoxProductsInstalled_SelectedIndexChanged(object sender, EventArgs e)
        {
            _IRCVersion = null;
            _CMVersion = null;
            _productsInstalled = null;

            String vmNameSelectd = null;

            vmNameSelectd = Convert.ToString(listBoxProductsInstalled.SelectedItem);
            listBoxPowerOff.ClearSelected();
            buttonPowerOn.Enabled = false;
            buttonPowerOff.Enabled = true;
            if (!string.IsNullOrEmpty(vmNameSelectd))
            {
                if (vmNameSelectd.ToLower().Contains("approva"))
                {
                    listBoxProductsInstalled.Height = 132;
                    labelInstalledProducts.Location = new System.Drawing.Point(248, 252);
                    listBoxProductsInstalled.Location = new System.Drawing.Point(248, 272);
                    lbHost.Height = 185;
                    label2.Text = "Selected VM Installed Prodcuts";
                    lbHost.Items.Clear();
                    getSnapshotInformation(vmNameSelectd);

                    String currentProductsInstalled = readRemoteRegistry(vmNameSelectd);

                    if (!String.IsNullOrEmpty(currentProductsInstalled))
                    {
                        lbHost.Items.Add("----------------------------------------------------------------");
                        lbHost.Items.Add("IRC Product Version :" + Convert.ToString(_IRCVersion));
                        if (!string.IsNullOrEmpty(Convert.ToString(_CMVersion)))
                            lbHost.Items.Add("CM Product Version :" + Convert.ToString(_CMVersion));
                        else
                            lbHost.Items.Add("CM Product Version: NOT INSTALLED");
                        lbHost.Items.Add("----------------------------------------------------------------");
                        String[] componenets = _productsInstalled[0].Split(',');
                        for (int i = 0; i < componenets.Length; i++)
                            if (!string.IsNullOrEmpty(componenets[i]))
                                lbHost.Items.Add(i + 1 + ":" + componenets[i]);
                        lbHost.Items.Add("----------------------------------------------------------------");
                    }
                    else
                    {
                        lbHost.Items.Clear();
                        lbHost.Items.Add("IRC/CM is not installed on Selected VM");
                    }
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(40);
            int remainingThread = 0;
            if (!String.IsNullOrEmpty(searchVer))
            {
                int countVMs = PowerOnvmNameList.Count;
                int c = 0;
                int chunk = 0;
                remainingThread = countVMs % 3;
                // Logger.WriteMessage("Power On VM Count: " + countVMs + " Remaining Thread: " + remainingThread);
                for (chunk = 0; chunk < countVMs / 3; chunk++)
                {
                    taskArray = new System.Threading.Tasks.Task[3];
                    lock (PowerOnvmNameList)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            Logger.WriteMessage("C Value:" + c + " VM NAME:" + PowerOnvmNameList[c]);
                            taskArray[j] = System.Threading.Tasks.Task.Run(() => getRegDetails(PowerOnvmNameList[c++]));
                        }
                    }
                    System.Threading.Tasks.Task.WaitAll(taskArray);
                    taskArray = null;
                }
                backgroundWorker1.ReportProgress(90);
                if (remainingThread != 0)
                {
                    taskArray = new System.Threading.Tasks.Task[remainingThread];
                    lock (PowerOnvmNameList)
                    {
                        for (int j = 0; j < remainingThread; j++)
                        {
                            // Logger.WriteMessage("C Value:" + c + " VM NAME:" + PowerOnvmNameList[c]);
                            taskArray[j] = System.Threading.Tasks.Task.Run(() => getRegDetails(PowerOnvmNameList[c++]));
                        }
                    }
                    System.Threading.Tasks.Task.WaitAll(taskArray);
                }
                autoReset.Set();
                watch.Stop();
                backgroundWorker1.ReportProgress(100);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Thread.Sleep(500);
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Inventory_FormClosed(object sender, FormClosedEventArgs e)
        {
            clearConnections();
        }

        private void clearConnections()
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.StartInfo.Arguments = "/c net use * /delete /Y";
            cmd.Start();
        }

        private bool startRemoteRegistryService(String Vmname)
        {
            bool flag = false;
            try
            {
                ServiceController service = new ServiceController("RemoteRegistry", Vmname);
                if (service.Status.Equals(ServiceControllerStatus.Stopped))
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                    flag = true;
                }
                else if (service.Status.Equals(ServiceControllerStatus.Running))
                {
                    flag = true;
                }
            }
            catch (Exception serviceException)
            {
                flag = false;
                Logger.WriteMessage("Unable to start Remote Registry on server " + Vmname);
                Logger.WriteMessage(serviceException.Message);
            }
            return flag;
        }
    }
}